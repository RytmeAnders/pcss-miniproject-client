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
            lobby = true;
            Console.Clear();

            while (lobby)
            {
                string LobbyResponse = reader.ReadLine();

                if (LobbyResponse.Equals("Lobby"))
                {
                    Console.WriteLine("LOBBY ENTERED");
                    playerNumber = Int32.Parse(reader.ReadLine());
                    Console.WriteLine("player " + playerNumber + ":");
                    LobbyResponse = "";
                }
                if (LobbyResponse.Equals("update"))
                {
                    string message = reader.ReadLine();
                    Console.WriteLine(message);
                    LobbyResponse = "";
                }

                if (LobbyResponse.Equals("Game Started"))
                {
                    lobby = false;
                    LobbyResponse = "";
                }
            }


            if (!lobby)
            {
                Console.Clear();
                while (true)
                {
                    // Wait for other players to join / wait to be killed
                    string response = reader.ReadLine();
                    Console.WriteLine(response);

                    if (response.Equals("Game Over"))
                        break;

                    if (response.Equals("It's your turn!"))
                    {
                        // Guess
                        Console.WriteLine("Take a guess (0-9):");
                        writer.WriteLine(Console.ReadLine());
                    }

                    if (response.Equals("Now guess!"))
                    {
                        // Guess
                        Console.WriteLine("Type 1000 to change your secret number");
                        Console.WriteLine("Take a guess (0-9):");
                        writer.WriteLine(Console.ReadLine());
                        Console.WriteLine();
                    }

                    if (response.Equals("change"))
                    {
                        Console.WriteLine("Enter your new secret number:");
                        mySecretNumber = Int32.Parse(Console.ReadLine());
                        writer.WriteLine(mySecretNumber);
                        Console.WriteLine();
                    }
                }
            }

        }
    }
}
