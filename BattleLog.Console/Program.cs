using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BattleLog.EA;

namespace BattleLog
{
    class Program
    {
        public static bool flag = false;
        public static object obj = new object();

        public static Server server = new Server();
        public static Api ea = new Api();
        public static string ServerGuid = string.Empty;
        public static List<Account> Accounts = new List<Account>();
        public static List<Account> TempAccounts = new List<Account>();

        static void Main(string[] args)
        {
            if (File.Exists(@"settings.bin"))
                LoadConfig();
            if (File.Exists(@"accounts.bin"))
            {
                new Task(() =>
                {
                    AddLog($"Loading accounts from accounts.bin");
                    Accounts = new Account().ReadFromBinaryFile<List<Account>>("accounts.bin");
                    TempAccounts = Accounts;
                    AddLog($"Loaded {Accounts.Count} account");
                }).Start();
            }
            flag = true;
            server.work = true;
            new Task(() => {
                while (true)
                {
                    AddLog("", false);
                    Thread.Sleep(1000);
                }
            }).Start();
            server.StartServerMonitor();
            Console.Read();
        }

        private void SaveConfig()
        {
            new Account().WriteToBinaryFile("settings.bin", server);
        }

        static private void LoadConfig()
        {
            try
            {
                server = new Account().ReadFromBinaryFile<Server>("settings.bin");
                flag = true;
                Start();
            }
            catch { };
        }

        static List<string> text = new List<string>(21);
        static private void AddLog(string Text, bool upd = true)
        {
            lock (obj)
            {
                DateTime time = DateTime.Now;
                if (upd)
                {
                    if (text.Count >= 19)
                        text.RemoveAt(0);
                    text.Add($"[{time.ToString("MM/dd/yyyy HH:mm:ss")}] " + Text.Substring(0, (Text.Length > 70 ? 70 : Text.Length)));

                    try
                    {
                        using (StreamWriter w = File.AppendText("log.log"))
                        {
                            w.WriteLine($"[{time.ToString("MM/dd/yyyy HH:mm:ss")}] " + Text);
                        }
                    }
                    catch { }
                }
                string text2 = String.Join("\n", text);
                string header1 = string.Format("#SERVER\n\tPlayers: {0}\t\t\tName: {1}\n\tIP: {2}\t\t\tJoin limit: {3}\n", $"{server.cur} / {server.max}",server.name, server.ip, server.joinLimit);
                string header2 = string.Format("#SETTINGS\n\tMax request per account: {0}\t\tTime tick (sec): {1}\n\tSleep between requests (sec): {2}\t\tMax acc per tick: {3}\n\n", server.requestLimit, server.tickTime, server.sleepTime, server.maxThreads);
                Console.Clear();
                Console.WriteLine(header1 + header2 + text2);                
            }
        }

        public static List<Account> GetAccounts()
        {
            var acc = TempAccounts.OrderBy(x => x.lastusedCount);
            List<Account> aaaa = acc.Take(server.maxThreads).ToList();
            TempAccounts.RemoveAll(x => aaaa.Contains(x));
            return aaaa;
        }

        public static void SetAccounts(List<Account> accs)
        {
            foreach (Account a in accs)
            {
                if (!TempAccounts.Contains(a))
                    TempAccounts.Add(a);
            }
        }

        public static async Task StartThread(Account account)
        {
            await Task.Run(() =>
            {
                try
                {
                    var t = ea.JoinBfServer(account, server);
                    if (t.Item1)
                        AddLog($"Successfully joined: {account.name}");
                    else
                        AddLog($"Error when join: {account.name} - {t.Item2}");
                }
                catch (Exception ex)
                {
                    AddLog($"Exception when join: {account.name} - {ex.ToString()}");
                }
            });
        }

        public static async Task ThreadWorker()
        {
            while (true)
            {
                while (server.cur < server.joinLimit && flag)
                {
                    List<Account> accounts = GetAccounts();
                    var q = new ConcurrentQueue<Account>(accounts);
                    var tasks = new List<Task>();
                    int threads = server.joinLimit - server.cur > server.maxThreads ? server.maxThreads : server.joinLimit - server.cur;
                    for (int n = 0; n < threads; n++)
                    {
                        tasks.Add(Task.Run(async () =>
                        {
                            while (q.TryDequeue(out Account acc))
                            {
                                await StartThread(acc);
                            }
                        }));
                    }
                    await Task.WhenAll(tasks);
                    Thread.Sleep(server.tickTime * 1000);
                    SetAccounts(accounts);
                }
                Thread.Sleep(1000);
            }
        }

        public static void Start()
        {
            new Task(() =>
            {
                ThreadWorker();
            }).Start();
        }

