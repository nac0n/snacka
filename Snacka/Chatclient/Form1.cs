using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.Globalization;
using System.Security.Cryptography;

namespace Chatclient
{
    public partial class Form1 : Form
    {
        //Kryptering hash
        string hash = "f0xle@rn";

        protected Thread SendThread;
        protected Thread ConnectThread;
        protected Socket socket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
        string userName;

        //static IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
        //static IPAddress ipAddress = ipHostInfo.AddressList[0];
        //public static string ip = "192.168.56.1";
        //public static long ipadress = Convert.ToInt64(ip);

        static IPEndPoint remoteEP = CreateIPEndPoint("192.168.153.113:11000"); /*new IPEndPoint(ipAddress, 11000);*/
        public static bool listeningToServer = false;

        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public static IPEndPoint CreateIPEndPoint(string endPoint)
        {
            string[] ep = endPoint.Split(':');
            if (ep.Length != 2) throw new FormatException("Invalid endpoint format");
            IPAddress ip;
            if (!IPAddress.TryParse(ep[0], out ip))
            {
                throw new FormatException("Invalid ip-adress");
            }
            int port;
            if (!int.TryParse(ep[1], NumberStyles.None, NumberFormatInfo.CurrentInfo, out port))
            {
                throw new FormatException("Invalid port");
            }
            return new IPEndPoint(ip, port);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //User's typebox
            Console.Write("User types ");
            listBox1.Text = "User";

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //Send button
            SendThread = new Thread(() => SendMessage());
            SendThread.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Connect button
            ConnectThread = new Thread(() => ConnectionUser());
            ConnectThread.Start();

        }

        public void ListenToServer(Socket socket)
        {
            while(listeningToServer == true)
            {
                byte[] bytes = new byte[1024];


                try
                {
                    if(socket.Connected == false)
                    {
                        listeningToServer = false;
                    }
                    
                    if(listeningToServer == true)
                    {
                        try
                        {
                            int bytesRec = socket.Receive(bytes);
                            string str = Encoding.UTF8.GetString(bytes, 0, bytesRec);
                            Console.WriteLine("Someone Wrote = {0}", str);

                            // Dekryptera str
                            byte[] data2 = Convert.FromBase64String(str);
                            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                            {
                                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                                using (TripleDESCryptoServiceProvider triDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                                {
                                    ICryptoTransform transform = triDes.CreateDecryptor();
                                    byte[] results = transform.TransformFinalBlock(data2, 0, data2.Length);
                                    str = UTF8Encoding.UTF8.GetString(results);
                                }
                            }

                            Invoke(new MethodInvoker(delegate ()
                            {
                                listBox1.Items.Add(str);
                                textBox1.Text = "";
                            }));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }

                catch(SocketException se)
                {
                    Console.WriteLine(se.Message);
                }
                
            }
            Console.WriteLine("Stopped listening to server...");
        }

        private void SendMessage()
        {
            userName = textBox4.Text;

            if (textBox4.Text == "")
            {
                userName = "Anonym";
            }

            string sentMsg = textBox1.Text;

            if(sentMsg != "")
            {
                Console.WriteLine(sentMsg);

                string complSentMsg = userName + ": " + sentMsg;

                //Krypterar compSentMsg
                //Console.WriteLine("Skriv en text");
                //string Text = Console.ReadLine();
                byte[] data1 = UTF8Encoding.UTF8.GetBytes(complSentMsg);
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                    using (TripleDESCryptoServiceProvider triDes = new TripleDESCryptoServiceProvider()
                    { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = triDes.CreateEncryptor();
                        byte[] result = transform.TransformFinalBlock(data1, 0, data1.Length);
                        complSentMsg = Convert.ToBase64String(result, 0, result.Length);
                    }
                }

                // Encode the data string into a byte array.  
                byte[] msg = Encoding.UTF8.GetBytes(complSentMsg);

          // Encode the data string into a byte array.  
          //byte[] msg = Encoding.UTF8.GetBytes(userName + ": " + sentMsg + "");
                

                // Send the data through the socket.  
                try
                {
                    if (socket.Connected == true)
                    {
                        int bytesSent = socket.Send(msg);
                        Console.WriteLine("Sent Message!");


                        // Receive the response from the remote device.  
                        byte[] bytes = new byte[1024];
                        int bytesRec = socket.Receive(bytes);
                        string str = Encoding.UTF8.GetString(bytes, 0, bytesRec);
                        Console.WriteLine("Echoed test = {0}", str);

                        // Dekryptera str
                        byte[] data2 = Convert.FromBase64String(str);
                        using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                        {
                            byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                            using (TripleDESCryptoServiceProvider triDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                            {
                                ICryptoTransform transform = triDes.CreateDecryptor();
                                byte[] results = transform.TransformFinalBlock(data2, 0, data2.Length);
                                str = UTF8Encoding.UTF8.GetString(results);
                            }
                        }

                        Invoke(new MethodInvoker(delegate ()
                        {
                            listBox1.Items.Add(str);
                            textBox1.Text = "";
                        }));
                        
                    }
                }
                catch (SocketException se)
                {
                    Console.WriteLine("Error in try method around line 165, SocketException");
                    Console.WriteLine(se.Message);
                }
                catch (NullReferenceException nre)
                {
                    Console.WriteLine("Error in try method around line 170, NullreferenceException");
                    Console.WriteLine(nre.Message);
                }
            }

        }

        private void ConnectionUser()
        {
            //Connect/Disconnect button event
            if (socket != null)
            {
                if (socket.Connected == false)
                {

                    //CONNECT
                    
                    socket = new Socket(AddressFamily.InterNetwork,
                            SocketType.Stream, ProtocolType.Tcp);
                    try
                    {
                        socket.Connect(remoteEP);
                        listeningToServer = true;
                        Thread t = new Thread(() => ListenToServer(socket));
                        t.Start();
                        
                       
                    }

                    catch (SocketException)
                    {
                        Console.WriteLine("No server found");
                    }

                    //button2.Text = "Disconnect";
                    //listBox1.Items.Add("You are now connected");
                }

                else
                {
                    //DISCONNECT

                    socket.Close();
                    listeningToServer = false;
                    socket = null;

                    //try
                    //{
                    //    button2.Text = "Connect";
                    //    listBox1.Items.Add("You are now disconnected");
                    //}
                    //catch (InvalidOperationException ioe)
                    //{
                    //    Console.WriteLine("InvalidOperationException ioe");
                    //    Console.WriteLine(ioe.Message);
                    //}
                    
                }
            }

            else
            {
                try
                {
                    socket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Couldn't create new socket");
                    Console.WriteLine(e.Message);
                };


                if (socket.Connected == false)
                {
                    try
                    {
                        socket.Connect(remoteEP);
                        listeningToServer = true;
                        Thread t = new Thread(() => ListenToServer(socket));
                        t.Start();
                    }

                    catch (SocketException)
                    {
                        Console.WriteLine("No server found");
                    }

                    //button2.Text = "Disconnect";
                    //listBox1.Items.Add("You are now connected");
                }

                else
                {
                    listeningToServer = false;

                    //button2.Text = "Connect";
                    //listBox1.Items.Add("You are now disconnected");
                }
            }
        }

        bool SocketConnected()
        {
            bool part1 = socket.Poll(1000, SelectMode.SelectRead);
            bool part2 = (socket.Available == 0);
            if (part1 && part2)
                return false;
            else
                return true;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                button1.PerformClick();
            }
        }
    }
}
