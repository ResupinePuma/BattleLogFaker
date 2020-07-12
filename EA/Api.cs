using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using xNet;
using System.Xml.Serialization;
using System.Xml;



namespace BattleLog.EA
{
    public class Api
    {
        public bool is_proxy { get; set; }
        public ProxyClient proxy { get; set; }

        public string APIName = "bf3";

        public string MCUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36 Edge/18.18362";
        public int JoinLimit { get; set; }

        public Tuple<bool,string> JoinBfServer(Account account, Server server)
        {
            if (server.cur >= server.max)
                return Tuple.Create(false, $"Server players: {server.cur}/{server.max}");
            if (server.joinLimit != 0 && server.cur >= server.joinLimit)
                return Tuple.Create(false, $"Join limit reached");
            if (account.joinTime.Count != 0)
            {
                if (DateTime.Now.Subtract(account.joinTime[0]).TotalSeconds > 60 * 60 * 24)
                {
                    if (account.lastusedCount < 30)
                    {
                        account.joinTime.RemoveAt(0);
                        account.lastusedCount--;
                    }
                    else
                        return Tuple.Create(false, $"Account used {account.lastusedCount} times for last 24H");
                }
                else if (DateTime.Now.Subtract(account.joinTime[account.joinTime.Count - 1]).TotalSeconds < server.sleepTime)
                    return Tuple.Create(false, $"Account used less than {server.sleepTime} seconds ago");
                else
                    if (!(account.lastusedCount < server.requestLimit))
                        return Tuple.Create(false, $"Account used {account.lastusedCount} times for last 24H");
            }
            using (HttpRequest request = new HttpRequest())
            {
                try
                {
                    request.AllowAutoRedirect = true;
                    request.IgnoreProtocolErrors = true;
                    request.Cookies = account.GetCookies(account.cookies);
                    request.UserAgent = this.MCUserAgent;
                    string cS = GetCheckSum(request.Cookies);
                    if (this.is_proxy)
                        request.Proxy = proxy;
                    string response = request.Post($"http://battlelog.battlefield.com/bf3/launcher/reserveslotbygameid/1/{account.personaId}/{server.gid}/1/{server.guid}/0", $"post-check-sum={cS}", "application/x-www-form-urlencoded; charset=UTF-8").ToString();
                    if (this.is_proxy)
                        request.Proxy = proxy;
                    var persona = request.Get("http://battlelog.battlefield.com/bf3/launcher/playablepersona/");
                    account.cookies = account.SetCookies(persona.Cookies);
                    if (response != string.Empty)
                    {
                        account.joinTime.Add(DateTime.Now);
                        account.lastusedCount++;
                    }
                    if (response.Contains("JOINED_GAME"))
                        return Tuple.Create(true, $"");
                    else
                        return Tuple.Create(false, $"Response message: {response.Replace("\n","")}");
                }
                catch (Exception ex){
                    account.joinTime.Insert(0, DateTime.MinValue);
                    account.lastusedCount++;
                    throw new Exception(ex.ToString());
                }           
            }
        }

