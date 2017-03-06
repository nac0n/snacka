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
            string userName = textBox4.Text;

            if (textBox4.Text == null)
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

                    int bytesSent = socket.Send(msg);
                    Console.WriteLine("Sent Message!");

                    // Receive the response from the remote device.  
                    int bytesRec = socket.Receive(bytes);
                    string str = Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    Console.WriteLine("Echoed test = {0}", str);

                    this.Invoke(new MethodInvoker(delegate ()
                    {

                        listBox1.Items.Add(str);
                        //Thread.CurrentThread.Abort();

                    }));

                }
            }
            catch (SocketException se)
            {
                Console.WriteLine("Error in try method around line 70, SocketException");
                Console.WriteLine(se.Message);
            }
            catch (NullReferenceException nre)
            {
                Console.WriteLine("Error in try method around line 70, NullreferenceException");
                Console.WriteLine(nre.Message);
                socket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
            }
        }

        private void ConnectionUser()
        {
            if (socket != null)
            {
                if (socket.Connected == false)
                {
                    IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                    IPAddress ipAddress = ipHostInfo.AddressList[0];
                    IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                    socket.Connect(remoteEP);
                }

                else
                {
                    // Release the socket.  
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Disconnect(false);
                    socket.Close();
                    socket = null;
                    Thread.CurrentThread.Abort();
                }
            }

            else
            {
                try
                {
                    socket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
                }
                catch (Exception e) { Console.WriteLine(e.Message); };


                if (socket.Connected == false)
                {
                    IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                    IPAddress ipAddress = ipHostInfo.AddressList[0];
                    IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                    socket.Connect(remoteEP);
                }

                else
                {
                    // Release the socket.  
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Disconnect(false);
                    socket.Close();
                    socket = null;
                    Thread.CurrentThread.Abort();
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
    }
}
