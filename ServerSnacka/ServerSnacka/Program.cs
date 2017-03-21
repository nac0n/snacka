using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace ServerSnacka
{
    public class Server
    {
        
        public static string data = "";
        public static List<Socket>_clientSocketsList = new List<Socket>();
        
        public static int Main(String[] args)
        {
            StartListening();
            return 0;
        }
        
        public static void CheckAndFixAvailableClients()
        {

            for(int i = 0; i < _clientSocketsList.Count; i++)
            {
                if(_clientSocketsList[i].Connected == false)
                {
                    _clientSocketsList.RemoveAt(i);
                }
            }
        }

        public static void SendToAllClients(byte[] msg)
        {
            CheckAndFixAvailableClients();
            int temp = 1;

            Console.WriteLine("TRYING TO FIND CLIENTS...");
            foreach (Socket client in _clientSocketsList)
            {
                try
                {
                    Console.WriteLine("FOUND CLIENT, SENDING MESSAGE...");
                    client.Send(msg);
                    temp += 1;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            Console.WriteLine("DONE SENDING!");
            
        }

        public static void SendToOneClient(byte[] msg, Socket socket)
        {
            int temp = 1;
            try
            {
                Console.WriteLine("TRYING TO SEND MESSAGE...");
                socket.Send(msg);
                temp += 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("DONE SENDING TO ONE CLIENT!");
        }

        public static void ThreadWork(Socket handler)
        {
            // Data buffer for incoming data.  
            byte[] bytes = new Byte[1024];
            Socket socket = handler;
           _clientSocketsList.Add(socket);

            while (true)
            {
                Console.WriteLine("WHILE 1");
                if (socket.Connected == true)
                {
                    data = "";

                    // An incoming connection needs to be processed.  
                    while (true)
                    {
                        Console.WriteLine("WHILE 2");
                        int bytesRec = 0;
                        bytes = new byte[1024];

                        try
                        {
                            bytesRec = socket.Receive(bytes);
                            Console.WriteLine("Received data");
                        }

                        catch (SocketException se)
                        {
                            Console.WriteLine("No response was given to user");
                            Console.WriteLine(se.Message);
                            socket.Shutdown(SocketShutdown.Both);
                            socket.Close();

                            try
                            {
                                Thread.CurrentThread.Abort();
                            }
                            catch (ThreadAbortException tae)
                            {
                                Console.WriteLine("Can't abort thread");
                                Console.WriteLine(tae.Message);
                            }
                            break;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Exception e in row 78, Recieving connection failed");
                            Console.WriteLine(e.Message);
                            break;
                        }

                        data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                        if (data.IndexOf("") > -1)
                        {
                            break;
                        }
                    }

                    Console.WriteLine("END OF WHILE 2");

                    // Show the data on the console.
                    Console.WriteLine("Text received : {0}", data);

                    // Dekryptera str
                    //string[] temp = data.Split(':');
                    string[] temp = DeCryptMessage(data).Split(':');

                    if (temp[0] == "PASSCHECK")
                    {
                        //Check password and return a "true" message to client in another method
                        string hash = "f0xle@rn";
                        string username = temp[1];
                        string password = temp[2];

                        var path = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.Parent.FullName;
                        Console.WriteLine("****" + path);
                        path = path + @"\db\snackaDb.mdf";

                        byte[] data = CryptMessage(password);

                        password = Encoding.UTF8.GetString(data, 0, data.Length);
                        Console.WriteLine("Password: " + password);
                        //C:\Users\Jessica\snacka\db\snackaDb.mdf

                        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + path + ";Integrated Security=True;Connect Timeout=30");
                        string query = "SELECT * from login Where username = '" + username + "' and password = '" + password + "'";
                        Console.WriteLine(username + " " + password);
                        SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                        DataTable dtbl = new DataTable();
                        sda.Fill(dtbl);

                        string complSentMsg;
                        
                        // Encode the data string into a byte array.  
                        //byte[] msg = Encoding.UTF8.GetBytes(complSentMsg);
                        byte[] tempmsg;

                        Console.WriteLine(dtbl.Rows.Count);

                        if (dtbl.Rows.Count == 1)
                        {
                            tempmsg = CryptMessage("PASSRESP:true");
                        }
                        else
                        {
                            tempmsg = CryptMessage("PASSRESP:false");
                        }
                        Thread t = new Thread(() => SendToOneClient(tempmsg, socket));
                        t.Start();

                        Console.WriteLine("SENT PASSWORDCHECK TO CLIENT");
                        break;
                    }
                    else
                    {
                        // Echo the data back to the client.  
                        if (data != "")
                        {

                            try
                            {
                                byte[] msg = Encoding.UTF8.GetBytes(data);
                                data = "";
                                Console.WriteLine("Trying to respond with message to clients!");

                                //socket.Send(msg);
                                Thread th = new Thread(() => SendToAllClients(msg));
                                th.Start();


                            }
                            catch (ObjectDisposedException ode)
                            {
                                Console.WriteLine("Server, Line 118, Catch ObjectDisposedException");
                                Console.WriteLine(ode.Message);
                                //Thread.CurrentThread.Abort();
                            }
                            catch (SocketException se)
                            {
                                Console.WriteLine("Server, Line 124, Catch SocketException");
                                Console.WriteLine(se.Message);
                                //Thread.CurrentThread.Abort();
                            }

                        }
                        else
                        {
                            Console.WriteLine("Data was empty, Line 132");
                        }
                    }
                   

                    //handler.Shutdown(SocketShutdown.Both);
                    //handler.Close();

                }

                else
                {
                    try
                    {
                        socket = handler;
                    }
                    catch (ObjectDisposedException ode)
                    {
                        Console.WriteLine("Socket closed and gone...");
                    }

                }
            }
            Console.WriteLine("End WHILE 1");
        }
        private static void SendMessage()
        {

        }

        private static byte[] CryptMessage(string msg)
        {
            string hash = "f0xle@rn";
            byte[] tempmsg;
            string complSentMsg = msg;

            byte[] temp1 = UTF8Encoding.UTF8.GetBytes(complSentMsg);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider triDes = new TripleDESCryptoServiceProvider()
                { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = triDes.CreateEncryptor();
                    byte[] result = transform.TransformFinalBlock(temp1, 0, temp1.Length);
                    complSentMsg = Convert.ToBase64String(result, 0, result.Length);
                }
            }
            tempmsg = Encoding.UTF8.GetBytes(complSentMsg);

            return tempmsg;
        }

        private static string DeCryptMessage(string data)
        {
            byte[] data2 = Convert.FromBase64String(data);

            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                string hash = "f0xle@rn";
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider triDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = triDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data2, 0, data2.Length);
                    data = UTF8Encoding.UTF8.GetString(results);
                }
            }

            return data;
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

        public static void StartListening()
        {
            Console.WriteLine("INITIATED LISTEN");
            // Establish the local endpoint for the socket.  
            // Dns.GetHostName returns the name of the   
            // host running the application.  
            //IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
           // IPAddress ipAddress = ipHostInfo.AddressList[0];
            //IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
            IPEndPoint localEndPoint = CreateIPEndPoint("192.168.153.113:11000");

            // Create a TCP/IP socket.  
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and   
            // listen for incoming connections.  
            
            listener.Bind(localEndPoint);
            Console.WriteLine("Got past bind");
            listener.Listen(10);

            Console.WriteLine("Got past bind and listen");
            try
            {
                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    // Program is suspended while waiting for an incoming connection.  
                    Socket handler = listener.Accept();
                    Console.WriteLine("Accepted listener...");
                    Thread thread = new Thread(() => ThreadWork(handler));
                    thread.Start();
                    Console.WriteLine("Client Connected");
                }
                Console.WriteLine("Exited WHILE in FIRST METHOD row 347");
            }

            catch (Exception e)
            {
                Console.WriteLine("Error in trying to connect or create thread in line 164, Exception e");
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }
        
    }

}
