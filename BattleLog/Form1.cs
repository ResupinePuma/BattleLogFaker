using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System.Xml.Serialization;
using xNet;
using System.Collections.Concurrent;
using BattleLog.EA;

namespace BattleLog
{
    public partial class Form1 : Form
    {
        bool flag = false;
        object obj = new object();

        Server server = new Server();
        Api ea = new Api();
        string ServerGuid = string.Empty;
        private List<Account> Accounts = new List<Account>();
        private List<Account> TempAccounts = new List<Account>();

        public Form1(string[] args = null)
        {
            InitializeComponent();
            Text = new Api().APIName + " " + Text;
            if (File.Exists(@"settings.bin"))
                LoadConfig();
            if (File.Exists(@"accounts.bin"))
            {
                new Task(() =>
                {
                    Invoke(new Action(() =>
                    {
                        AddLog($"Loading accounts from accounts.bin");
                    }));                    
                    Accounts = new Account().ReadFromBinaryFile<List<Account>>("accounts.bin");
                    TempAccounts = Accounts;
                    Invoke(new Action(() =>
                    {
                        AccsLabel.Text = $"Accounts count: {Accounts.Count}";
                        AddLog($"Loaded {Accounts.Count} account");
                        ServerBox.Enabled = true;
                    }));
                }).Start();
            }
        }

        private void SaveConfig()
        {
            new Account().WriteToBinaryFile("settings.bin", server);
        }

        private void LoadConfig()
        {
            try
            {
                server = new Account().ReadFromBinaryFile<Server>("settings.bin");
                //this.flag = true;
                //Start();
            }
            catch { };
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfig();
            if (Accounts.Count != 0)
            {
                foreach (Account ta in TempAccounts)
                    Accounts.Find(x => x.personaId == ta.personaId).lastusedCount = ta.lastusedCount;
                new Account().WriteToBinaryFile("accounts.bin", Accounts);
            }
                
        }

        private void AddLog(string Text)
        {
            lock (obj)
            {
                DateTime time = DateTime.Now;
                string text = $"[{time.ToString("MM/dd/yyyy HH:mm:ss")}] " + Text.Substring(0, (Text.Length > 70 ? 70 : Text.Length)) + "\r\n";
                LogTB.Text += text;
                try
                {
                    using (StreamWriter w = File.AppendText("log.log"))
                    {
                        w.WriteLine($"[{time.ToString("MM/dd/yyyy HH:mm:ss")}] " + Text);
                    }
                }
                catch { }
            }
        }

        public List<Account> GetAccounts()
        {
            var acc = TempAccounts.OrderBy(x => x.lastusedCount);
            List<Account> aaaa = acc.Take(server.maxThreads).ToList();
            TempAccounts.RemoveAll(x => aaaa.Contains(x));
            return aaaa;
        }

        public void SetAccounts(List<Account> accs)
        {
            foreach (Account a in accs)
            {
                if (!TempAccounts.Contains(a))
                    TempAccounts.Add(a);
            }
        }

        public async Task StartThread(Account account)
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

        public async Task ThreadWorker()
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

        public void Start()
        {            
            new Task(() =>
            {
               ThreadWorker();
            }).Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
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

        public async Task ThreadLoadAccsWorker(string[] accounts)
        {
            int count = 0;
            while (accounts.Length != count)
            {
                var q = new ConcurrentQueue<string>(accounts);
                var tasks = new List<Task>();
                for (int n = 0; n < 20; n++)
                {
                    tasks.Add(Task.Run(async () =>
                    {
                        while (q.TryDequeue(out string acc))
                        {
                            count++;
                            await StartLoadAccsThread(acc);
                            AccsLabel.Text = $"Accounts count: {Accounts.Count}";
                        }
                    }));
                    ServerBox.Enabled = true;
                }
                await Task.WhenAll(tasks);                
            }
            new Account().WriteToBinaryFile("accounts.bin", Accounts, true);
            AddLog($"Loaded {Accounts.Count} accounts");
        }

        private void LoadAccs_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        AddLog($"Account loading started");
                        new Task(() =>
                        {
                            ThreadLoadAccsWorker(File.ReadAllLines(ofd.FileName));
                        }).Start();

                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void Requests_ValueChanged(object sender, EventArgs e)
        {
            int value = 30;
            int.TryParse((sender as NumericUpDown).Value.ToString(), out value);
            server.requestLimit = value;
        }

        private void Sleep_ValueChanged(object sender, EventArgs e)
        {
            int value = 120;
            int.TryParse((sender as NumericUpDown).Value.ToString(), out value);
            server.sleepTime = value;
        }

        private void Lock_Ckecked(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                GuidBox.Enabled = false;
                server.work = true;
                server.StartServerMonitor();
            }
            else
            {
                GuidBox.Enabled = true;
                server.work = false;
            }
        }

