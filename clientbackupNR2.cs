using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ServerSnacka
{
    public class Server
    {
        // Incoming data from the client.  
        public static string data = null;
        
        public static void ConnectionListenerThread(Socket handler)
        {

        }

        public static void MessageListenerThread(Socket handler)
        {

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

                while (true)
                {
                    Console.WriteLine("Waiting for a request...");

                    // Program is suspended while waiting for an incoming request.  
                    Socket handler = listener.Accept();
                    // Program resumes
                    Thread t1 = new Thread(() => ConnectionListenerThread(handler));
                    Thread t2 = new Thread(() => MessageListenerThread(handler));
                    t1.Start();
                    t2.Start();
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
