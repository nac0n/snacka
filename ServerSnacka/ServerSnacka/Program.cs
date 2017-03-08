using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Collections.Generic;
//using System.Linq;
//using System.Collections.Generic;

namespace ServerSnacka
{
    public class Server
    {
        // Incoming data from the client.  
        public static string data = "";
        public static List<Socket>_clientSocketsList = new List<Socket>();
        //public static List<Thread> threads = new List<Thread>();
        
        public static int Main(String[] args)
        {
            StartListening();
            return 0;
        }

        //public static void SendToAllClients(byte[] msg)
        //{
        //    foreach(Socket client in _clientSocketsList)
        //    {
        //        client.Send(msg);
        //    }
        //}

        public static void ThreadWork(Socket handler)
        {
            // Data buffer for incoming data.  
            byte[] bytes = new Byte[1024];
            Socket socket = handler;
           //_clientSocketsList.Add(socket);

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
                            //Tråden stannar här och väntar på att klient ska skriva någonting.
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
                            socket.Send(msg);
                            //SendToAllClients(msg);
                            Console.WriteLine("SENT!");
                            //socket.Close();
                            //try
                            //{
                            //    Thread.CurrentThread.Abort();
                            //}
                            //catch(ThreadAbortException tae)
                            //{
                            //    Console.WriteLine("Couldn't Abort Thread, Row 110");
                            //    Console.WriteLine(tae.Message);
                            //}
                            
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
                        //socket.Shutdown(SocketShutdown.Both);
                        //socket.Close();
                        //try
                        //{
                        //    Thread.CurrentThread.Abort();
                        //}
                        //catch(ThreadAbortException tae)
                        //{
                        //    Console.WriteLine("Couldn't Abort Thread, Row 110");
                        //    Console.WriteLine(tae.Message);
                        //}
                    }
                    catch (ObjectDisposedException ode)
                    {
                        Console.WriteLine("Socket already closed...");
                        //ThreadWork(socket);
                        //break;
                        //try
                        //{
                        //    Thread.CurrentThread.Abort();
                        //}
                        //catch(ThreadAbortException tae)
                        //{
                        //    Console.WriteLine("Couldn't Abort Thread, Row 110");
                        //    Console.WriteLine(tae.Message);
                        //}
                    }

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
                Console.WriteLine("Error in trying to connect or create thread in line 164, Exception e");
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }
        
    }

}
