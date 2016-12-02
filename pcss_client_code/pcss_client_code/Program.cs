using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace pcss_client_code
{
    class MT_Client
    {
        static void Main()
        {
            Console.WriteLine("Starting echo client...");

            int playerNumber = 0;
            int mySecretNumber = 0;
            bool lobby = false;
            int port = 1234;
            string IP = "127.0.0.1";

            Console.WriteLine("Please Enter the IP of the server");
            IP = Console.ReadLine();
            Console.WriteLine("Please enter the port of the server");
            port = Int32.Parse(Console.ReadLine());

            TcpClient client = new TcpClient("localhost", port);
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };

            //confirms the player is connected
            Console.WriteLine("You are connected");

            // Choose your number 0-9
            Console.WriteLine("Enter your secret number:");
            mySecretNumber = Int32.Parse(Console.ReadLine());

            // Let the server know our number
            writer.WriteLine(mySecretNumber);

        }
    }
}