        private void GuidBox_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (sender as TextBox);
            server.guid = tb.Text;

            new Task(() => {
                while (true)
                {
                    PCount.Text = $"Players: {server.cur}/{server.max}";
                    Sip.Text = $"IP: {server.ip}";
                    SName.Text = $"Name: {server.name}";
                    JoinLimitCnt.Maximum = server.max;
                    SrvUpdateLabel.Text = $"Updated at: {server.updatedTime.ToString("HH:mm:ss")}";
                    Thread.Sleep(1000);
                }
            }).Start();
        }

        private void StartWorkerBtn_Click(object sender, EventArgs e)
        {
            this.flag = true;
            this.ServerGuid = GuidBox.Text;
            //this.sleep = (int)sleepCount.Value;
            StopWorkerBtn.Enabled = true;
            StartWorkerBtn.Enabled = false;
            Start();
        }

        private void StopWorkerBtn_Click(object sender, EventArgs e)
        {
            this.flag = false;
            StopWorkerBtn.Enabled = false;
            StartWorkerBtn.Enabled = true;
        }

        private void LogTB_TextChanged(object sender, EventArgs e)
        {
            LogTB.SelectionStart = LogTB.Text.Length;
            LogTB.ScrollToCaret();
        }

        private void ThreadCount_Changed(object sender, EventArgs e)
        {
            int value = 1;
            int.TryParse((sender as NumericUpDown).Value.ToString(), out value);
            server.maxThreads = value;
        }

        private void JoinLimit_Changed(object sender, EventArgs e)
        {
            int value = 0;
            int.TryParse((sender as NumericUpDown).Value.ToString(), out value);
            server.joinLimit = value;
        }

        private void ServerUpdate_Changed(object sender, EventArgs e)
        {
            int value = 5;
            int.TryParse((sender as NumericUpDown).Value.ToString(), out value);
            server.updateTime = value;
        }

        private void TimeTick_Changed(object sender, EventArgs e)
        {
            int value = 30;
            int.TryParse((sender as NumericUpDown).Value.ToString(), out value);
            server.tickTime = value;
        }

        private void ProxyTB_TextChanged(object sender, EventArgs e)
        {
            LogTB.SelectionStart = LogTB.Text.Length;
            LogTB.ScrollToCaret();
        }

        private void proxyCB_Checked(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                var p = ParseProxy(ProxyTB.Text);
                server.proxy = p;
                ea.proxy = p;                
                server.is_proxy = ea.is_proxy = true;
            }
            else
                server.is_proxy = ea.is_proxy = false;
        }

        public ProxyClient ParseProxy(string s)
        {
            string[] a = new string[5];
            a = s.Split('\n');
            switch (a[0])
            {
                case "http":
                    return new HttpProxyClient(a[3], int.Parse(a[4]), a[1], a[2]);
                case "https":
                    return new HttpProxyClient(a[3], int.Parse(a[4]), a[1], a[2]);
                case "socks4":
                    return new Socks4ProxyClient(a[3], int.Parse(a[4]), a[1]);
                case "socks5":
                    return new Socks5ProxyClient(a[3], int.Parse(a[4]), a[1], a[2]);
                default:
                    return new HttpProxyClient();
            }
        }

    }
}
