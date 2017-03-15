using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Collections.Generic;
using System.Globalization;

namespace ServerSnacka
{
    public class Server
    {
        public static string data = "";
        public static List<Socket>_clientSocketsList = new List<Socket>();
        //public static List<Thread> threads = new List<Thread>();
        
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
            Console.WriteLine("CLIENTS:");
            int temp = 1;
            foreach (Socket client in _clientSocketsList)
            {
                try
                {
                    Console.WriteLine("FOUND CLIENTS, SENDING...");
                    client.Send(msg);
                    temp += 1;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            Console.WriteLine("SENT!");
            
        }

        public static void ThreadWork(Socket handler)
        {
            // Data buffer for incoming data.  
            byte[] bytes = new Byte[1024];
            Socket socket = handler;
           _clientSocketsList.Add(socket);

            while (true)
            {
                if (socket.Connected == true)
                {
                    data = "";

                    // An incoming connection needs to be processed.  
                    while (true)
                    {
                        int bytesRec = 0;
                        bytes = new byte[1024];

                        try
                        {
                            bytesRec = socket.Receive(bytes);
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

                    // Show the data on the console.
                    Console.WriteLine("Text received : {0}", data);

                    // Echo the data back to the client.  
                    if (data != "")
                    {
                        try
                        {
                            byte[] msg = Encoding.UTF8.GetBytes(data);
                            data = "";
                            Console.WriteLine("Trying to respond with message to client!");
                            
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

            // Establish the local endpoint for the socket.  
            // Dns.GetHostName returns the name of the   
            // host running the application.  
            //IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
           // IPAddress ipAddress = ipHostInfo.AddressList[0];
            //IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
            IPEndPoint localEndPoint = CreateIPEndPoint("192.168.43.191:11000");

            // Create a TCP/IP socket.  
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and   
            // listen for incoming connections.  
            
            listener.Bind(localEndPoint);
            listener.Listen(10);

            try
            {
                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    // Program is suspended while waiting for an incoming connection.  
                    Socket handler = listener.Accept();
                    Thread thread = new Thread(() => ThreadWork(handler));
                    thread.Start();
                    Console.WriteLine("Client Connected");
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("Error in trying to connect or create thread in line 164, Exception e");
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }
        
    }

}
