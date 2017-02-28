using System;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Chatclient
{
    public partial class Form1 : Form
    {
        //Henrik
        Socket socket;

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

            Console.Write("User types");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Send button
            //Henrik
            if (socket != null)
            {
                //Henrik
                if (socket.Connected == true)
                {
                    Console.WriteLine("Sent Message!");
                    string sentMsg = textBox1.Text;
                    Console.WriteLine(sentMsg);
                    // Encode the data string into a byte array.  
                    byte[] msg = Encoding.ASCII.GetBytes(sentMsg + "<EOF>");
                    byte[] bytes = new byte[1024];

                    // Send the data through the socket.  
                    int bytesSent = socket.Send(msg);

                    // Receive the response from the remote device.  
                    int bytesRec = socket.Receive(bytes);
                    string str = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    Console.WriteLine("Echoed test = {0}", str);

                    listBox1.Items.Add(str);
                    textBox1.Text = "";
                }

                //Henrik
                else
                {
                    //test
                    Console.WriteLine("Socket isn't connected");
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Connect button
            //Henrik
            if (socket != null)
            {
                //Henrik
                if (socket.Connected == false)
                {
                    IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                    IPAddress ipAddress = ipHostInfo.AddressList[0];
                    IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                    socket.Connect(remoteEP);
                }

                //Henrik
                else
                {
                    // Release the socket.  
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
            }

            //Henrik
            else
            {
                //Henrik
                socket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
                
                //Henrik
                if (socket.Connected == false)
                {
                    IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                    IPAddress ipAddress = ipHostInfo.AddressList[0];
                    IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                    socket.Connect(remoteEP);
                }

                //Henrik
                else
                {
                    // Release the socket.  
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
            }
           
        }

    }
}
