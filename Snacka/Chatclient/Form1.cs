using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace Chatclient
{
    public partial class Form1 : Form
    {
        protected Thread SendThread;
        protected Thread ConnectThread;
        protected Socket socket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
        String userName;
        static IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
        static IPAddress ipAddress = ipHostInfo.AddressList[0];
        static IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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

        private void SendMessage()
        {
            userName = textBox4.Text;

            if (textBox4.Text == "")
            {
                userName = "Anonym";
            }

            string sentMsg = textBox1.Text;
            Console.WriteLine(sentMsg);
            // Encode the data string into a byte array.  
            byte[] msg = Encoding.UTF8.GetBytes(userName + ": " + sentMsg + "<EOF>");
            byte[] bytes = new byte[1024];
            

            // Send the data through the socket.  
            try
            {
                if (socket.Connected == true)
                {
                    //socket = new Socket(AddressFamily.InterNetwork,
                    //    SocketType.Stream, ProtocolType.Tcp);
                    int bytesSent = socket.Send(msg);
                    Console.WriteLine("Sent Message!");

                    // Receive the response from the remote device.  
                    int bytesRec = socket.Receive(bytes);
                    string str = Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    Console.WriteLine("Echoed test = {0}", str);

                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        listBox1.Items.Add(str);
                    }));

                }
            }
            catch (SocketException se)
            {
                Console.WriteLine("Error in try method around line 103, SocketException");
                Console.WriteLine(se.Message);
            }
            catch (NullReferenceException nre)
            {
                Console.WriteLine("Error in try method around line 108, NullreferenceException");
                Console.WriteLine(nre.Message);
                //socket = new Socket(AddressFamily.InterNetwork,
                //    SocketType.Stream, ProtocolType.Tcp);
            }
        }

        private void ConnectionUser()
        {
            if (socket != null)
            {
                if (socket.Connected == false)
                {
                    // IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                    // IPAddress ipAddress = ipHostInfo.AddressList[0];
                    // IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                    try
                    {
                        socket.Connect(remoteEP);
                    }
                    catch (SocketException)
                    {
                        Console.WriteLine("No server found");
                    }
                    button2.Text = "Disconnect";
                    listBox1.Items.Add("You are now connected");


                }

                else
                {
                    // Release the socket.  
                    //socket.Shutdown(SocketShutdown.Both);
                    //socket.Disconnect(false);
                    //socket.Close();
                    socket = null;
                    //Thread.CurrentThread.Abort();
                    button2.Text = "Connect";
                    listBox1.Items.Add("You are now disconnected");
                }
            }

            else
            {
                //try
                //{
                    socket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
                //}
                //catch (Exception e) { Console.WriteLine(e.Message); };


                if (socket.Connected == false)
                {
                    //IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                    //IPAddress ipAddress = ipHostInfo.AddressList[0];
                    //IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                    try
                    {
                        socket.Connect(remoteEP);
                    }

                    catch (SocketException)
                    {
                        Console.WriteLine("No server found");
                    }

                    button2.Text = "Disconnect";
                    listBox1.Items.Add("You are now connected");

                }

                else
                {
                    // Release the socket.  
                    //socket.Shutdown(SocketShutdown.Both);
                    //socket.Disconnect(false);
                    //socket.Close();
                    //socket = null;
                    //Thread.CurrentThread.Abort();
                    button2.Text = "Connect";
                    listBox1.Items.Add("You are now disconnected");
                }
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////

            //if (SocketConnected())
            //{
            //    Console.WriteLine("Socket was connected, disconnecting now...");

            //    socket.Shutdown(SocketShutdown.Both);
            //    socket.Disconnect(false);
            //    //IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            //    //IPAddress ipAddress = ipHostInfo.AddressList[0];
            //    //IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);
            //    //socket.Poll(5000, SelectMode.SelectWrite);
            //    //socket.Connect(remoteEP);
            //}

            //else
            //{
            //    socket = new Socket(AddressFamily.InterNetwork,
            //        SocketType.Stream, ProtocolType.Tcp);
            //    Console.WriteLine("Socket was disconnected, connecting now...");
            //    IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            //    IPAddress ipAddress = ipHostInfo.AddressList[0];
            //    IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);
            //    socket.Poll(5000, SelectMode.SelectWrite);
            //    socket.Connect(remoteEP);

            //    // Release the socket.  
            //    //socket.Shutdown(SocketShutdown.Both);
            //    //socket.Disconnect(false);
            //    //socket.Close();

            //    //socket = null;
            //}
            // Send message 
            // get response
            // Disconnect socket
            // abort thread
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