        public async Task StartLoadAccsThread(string acc)
        {
            await Task.Run(() =>
            {
                string[] data = acc.Split(':');
                try
                {
                    var l = ea.Login(data[0], data[1]);
                    if (l.Item1.personaId == 0)
                        AddLog($"Error when load account: {data[0]} - {l.Item2}");
                    else
                    {
                        AddLog($"Account: {data[0]} ready");
                        Accounts.Add(l.Item1);
                    }
                }
                catch (Exception ex)
                {
                    AddLog($"Exception with account {data[0]}: {ex.ToString()}");
                }
            });
        }

        //public async Task ThreadLoadAccsWorker(string[] accounts)
        //{
        //    int count = 0;
        //    while (accounts.Length != count)
        //    {
        //        var q = new ConcurrentQueue<string>(accounts);
        //        var tasks = new List<Task>();
        //        for (int n = 0; n < 20; n++)
        //        {
        //            tasks.Add(Task.Run(async () =>
        //            {
        //                while (q.TryDequeue(out string acc))
        //                {
        //                    count++;
        //                    await StartLoadAccsThread(acc);
        //                    AccsLabel.Text = $"Accounts count: {Accounts.Count}";
        //                }
        //            }));
        //            ServerBox.Enabled = true;
        //        }
        //        await Task.WhenAll(tasks);
        //    }
        //    new Account().WriteToBinaryFile("accounts.bin", Accounts, true);
        //    AddLog($"Loaded {Accounts.Count} accounts");
        //}

        //private void LoadAccs_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        using (OpenFileDialog ofd = new OpenFileDialog())
        //        {
        //            if (ofd.ShowDialog() == DialogResult.OK)
        //            {
        //                AddLog($"Account loading started");
        //                new Task(() =>
        //                {
        //                    ThreadLoadAccsWorker(File.ReadAllLines(ofd.FileName));
        //                }).Start();

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        //private void Requests_ValueChanged(object sender, EventArgs e)
        //{
        //    int value = 30;
        //    int.TryParse((sender as NumericUpDown).Value.ToString(), out value);
        //    server.requestLimit = value;
        //}

        //private void Sleep_ValueChanged(object sender, EventArgs e)
        //{
        //    int value = 120;
        //    int.TryParse((sender as NumericUpDown).Value.ToString(), out value);
        //    server.sleepTime = value;
        //}

        //private void Lock_Ckecked(object sender, EventArgs e)
        //{
        //    if ((sender as CheckBox).Checked)
        //    {
        //        GuidBox.Enabled = false;
        //        server.work = true;
        //        server.StartServerMonitor();
        //    }
        //    else
        //    {
        //        GuidBox.Enabled = true;
        //        server.work = false;
        //    }
        //}

        //private void GuidBox_TextChanged(object sender, EventArgs e)
        //{
        //    TextBox tb = (sender as TextBox);
        //    server.guid = tb.Text;

        //    new Task(() => {
        //        while (true)
        //        {
        //            PCount.Text = $"Players: {server.cur}/{server.max}";
        //            Sip.Text = $"IP: {server.ip}";
        //            SName.Text = $"Name: {server.name}";
        //            JoinLimitCnt.Maximum = server.max;
        //            SrvUpdateLabel.Text = $"Updated at: {server.updatedTime.ToString("HH:mm:ss")}";
        //            Thread.Sleep(1000);
        //        }
        //    }).Start();
        //}

        private void StartWorkerBtn_Click(object sender, EventArgs e)
        {
            flag = true;
            Start();
        }

        //private void StopWorkerBtn_Click(object sender, EventArgs e)
        //{
        //    this.flag = false;
        //    StopWorkerBtn.Enabled = false;
        //    StartWorkerBtn.Enabled = true;
        //}

        //private void LogTB_TextChanged(object sender, EventArgs e)
        //{
        //    LogTB.SelectionStart = LogTB.Text.Length;
        //    LogTB.ScrollToCaret();
        //}

        //private void ThreadCount_Changed(object sender, EventArgs e)
        //{
        //    int value = 1;
        //    int.TryParse((sender as NumericUpDown).Value.ToString(), out value);
        //    server.maxThreads = value;
        //}

        //private void JoinLimit_Changed(object sender, EventArgs e)
        //{
        //    int value = 0;
        //    int.TryParse((sender as NumericUpDown).Value.ToString(), out value);
        //    server.joinLimit = value;
        //}

        //private void ServerUpdate_Changed(object sender, EventArgs e)
        //{
        //    int value = 5;
        //    int.TryParse((sender as NumericUpDown).Value.ToString(), out value);
        //    server.updateTime = value;
        //}

        //private void TimeTick_Changed(object sender, EventArgs e)
        //{
        //    int value = 30;
        //    int.TryParse((sender as NumericUpDown).Value.ToString(), out value);
        //    server.tickTime = value;
        //}





    }
}
