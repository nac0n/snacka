using System;
using System.Globalization;
using System.Net;
//using System.Data;
//using System.Data.SqlClient;
//using System.IO;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Chatclient
{
    public partial class LoginForm : Form
    {
        string hash = "f0xle@rn";
        Thread SendPassWordThread;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public string Hash(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            byte[] hashBytes;
            using (var algorithm = new System.Security.Cryptography.SHA512Managed())
            {
                hashBytes = algorithm.ComputeHash(bytes);
            }
            return Convert.ToBase64String(hashBytes);
        }
     
        //Klickar på att logga in
        private void Login_Click(object sender, EventArgs e)
        {
            string password = textBox2.Text.Trim();
            string username = textBox1.Text.Trim();
            string hashed = Hash(password);

            if(PassWordIsCorrect(username, password))
            {
                Form1 chat = new Form1();
                this.Hide();
                Program.userName = textBox1.Text.Trim();
                chat.Show();
            }
            else
            {
                MessageBox.Show("Wrong Password, try again");
            }

            //var path = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.Parent.FullName;
            //Console.WriteLine("****"+path);
            //path = path + @"\db\snackaDb.mdf";

            ////C:\Users\Jessica\snacka\db\snackaDb.mdf

            //SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="+path+";Integrated Security=True;Connect Timeout=30");
            //string query = "SELECT * from login Where username = '" + textBox1.Text.Trim() + "' and password = '"+ hashed + "'";
            //SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            //DataTable dtbl = new DataTable();
            //sda.Fill(dtbl);

            //if(dtbl.Rows.Count == 1)
            //{
            //    Form1 chat = new Form1();
            //    this.Hide();
            //    Program.userName = textBox1.Text.Trim();
            //    chat.Show();
            //}
            //else
            //{
            //    MessageBox.Show("Wrong Password, try again");
            //}

        }

        private bool PassWordIsCorrect(string username, string password)
        {
            
            Socket socket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint remoteEP = CreateIPEndPoint("192.168.56.1:11000");

            try
            {
                socket.Connect(remoteEP);

                bool passIsCorrect;
                passIsCorrect = ConfirmPassViaServer(socket, username ,password);
                if(passIsCorrect == true)
                {
                    socket.Close();
                    socket = null;
                    return true;
                   
                }
                else
                {
                    socket.Close();
                    socket = null;
                    return false;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Could not connect");
                Console.WriteLine(e.Message);
            }

            return false;
        }

        private bool ConfirmPassViaServer(Socket socket, string username, string password)
        {
            try
            {
                string complSentMsg = "PASSCHECK:" + username + ":" + password;
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
                byte[] msg = Encoding.UTF8.GetBytes(complSentMsg);
                socket.Send(msg);

                byte[] bytes = new byte[1024];
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
                        string[] s = str.Split(':');
                        Console.WriteLine("s is = " + s[1]);
                        bool tempBool = Convert.ToBoolean(s[1]);

                        if(tempBool)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        
        public static IPEndPoint CreateIPEndPoint(string endPoint)
        {
            string[] ep = endPoint.Split(':');

            if (ep.Length != 2)
                throw new FormatException("Invalid endpoint format");

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
            // Create a TCP/IP  socket.  


        }

        private void GetHashCode(string password)
        {
            throw new NotImplementedException();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Login.PerformClick();
            }
        }
    }

   
}
