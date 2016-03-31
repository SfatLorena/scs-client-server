/*       Client Program      */

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;



public class clnt
{
    static int port = 8002;
    public static void Main()
    {

        String client_name="";
        TcpClient socketForServer;
        try
        {
            socketForServer = new TcpClient("localHost", 10);
        }
        catch
        {
            Console.WriteLine(
            "Failed to connect to server at {0}:999", "localhost");
            return;
        }
        //client_name=socketForServer.

        NetworkStream networkStream = socketForServer.GetStream();
        System.IO.StreamReader streamReader = new System.IO.StreamReader(networkStream);
        System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(networkStream);
        Console.WriteLine("************ CLIENT *************");

        try
        {
            string outputString;
            // read the data from the host and display it
            {
                outputString = streamReader.ReadLine();
                Console.WriteLine("Message Recieved from server:" + outputString);

                outputString = streamReader.ReadLine();

                //Console.WriteLine("Type your message to be recieved by server:");
                Console.WriteLine("type:");
                
                string str = Console.ReadLine();
                while (true )//&& outputString != "exit")
                {
                    /*if (outputString != "")
                    {
                        Console.WriteLine("Message Recieved from server:" + outputString);
                        outputString = streamReader.ReadLine();
                    }*/
                    if (str == "exit")
                    {
                        break;
                        streamWriter.WriteLine(str);
                        streamWriter.Flush();

                    }
                    streamWriter.WriteLine(str);
                    streamWriter.Flush();
                    Console.WriteLine("type:");
                    str = Console.ReadLine();
                    
                }
                

            }
        }
        catch
        {
            Console.WriteLine("Exception reading from Server");
        }
        // tidy up
        networkStream.Close();
        Console.WriteLine("Press any key to exit from client program");
        Console.ReadKey();
    }

}