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
            //initializes script variables         
            int playerNumber = 0;
            int mySecretNumber = 0;
            bool lobby = false;
            int port = 0;
            string IP = "";

           //request specific ip and port of the server
            Console.WriteLine("Please Enter the IP of the server");
            IP = Console.ReadLine();
            Console.WriteLine("Please enter the port of the server");
            port = Int32.Parse(Console.ReadLine());

            //Creates the tcpClient 
            TcpClient client = new TcpClient(IP, port);
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

            //The while loop initializes the lobby 
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
                //Updates the current number of players in the lobby for every
                if (LobbyResponse.Equals("update"))
                {
                    string message = reader.ReadLine();
                    Console.WriteLine(message);
                    Console.WriteLine(playerNumber);
                    LobbyResponse = "";
                }
                //Sends message from the server whenever the lobby is full 
                if (LobbyResponse.Equals("Game Started"))
                {
                    lobby = false;
                    LobbyResponse = "";
                }
            }

            //Goes out of the lobby and starts the game
            if (!lobby)
            {
                Console.Clear();
                //Keeps the game running
                while (true)
                {
                    // Wait for other players to join / wait to be killed
                    string response = reader.ReadLine();
                    Console.WriteLine(response);

                    //Stops the game from running if another player guesses your 
                    if (response.Equals("Game Over"))
                        break;

                    //Lets you choose a target
                    if (response.Equals("It's your turn!"))
                    {
                        // Target
                        Console.WriteLine("Choose a target (next / previous):");
                        writer.WriteLine(Console.ReadLine());
                        Console.WriteLine();
                    }

                    //Lets you take a guess or change your number after choosing a target
                    if (response.Equals("Now guess!"))
                    {
                        // Guess
                        Console.WriteLine("Type 1000 to change your secret number");
                        Console.WriteLine("Take a guess (0-9):");
                        writer.WriteLine(Console.ReadLine());
                        Console.WriteLine();
                    }

                    //Used if you choose the change your secret number
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
