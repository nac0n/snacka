using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
//using System.Linq;
//using System.Collections.Generic;

namespace ServerSnacka
{
    public class Server
    {
        // Incoming data from the client.  
        public static string data = null;
        //public static List<Thread> threads = new List<Thread>();
        
        public static int Main(String[] args)
        {
            StartListening();
            return 0;
        }

        public static void BroadcastToAllClients()
        {

        }

        public static void ThreadWork(Socket handler)
        {
            // Data buffer for incoming data.  
            byte[] bytes = new Byte[1024];
            Socket socket = handler;

            while (true)
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);
                //Flyttat upp denna--v
                Console.WriteLine("Waiting for connection...");

                Socket handler = listener.Accept();
                Console.WriteLine("You are successfully connected");
               


                // Start listening for connections.  
                while (true)
                {
                    // Program is suspended while waiting for an incoming connection.  
                    data = null;

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

                            Console.WriteLine("No request from user");
                            socket.Shutdown(SocketShutdown.Both);
                            socket.Close();
                            Thread.CurrentThread.Abort();
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
                    if (data != null)
                    {
                        try
                        {
                            byte[] msg = Encoding.UTF8.GetBytes(data);
                            data = "";
                            Console.WriteLine("Trying to respond with message to client!");
                            socket.Send(msg);
                            Console.WriteLine("SENT!");
                        }
                        catch (ObjectDisposedException ode)
                        {
                            Console.WriteLine("Server, Line 59, Catch ObjectDisposedException");
                            Console.WriteLine(ode.Message);
                            //Thread.CurrentThread.Abort();
                        }
                        catch (SocketException se)
                        {
                            Console.WriteLine("Server, Line 59, Catch SocketException");
                            Console.WriteLine(se.Message);
                            //Thread.CurrentThread.Abort();
                        }

                    }
                    else
                    {
                        data = "Empty String";
                    }

                    //handler.Shutdown(SocketShutdown.Both);
                    //handler.Close();

                }

                else
                {
                    try
                    {
                        socket.Shutdown(SocketShutdown.Both);
                        socket.Close();
                        //Thread.CurrentThread.Abort();
                    }
                    catch (ObjectDisposedException ode)
                    {
                        Console.WriteLine("Socket already closed...");
                        //Thread.CurrentThread.Abort();
                    }

                    break;
                }
            }

        }

        public static void StartListening()
        {

            // Establish the local endpoint for the socket.  
            // Dns.GetHostName returns the name of the   
            // host running the application.  
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and   
            // listen for incoming connections.  

            try
            {
                listener.Bind(localEndPoint);

                listener.Listen(10);

                // Program is suspended while waiting for an incoming connection.  
                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    Socket handler = listener.Accept();
                    Thread thread = new Thread(() => ThreadWork(handler));
                    // threads.Add(new Thread(() => ThreadWork(handler)));
                    //Thread t = threads.ElementAt<Thread>(threads.Count -1);
                    //t.Start();
                    thread.Start();
                    Console.WriteLine("Client Connected");
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        public static int Main(String[] args)
        {
            StartListening();
            return 0;
        }
    }

}