        public Tuple<Account, string> Login(String Email, String Password)
        {
            using (HttpRequest request = new HttpRequest())
            {
                // request.Proxy = ProxyClient.Parse(ProxyType.);
                request.AllowAutoRedirect = true;
                request.IgnoreProtocolErrors = true;
                request.KeepAlive = true;
                request.Cookies = new CookieDictionary();
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36 Edge/18.18362";
                //request.AddHeader("Upgrade-Insecure-Requests", "1");
                if (this.is_proxy)
                    request.Proxy = proxy;
                var response = request.Get("https://accounts.ea.com/connect/auth?locale=en_US&state=bf3&redirect_uri=https%3A%2F%2Fbattlelog.battlefield.com%2Fsso%2F%3Ftokentype%3Dcode&response_type=code&client_id=battlelog&display=web%2Flogin");
                string js = request.Post(request.Address, $"email={Email}&password={Password}&pn_text=&passwordForPhone=&country=IQ&phoneNumber=&_rememberMe=on&rememberMe=on&_eventId=submit&gCaptchaResponse=&isPhoneNumberLogin=false&isIncompletePhone=&countryPrefixPhoneNumber=", "application/x-www-form-urlencoded").ToString();
                if (request.Cookies.ToString().Contains($"webun={Email};"))
                {
                    Match a = Regex.Match(js, "var redirectUri = '(.*?)'");
                    string JSRedi = a.Groups[1].Value + "&_eventId=end";
                    response = request.Get(JSRedi);
                    if (this.is_proxy)
                        request.Proxy = proxy;
                    string persona = request.Get("http://battlelog.battlefield.com/bf3/launcher/playablepersona/").ToString();
                    a = Regex.Match(persona, "\"isLoggedIn\":(.*?),", RegexOptions.Multiline);
                    bool isLogged = false;
                    if (bool.TryParse(a.Groups[1].Value, out isLogged))
                        if (isLogged == false)
                            return Login(Email, Password);

                    a = Regex.Match(persona, "\"personaId\":\"(.*?)\"", RegexOptions.Multiline);
                    int id = 0;
                    if (int.TryParse(a.Groups[1].Value, out id))
                        if (id != 0)
                            return Tuple.Create(new Account(Email, response.Cookies.ToString(), id), "");
                        else
                            return Tuple.Create(new Account(Email, response.Cookies.ToString(), id), "Account potentialy banned or doesn't have a game");
                    else
                        throw new Exception($"Can not load account: {Email} {persona}");
                }
                else
                    return Tuple.Create(new Account(Email, response.Cookies.ToString(), 0), $"Something wrong with cookies: {response.Cookies.ToString()}");
            }
        }

         public string GetCheckSum(CookieDictionary cookies)
        {
            cookies.TryGetValue("beaker.session.id", out string FullCheckSum);
            string CheckSum = FullCheckSum.Substring(0, 10);
            return CheckSum;
        }

    }

    //public class Parser
    //{
    //    public bool is_proxy { get; set; }
    //    public ProxyClient proxy { get; set; }

    //    public CookieDictionary EaCookie = new CookieDictionary();
    //    private int MAX_PLAYER { get; set; }
    //    private int CURRENT_PLAYERS { get; set; }
    //    public String MCUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36 Edge/18.18362";
    //    public string LoginUrl = "https://accounts.ea.com/connect/auth?locale=en_US&state=bf3&redirect_uri=https%3A%2F%2Fbattlelog.battlefield.com%2Fsso%2F%3Ftokentype%3Dcode&response_type=code&client_id=battlelog&display=web%2Flogin";

    //    public string GetPersonaId()
    //    {
    //        using (HttpRequest request = new HttpRequest())
    //        {
    //            request.AllowAutoRedirect = true;
    //            request.IgnoreProtocolErrors = true;
    //            request.Cookies = this.EaCookie;
    //            request.UserAgent = this.MCUserAgent;
    //            request.AddHeader("Upgrade-Insecure-Requests", "1");
    //            var response = request.Get("http://battlelog.battlefield.com/bf3/launcher/playablepersona/");
    //            String str = response.ToString();
    //            String id = Regex.Match(str, "\"personaId\":\"(.*?)\",").Groups[1].Value;
    //            return id;
    //        }
    //    }

    //    public string GetGameId(String ServerGUID)
    //    {
    //        using (HttpRequest request = new HttpRequest())
    //        {
    //            request.AllowAutoRedirect = true;
    //            request.IgnoreProtocolErrors = true;
    //            request.Cookies = this.EaCookie;
    //            request.UserAgent = this.MCUserAgent;
    //            var response = request.Get($"http://battlelog.battlefield.com/bf3/en/servers/show/pc/{ServerGUID}/?json=1&join=true");
    //            String respo = response.ToString();
    //            this.CURRENT_PLAYERS = int.Parse(Regex.Match(respo, "\"2\":{\"current\":(\\d{1,2}),\"max\":(\\d{1,2})}}").Groups[1].Value);
    //            this.MAX_PLAYER = int.Parse(Regex.Match(respo, "\"2\":{\"current\":(\\d{1,2}),\"max\":(\\d{1,2})}}").Groups[2].Value);
    //            String id = Regex.Match(respo, "\"gameId\":\"(.*?)\"").Groups[1].Value;
    //            return id;
    //        }
    //    }        

