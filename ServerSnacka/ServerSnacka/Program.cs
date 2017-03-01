using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace ServerSnacka
{
    public class Server
    {
        // Incoming data from the client.  
        public static string data = null;
        
        public static void ThreadWork(Socket handler)
        {
            // Data buffer for incoming data.  
            byte[] bytes = new Byte[1024];

            if (handler.Connected == true)
            {
                data = null;

                // An incoming connection needs to be processed.  
                while (true)
                {
                    bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                }

                // Show the data on the console.  
                Console.WriteLine("Text received : {0}", data);

                // Echo the data back to the client.  
                byte[] msg = Encoding.UTF8.GetBytes(data);

                handler.Send(msg);
                        //handler.Shutdown(SocketShutdown.Both);
                        //handler.Close();
            }
            else
            {
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
                StartListening();
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
                    thread.Start();
                    Console.WriteLine("Connection established");
                }
               
                // Start listening for connections.  
                //while (true)
                //{
                //    if (handler.connected == true)
                //    {
                //        data = null;

                //        // An incoming connection needs to be processed.  
                //        while (true)
                //        {
                //            bytes = new byte[1024];
                //            int bytesRec = handler.Receive(bytes);
                //            data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                //            if (data.IndexOf("<EOF>") > -1)
                //            {
                //                break;
                //            }
                //        }

                //        // Show the data on the console.  
                //        Console.WriteLine("Text received : {0}", data);

                //        // Echo the data back to the client.  
                //        byte[] msg = Encoding.UTF8.GetBytes(data);

                //        handler.Send(msg);
                //        //handler.Shutdown(SocketShutdown.Both);
                //        //handler.Close();
                //    //}
                //    else
                //    {
                //        handler.Shutdown(SocketShutdown.Both);
                //        handler.Close();
                //        StartListening();
                //    }

                //}

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
