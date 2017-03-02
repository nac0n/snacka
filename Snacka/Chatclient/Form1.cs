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
        protected Socket socket;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Create a TCP/IP  socket.  
            
            
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

            string sentMsg = textBox1.Text;
            Console.WriteLine(sentMsg);
            // Encode the data string into a byte array.  
            byte[] msg = Encoding.UTF8.GetBytes(sentMsg + "<EOF>");
            byte[] bytes = new byte[1024];

            // Send the data through the socket.  
            int bytesSent = socket.Send(msg);
            Console.WriteLine("Sent Message!");

            // Receive the response from the remote device.  
            int bytesRec = socket.Receive(bytes);
            string str = Encoding.UTF8.GetString(bytes, 0, bytesRec);
            Console.WriteLine("Echoed test = {0}", str);

            listBox1.Items.Add(str);
            textBox1.Text = "";

            // Send message 
            // get response
            // Disconnect socket
            // abort thread
        }

        private void ConnectionUser()
        {
            if (!SocketConnected())
            {
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);
                socket.Poll(5000, SelectMode.SelectWrite);
                socket.Connect(remoteEP);
            }

            else
            {
                // Release the socket.  
                socket.Shutdown(SocketShutdown.Both);
                socket.Disconnect(false);
                socket.Close();
                
                socket = null;
            }
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

    }
}