    //}

    [Serializable]
    public class Account
    {
        public string name { get; set; }
        public string cookies { get; set; }
        public List<DateTime> joinTime { get; set; }
        public int lastusedCount { get; set; }
        public int personaId { get; set; }

        public CookieDictionary GetCookies(string cookies)
        {
            CookieDictionary Cookies = new CookieDictionary();
            int i = 0;
            foreach (string elem in cookies.Split(';'))
            {
                string[] data = elem.Split(new[] { '=' }, 2);
                if (data[0][0] == ' ')
                    data[0] = data[0].Substring(1);
                Cookies.Add(data[0], data[1]);
            }
            return Cookies;
        }
        public string SetCookies(CookieDictionary cookies)
        {
            string Cookies = string.Empty;
            Cookies = cookies.ToString();
            this.cookies = Cookies;
            return Cookies;
        }

        public void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }
        public T ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }

        public Account(string Email, string cookie, int persona)
        {
            this.name = Email;
            this.cookies = cookie;
            this.joinTime = new List<DateTime>();
            this.lastusedCount = 0;
            this.personaId = persona;
        }

        public Account(){}
    }

    [Serializable]
    public class Server
    {
        public string name { get; set; }
        public string ip { get; set; }
        public string guid { get; set; }
        public long gid { get; set; }
        public bool work { get; set; }
        public int joinLimit { get; set; }
        public int requestLimit { get; set; }
        public int max { get; set; }
        public int cur { get; set; }
        public int sleepTime { get; set; }
        public bool is_proxy { get; set; }
        public ProxyClient proxy { get; set; }
        public int maxThreads { get; set; }
        public int updateTime { get; set; }
        public DateTime updatedTime { get; set; }

        public int tickTime { get; set; }

        public Server(string guid, bool work = false)
        {
            this.guid = guid;
            this.work = work;
            this.requestLimit = 30;
            this.sleepTime = 120;
            this.updateTime = 5;
            this.tickTime = 30;
            this.maxThreads = 1;
        }

        public Server()
        {
            this.work = false;
            this.requestLimit = 30;
            this.sleepTime = 5;
            this.tickTime = 30;
            this.updateTime = 5;
            this.maxThreads = 1;
        }

        


        public void StartServerMonitor()
        {
            new Task(() => {
                while (this.work)
                {
                    try
                    {
                        if (Guid.TryParse(this.guid, out Guid pguid))
                            using (HttpRequest request = new HttpRequest())
                            {
                                request.AllowAutoRedirect = true;
                                if (this.is_proxy)
                                    request.Proxy = proxy;
                                string sg = pguid.ToString();
                                HttpResponse response = request.Get($"http://battlelog.battlefield.com/bf3/en/servers/show/pc/{sg}/?json=1");
                                string respo = response.ToString();
                                Match m = Regex.Match(respo, "\"2\":{\"current\":(\\d{1,2}),\"max\":(\\d{1,2})}}");
                                this.cur = int.Parse(m.Groups[1].Value);
                                this.max = int.Parse(m.Groups[2].Value);
                                this.ip = Regex.Match(respo, "(\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3})").Groups[0].Value;
                                this.name = Regex.Match(respo, "\"name\":\"(.*?)\"").Groups[1].Value;
                                this.gid = long.Parse(Regex.Match(respo, "\"gameId\":\"(.*?)\"").Groups[1].Value);
                                updatedTime = DateTime.Now;
                            }
                    }
                    catch
                    { }
                    Thread.Sleep(this.updateTime * 1000);
                }
                this.cur = 0;
                this.max = 0;
                this.ip = null;
                this.name = null;
                this.gid = 0;
            }).Start();
        }

    }
}
