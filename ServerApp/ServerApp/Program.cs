using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

//
/*   Server Program    */



public class serv
{
    static TcpListener tcpListener = new TcpListener(10);
    //static LinkedList<string> online = new LinkedList<string>();
    static Dictionary<string, Socket> online_sockets = new Dictionary<string, Socket>();

    static void Listeners()
    {
        //keep track of connected clients

        Socket connectedToClient;
        Socket socketForClient = tcpListener.AcceptSocket();
        if (socketForClient.Connected)
        {
            Console.WriteLine("Client " + socketForClient.RemoteEndPoint + " now connected to server.");

            online_sockets.Add(socketForClient.RemoteEndPoint+"",socketForClient);

            NetworkStream networkStream = new NetworkStream(socketForClient);
            System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(networkStream);
            System.IO.StreamReader streamReader = new System.IO.StreamReader(networkStream);

            ////here we send message to client

            //Console.WriteLine("type your message to be recieved by client:");
            //string theString = Console.ReadLine();
            //streamWriter.WriteLine(theString);
            ////Console.WriteLine(theString);
            //streamWriter.Flush();

            //List online clients on the client interface
            string online_clients = "";
            foreach (var i in online_sockets)
            {
                online_clients = online_clients + i.Key+" ";
            }
            streamWriter.WriteLine(online_clients);
            Console.WriteLine(online_clients);
            streamWriter.Flush();

            string id = streamReader.ReadLine();
            Console.WriteLine("Setting up connection with "+id+"...");
            //int key = int.Parse(id); //NOT NEEDED

            //setup the connection for the connected client
            connectedToClient = online_sockets[id];
            Console.WriteLine("Sending messages to: "+connectedToClient.RemoteEndPoint);
            NetworkStream networkStream2 = new NetworkStream(connectedToClient);
            System.IO.StreamWriter streamWriter2 = new System.IO.StreamWriter(networkStream2);

            //while (true)
            //{
            //here we recieve client's text if any.
            while (true)
            {
                string theString = streamReader.ReadLine();
                streamWriter2.WriteLine(theString);
                streamWriter2.Flush();
                Console.WriteLine("[message]:" + theString);
                if (theString == "exit")
                    break;
            }
            streamReader.Close();
            networkStream.Close();
            streamWriter.Close();
            //}

        }
        socketForClient.Close();
        online_sockets.Remove(socketForClient.RemoteEndPoint + "");
        Console.WriteLine("Press any key to exit from server program");
        Console.ReadKey();
    }

    public static void Main()
    {

            // IPAddress ipAd = IPAddress.Parse("192.168.1.101");
            // use local m/c IP address, and 
            // use the same in the client

            /* Initializes the Listener */
            // TcpListener myList = new TcpListener(ipAd, 8001);

            tcpListener.Start();
            Console.WriteLine("************ SERVER ************");
            Console.WriteLine("Hoe many clients are going to connect to this server?:");
            int numberOfClientsYouNeedToConnect = int.Parse(Console.ReadLine());
            for (int i = 0; i < numberOfClientsYouNeedToConnect; i++)
            {
                Thread newThread = new Thread(new ThreadStart(Listeners));
                newThread.Start();
            }
        }
    

}