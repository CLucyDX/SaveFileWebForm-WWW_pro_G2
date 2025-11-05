using System.Net.Sockets;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text;
using System;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.DirectoryServices.ActiveDirectory;
using System.Windows.Forms;
using System.Threading;
using System.Net.Http;
using System.Diagnostics;

namespace Web_Servers
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public Dictionary<string, string> ipDomainMap = new Dictionary<string, string>();
        public Dictionary<string, int> Status = new Dictionary<string, int>();
        public Dictionary<string, Socket> sockets = new Dictionary<string, Socket>();
        public Dictionary<string, TcpListener> Listeners = new Dictionary<string, TcpListener>();
        private int socket_counter = 10;
        public void add_socket()
        {
            ipDomainMap.Add("127.1.0.0", "xqwonline.com");
            Status.Add("127.1.0.0", 1);
            ipDomainMap.Add("127.1.0.1", "xqwworld.com");
            Status.Add("127.1.0.1", 1);
            ipDomainMap.Add("127.1.0.2", "xqwblog.com");
            Status.Add("127.1.0.2", 1);
            ipDomainMap.Add("127.1.0.3", "xqwgroup.com");
            Status.Add("127.1.0.3", 1);
            ipDomainMap.Add("127.1.0.4", "xqwdesign.com");
            Status.Add("127.1.0.4", 1);
            ipDomainMap.Add("127.1.0.5", "xqwmedia.com");
            Status.Add("127.1.0.5", 1);
            ipDomainMap.Add("127.1.0.6", "xqwbook.com");
            Status.Add("127.1.0.6", 1);
            ipDomainMap.Add("127.1.0.7", "xqwnet.com");
            Status.Add("127.1.0.7", 1);
            ipDomainMap.Add("127.1.0.8", "xqwlink.com");
            Status.Add("127.1.0.8", 1);
            ipDomainMap.Add("127.1.0.9", "xqwclub.com");// set the original IP address and domain name
            Status.Add("127.1.0.9", 1);
        }
        DateTime GetCurrentTime()
        {
            DateTime time = new DateTime();
            time = DateTime.Now;
            return time;
        }
        public void auto_run()
        {
            int i = 1, flag = 1;
            add_socket();
            foreach (var pair in ipDomainMap)
            {
                TcpListener myListener = new TcpListener(IPAddress.Parse(pair.Key), 8080);
                myListener.Start();
                Listeners.Add(pair.Key, myListener);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(string.Format("Web Server: {0} Running...\n", pair.Key));
                Thread thread = new Thread(new ParameterizedThreadStart(StartListen));
                thread.Start(myListener);
            }
            Console.ForegroundColor = ConsoleColor.White;
            status_display();
        }

        public void SendHeader(string HttpVersion, string Header, int LenByte, string status, ref Socket mySocket)
        {
            String buffer = "";

            if (Header.Length == 0)
            {
                Header = "text/html";
            }

            DateTime now = DateTime.Now;
            string format = "Sun, 10 DEC 2023 10:03:15 GMT";
            string timeString = now.ToString(format);

            buffer = buffer + HttpVersion + status + "\r\n";
            buffer = buffer + "Server: cx1193719-b\r\n";
            buffer = buffer + "Content-Type: " + Header + "\r\n";
            buffer = buffer + "Accept-Ranges: bytes\r\n";
            buffer = buffer + "Cache-Control: max-age=3600, must-revalidate\r\n";
            /*buffer = buffer + "Last-Modified: Fri,8 DEC 2023 21:00:02 GMT\r\n";
            buffer = buffer + "If-Modified-Since: Fri,8 DEC 2023 21:00:02 GMT\r\n";
            buffer = buffer + "Etag: 666\r\n";*/
            buffer = buffer + "Content-Length: " + LenByte + "\r\n\r\n";
            Byte[] bSendData = Encoding.ASCII.GetBytes(buffer);
            if (status == " 200 OK")
                Console.WriteLine(string.Format("{0}\n", buffer)); // response line
            else
                Console.WriteLine(string.Format("{0} {1}\t\n", HttpVersion, status)); // response line

            SendToBrowser(bSendData, ref mySocket);

            //Console.WriteLine("Total Bytes : " + LenByte.ToString());

        }

        public void SendToBrowser(String sData, ref Socket mySocket)
        {
            SendToBrowser(Encoding.ASCII.GetBytes(sData), ref mySocket);
        }

        public void SendToBrowser(Byte[] bSendData, ref Socket mySocket)
        {
            int numBytes = 0;
            try
            {
                if (mySocket.Connected)
                {
                    numBytes = mySocket.Send(bSendData, bSendData.Length, 0);
                }
                else
                    Console.WriteLine("Connect faile in socket....");
            }
            catch (Exception e)
            {
                Console.WriteLine("error occur : {0} ", e);
            }
        }

        public void StartListen(object obj)
        {
            int start_position = 0;
            String request;
            String sDirName;
            String request_file_name;
            String ErrorMessage;
            String file_directory;
            String Type;
            string ip = "";
            String sMyWebServerRoot = "";
            String FilePath = "";
            String sFormattedMessage = "";
            String response = "";

            while (true)
            {
                //listen new connect
                Socket mySocket;
                try
                {
                    TcpListener li = obj as TcpListener;
                    mySocket = li.AcceptSocket();
                    foreach (var pair in Listeners)
                    {
                        if (pair.Value == li)
                        {
                            ip = pair.Key;
                        }
                    }
                    sMyWebServerRoot = AppDomain.CurrentDomain.BaseDirectory + ip; //Setting a virtual directory that store files
                }
                catch
                {
                    return;
                }
                if (mySocket.Connected)
                {
                    Byte[] receive_data = new Byte[1024];
                    int i = mySocket.Receive(receive_data, receive_data.Length, 0);

                    string data_str = Encoding.ASCII.GetString(receive_data);// encode to string

                    //只处理"get"请求类型
                    if (data_str.Substring(0, 3) != "GET")
                    {
                        //Console.WriteLine(string.Format("{0}", data_str.Substring(0, 3)));
                        mySocket.Close();
                        return;
                    }

                    start_position = data_str.IndexOf("HTTP", 1); //position of HTTP

                    string sHttpVersion = data_str.Substring(start_position, 8);

                    request = data_str.Substring(0, start_position - 1);//get file name and function(GET,POST...)
                    request.Replace("//", "/");

                    request_file_name = request.Split('/')[1];
                    string file_type = request.Split('/')[1].Split('.')[1];
                    //Console.WriteLine(request_file_name);

                    file_directory = sMyWebServerRoot; // directroy of files
                    //Console.WriteLine("请求文件目录 : " + sLocalDir);

                    if (file_directory.Length == 0)
                    {
                        ErrorMessage = string.Format("<html><head>\r\n<title>File Directory Not Found</title>\r\n</head><body>\r\n" +
                            "<h1>File Directory Not Found</h1>\r\n<p>The File Directory is not on this serer</p>\r\n</body></html>");
                        SendHeader(sHttpVersion, "", ErrorMessage.Length, " 404 Not Found", ref mySocket);
                        SendToBrowser(ErrorMessage, ref mySocket);
                        mySocket.Close();
                        continue;
                    }


                    if (request_file_name.Length == 0)
                    {
                        request_file_name = "index.html";
                    }

                    Type = "";
                    switch (file_type)
                    {
                        case "html":
                            Type = "text/html";
                            break;
                        case "png":
                            Type = "image/png";
                            break;
                        case "jpg":
                            Type = "image/jpf";
                            break;
                        case "jpeg":
                            Type = "image/jpeg";
                            break;
                        case "gif":
                            Type = "image/gif";
                            break;
                        case "mp3":
                            Type = "audio/mp3";
                            break;
                        case "mp4":
                            Type = "video/mp4";
                            break;
                        case "txt":
                            Type = "text/plain";
                            break;
                        case "pdf":
                            Type = "application/pdf";
                            break;
                        default:
                            Type = "application/octet-stream";
                            break;
                    }

                    FilePath = file_directory + "/" + request_file_name;

                    if (!File.Exists(FilePath))
                    {
                        ErrorMessage = string.Format("<html><head>\r\n<title>404 Not Found</title>\r\n</head><body>\r\n" +
                            "<h1>404 Not Found</h1>\r\n<p>The requested URL /{0} was not found on this server.</p>\r\n</body></html>", request_file_name);
                        SendHeader(sHttpVersion, "", ErrorMessage.Length, " 404 Not Found", ref mySocket);
                        SendToBrowser(ErrorMessage, ref mySocket);
                        //Console.WriteLine(sFormattedMessage);
                    }
                    else
                    {
                        int Byte = 0;
                        response = "";
                        FileStream f1 = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                        BinaryReader reader = new BinaryReader(f1);
                        byte[] bytes = new byte[f1.Length];
                        int read_num;
                        while ((read_num = reader.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            response = response + Encoding.ASCII.GetString(bytes, 0, read_num);
                            Byte = Byte + read_num;
                        }
                        reader.Close();
                        f1.Close();
                        Console.WriteLine(string.Format("Server IP: {0}", ip));
                        SendHeader(sHttpVersion, Type, Byte, " 200 OK", ref mySocket);
                        if (Type == "text/html")
                            Console.WriteLine(response);
                        SendToBrowser(bytes, ref mySocket);
                    }
                    mySocket.Close();
                }
            }
        }
        public void change_combobox(string address)
        {
            ip_box.Items.Add(address);
        }
        public void status_display()
        {
            var s1 = "Running Normally";
            var s2 = "Running Pause";
            foreach (var pair in ipDomainMap)
            {
                if (Status[pair.Key] == 1)
                    this.AppendText(string.Format("Server IP: {0}        Domain name: {1}{2,30}", pair.Key, pair.Value, s1));
                else
                    this.AppendText(string.Format("Server IP: {0}        Domain name: {1}{2,30}", pair.Key, pair.Value, s2));
            }
        }

        public void AppendText(string txt)
        {
            if (IP_status.InvokeRequired)//determine the thread now whether is the thread that creat the ui
            {
                if (IP_status.Text == "")//it is not
                {
                    IP_status.BeginInvoke(new Action<string>(s =>//avoid accorss thread is invalid
                    {
                        this.IP_status.Text = string.Format("{0}", txt);
                    }), txt);
                }
                else
                {
                    IP_status.BeginInvoke(new Action<string>(s =>
                    {
                        this.IP_status.Text = string.Format("{0}\r\n{1}", IP_status.Text, txt);
                    }), txt);
                }

            }//...
            else
            {
                if (IP_status.Text == "")
                    this.IP_status.Text = string.Format("{0}", txt);
                else
                    this.IP_status.Text = string.Format("{0}\r\n{1}", IP_status.Text, txt);
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void start_button_Click(object sender, EventArgs e)
        {
            if (ip_box.SelectedIndex == -1)
                MessageBox.Show("Warnning! You do not select any IP address.");
            else if (Status[ip_box.Text] != 0)
            {
                MessageBox.Show("The IP address is running normally now. Please do not run repeatedly.");
            }
            else
            {
                TcpListener myListener = new TcpListener(IPAddress.Parse(ip_box.Text), 8080);
                myListener.Start();
                Listeners.Remove(ip_box.Text);
                Listeners.Add(ip_box.Text, myListener);
                Thread thread = new Thread(new ParameterizedThreadStart(StartListen));
                thread.Start(myListener);
                MessageBox.Show(string.Format("Server {0} run successfully.", ip_box.Text));
                Status[ip_box.Text] = 1;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(string.Format("Web Server: {0} Running...\n", ip_box.Text));
                Console.ForegroundColor = ConsoleColor.White;
                IP_status.Text = "";
                status_display();
            }
        }

        private void stop_button_Click(object sender, EventArgs e)
        {
            if (ip_box.SelectedIndex == -1)
                MessageBox.Show("Warnning! You do not select any IP address.");
            else if (Status[ip_box.Text] == 0)
            {
                MessageBox.Show("The IP address is running pause now. Please do not pause repeatedly.");
            }
            else
            {
                Listeners[ip_box.Text].Stop();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(string.Format("Web Server: {0} Running Pause\n", ip_box.Text));
                Console.ForegroundColor = ConsoleColor.White;
                MessageBox.Show(string.Format("Server {0} pause successfully.", ip_box.Text));
                Status[ip_box.Text] = 0;
                IP_status.Text = "";
                status_display();
            }
        }

        private void create_button_Click(object sender, EventArgs e)
        {
            int flag = 0;
            if (ip_create.Text == "" || domain_text.Text == "")
                MessageBox.Show("Warnning! You must input the IP address and domain name you want to create.");
            else if (ipDomainMap.ContainsKey(ip_create.Text) || ip_create.Text == "127.18.0.1")
                MessageBox.Show("Warnning! The IP address has already existed.");
            else
            {
                string txt = domain_text.Text.ToLower();
                string p = @"^([a-zA-Z0-9]([a-zA-Z0-9-_]{0,61}[a-zA-Z0-9])?\.)+[a-zA-Z]{2,11}$";
                Regex regex = new Regex(p);
                foreach (var pair in ipDomainMap)
                {
                    if (pair.Value == txt)
                        flag = 1;
                }
                if (flag == 1)
                    MessageBox.Show("Warnning! The domain name has already existed.");
                else if (!regex.IsMatch(txt))
                    MessageBox.Show("Warnning! The structure of domain name is not ture.");
                else
                {
                    TcpListener myListener = new TcpListener(IPAddress.Parse(ip_create.Text), 8080);
                    myListener.Start();
                    Listeners.Add(ip_create.Text, myListener);
                    ipDomainMap.Add(ip_create.Text, domain_text.Text);
                    string path = ip_create.Text;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    Thread thread = new Thread(new ParameterizedThreadStart(StartListen));
                    thread.Start(myListener);
                    MessageBox.Show(string.Format("Server {0} create and run successfully.", ip_create.Text));
                    Status.Add(ip_create.Text, 1);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(string.Format("Web Server: {0} Running...\n", ip_create.Text));
                    Console.ForegroundColor = ConsoleColor.White;
                    IP_status.Text = "";
                    status_display();
                }
            }
        }
        string filePath = "";
        string filetype = "";
        string filename = "";
        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.txtSoundPath.Text = ofd.FileName.ToString();
            }
            filePath = this.txtSoundPath.Text;
            filetype = System.IO.Path.GetExtension(ofd.FileName);
            filename = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName) + filetype;
        }

        private void btnUpSound_Click(object sender, EventArgs e)
        {
            try
            {
                string address = "https://localhost:44370/WebService/SaveFileWebForm.aspx";
                int count = UpSound_Request(address, filePath, filename);
                if (count >= 0)
                {
                    MessageBox.Show("Upload file successfully!");
                }
                else
                {
                    MessageBox.Show("Upload file failed!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.GetBaseException());
            }
        }
        public int UpSound_Request(string address, string fileNamePath, string saveName)
        {
            int returnValue = 0;
            FileStream fs = new FileStream(fileNamePath, FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(fs);
            string strBoundary = "----------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + strBoundary + "\r\n");
            StringBuilder build = new StringBuilder();
            build.Append("--");
            build.Append(strBoundary);
            build.Append("\r\n");
            build.Append("Content-Disposition: form-data; name=\"");
            build.Append("file");
            build.Append("\"; filename=\"");
            build.Append(saveName);
            build.Append("\";");
            build.Append("\r\n");
            build.Append("Content-Type: ");
            build.Append("application/octet-stream");
            build.Append("\r\n");
            build.Append("\r\n");
            string strPostHeader = build.ToString();
            Trace.WriteLine(strPostHeader);
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(strPostHeader);
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(address));
            httpReq.Method = "POST";
            httpReq.AllowWriteStreamBuffering = false;
            httpReq.Timeout = 300000;
            httpReq.ContentType = "multipart/form-data; boundary=" + strBoundary;
            long length = fs.Length + postHeaderBytes.Length + boundaryBytes.Length;
            long fileLength = fs.Length;
            httpReq.ContentLength = length;
            try
            {
                int bufferLength = 4096;
                byte[] buffer = new byte[bufferLength];
                long offset = 0;
                DateTime startTime = DateTime.Now;
                int size = r.Read(buffer, 0, bufferLength);
                Stream postStream = httpReq.GetRequestStream();
                postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                while (size > 0)
                {
                    postStream.Write(buffer, 0, size);
                    Application.DoEvents();
                    size = r.Read(buffer, 0, bufferLength);
                }
                //添加尾部的时间戳   
                postStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                postStream.Close();
                //获取服务器端的响应   
                WebResponse webRespon = httpReq.GetResponse();
                Stream s = webRespon.GetResponseStream();
                //读取服务器端返回的消息  
                StreamReader sr = new StreamReader(s);
                String sReturnString = sr.ReadLine();
                s.Close();
                sr.Close();
                if (sReturnString == "Success")
                {
                    returnValue = 1;
                }
                else if (sReturnString == "Error")
                {
                    returnValue = 0;
                }
            }
            catch
            {
                returnValue = 0;
            }
            finally
            {
                fs.Close();
                r.Close();
            }
            return returnValue;
        }
    }

}
