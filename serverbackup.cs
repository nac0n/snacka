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
            Socket socket = handler;

            while (true)
            {
                if (handler.Connected == true)
                {
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
                        if (data.IndexOf("<EOF>") > -1)
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
                            Console.WriteLine("couldn't send response as the socket is disposed...");
                            Thread.CurrentThread.Abort();
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
                        Thread.CurrentThread.Abort();
                    }
                    catch(ObjectDisposedException ode)
                    {
                        Console.WriteLine("Socket already closed...");
                        Thread.CurrentThread.Abort();
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
