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

            Console.Write("User types ");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Send button

            string userName = textBox4.Text;

            if (textBox4.Text == null)
            {
                userName = "Anonym";
            }

            if (socket != null)
            {
                if (socket.Connected == true)
                {
                    
                    string sentMsg = textBox1.Text;

                    if (sentMsg != "")
                    {
                        Console.WriteLine(sentMsg);
                        // Encode the data string into a byte array.  
                        byte[] msg = Encoding.UTF8.GetBytes(userName + ": " + sentMsg + "");
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
                    }
                    
                }
                
                else
                {
                    Console.WriteLine("Socket isn't connected");
                }
            }
            else
            {
                Console.WriteLine("There exists no Socket to connect to... Try connecting to a socket first.");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Connect button
            
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
                    socket.Close();
                }
            }

            else
            {
                socket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
                
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
                    socket.Close();
                }
            }
           
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
