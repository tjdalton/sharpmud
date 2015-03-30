using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharpMud
{
    class Program
    {
        static World world;
        static void Main(string[] args)
        {
            IPAddress i = IPAddress.Any;
            TcpListener serverSocket = new TcpListener(i, 8888);
            TcpClient clientSocket = default(TcpClient);
            int counter = 0;

            serverSocket.Start();
            Console.WriteLine(" >> " + "Server Started");
            world = new World();
            initDatabase(world);
            counter = 0;
            while (true)
            {
                counter += 1;
                clientSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine(" >> " + "Client No:" + Convert.ToString(counter) + " started!");
                handleClient client = new handleClient(world);
                
                client.startClient(clientSocket, Convert.ToString(counter));
            }

        }

        public static void initDatabase(World w)
        {
            w = new World();
            //var n = World.DB.Rooms.FirstOrDefault(u => u.Id == 1);
            var n = World.DB.Rooms.FirstOrDefault(u => u.Id == 1);
            if (n == null)
            {
                Room r = new Room();
                r.Description = "A featureless grey room";
                World.DB.Rooms.Add(r);
                Direction d = new Direction();
                d.Name = "ne";
                d.From = "sw";
                World.DB.Directions.Add(d);
                d = new Direction();
                d.Name = "sw";
                d.From = "ne";
                World.DB.Directions.Add(d);
                d = new Direction();
                d.Name = "nw";
                d.From = "se";
                World.DB.Directions.Add(d);
                d = new Direction();
                d.Name = "n";
                d.From = "s";
                World.DB.Directions.Add(d);
                d = new Direction();
                d.Name = "s";
                d.From = "n";
                World.DB.Directions.Add(d);
                d = new Direction();
                d.Name = "e";
                d.From = "w";
                World.DB.Directions.Add(d);
                d = new Direction();
                d.Name = "w";
                d.From = "e";
                World.DB.Directions.Add(d);
                d = new Direction();
                d.Name = "up";
                d.From = "down";
                World.DB.Directions.Add(d);
                d = new Direction();
                d.Name = "down";
                d.From = "up";
                World.DB.Directions.Add(d);
                Permission a = new Permission();
                a.Name = "none";
                World.DB.Permissions.Add(a);
                a = new Permission();
                a.Name = "all";
                World.DB.Permissions.Add(a);
                World.DB.SaveChanges();
            }

        }
    }

    public enum ChatState
    {
        Login,
        CharacterCreate,
        Playing
    }
    //Class to handle each client request separatly
    public class handleClient
    {
        TcpClient clientSocket;
        string clNo;
        Connection player;
        World world;
        ChatState state;
        string username;


        public handleClient(World w)
        {
            world = w;
            state = ChatState.Login;
        }
        public void startClient(TcpClient inClientSocket, string clineNo)
        {
            this.clientSocket = inClientSocket;
            this.clNo = clineNo;
            player = new Connection(clineNo, world, inClientSocket);
            player.SocketEvent = new SocketEvent();
            world.AddConnection(player);
            Thread ctThread = new Thread(doChat);
            ctThread.Start();
        }
        private void doChat()
        {
            string dataFromClient;
            while ((true))
            {
                try
                {
                    NetworkStream networkStream = clientSocket.GetStream();
                    StreamReader reader = new StreamReader(networkStream);
                    player.Stream = networkStream;
                    if (state == ChatState.Login)
                    {
                        StreamWriter writer = new StreamWriter(networkStream);
                        writer.WriteLine("What is your username?");
                        writer.Flush();
                        dataFromClient = reader.ReadLine();
                        username = dataFromClient;
                        Player p;
                        try
                        {
                            p = World.DB.Players.First(u => u.Username == username);
                            player.Player = p;
                           // World.DB.Permissions.getByName("all").Players.Add(p);
                            World.DB.SaveChanges();
                            state = ChatState.Playing;
                            world.EnterRoom(player,String.Empty);
                        }
                        catch(Exception e)
                        {
                            p = new Player();
                            p.Username = username;
                            writer.WriteLine("What is your password");
                            writer.Flush();
                            p.Password = reader.ReadLine();
                            writer.WriteLine("What is your email");
                            writer.Flush();
                            p.Email = reader.ReadLine();
                            p.Status = 1;
                            writer.WriteLine("What is your first name?");
                            writer.Flush();
                            p.Firstname = reader.ReadLine();
                            writer.WriteLine("What is your last name?");
                            writer.Flush();
                            World.DB.Permissions.GetByName("all").Players.Add(p);
                            p.Lastname = reader.ReadLine();
                            p.LastLogin = DateTime.Now;
                            var startRoom = World.DB.Rooms.FirstOrDefault(u => u.Id == 1);
                            Mob m = new Mob();
                            m.Description = "A Player";
                            m.Room = startRoom;
                            World.DB.Mobs.Add(m);
                            startRoom.Mobs.Add(m);
                            p.Mob = m;
                            World.DB.Players.Add(p);
							player.Player = p;
							World.DB.SaveChanges();
                            state = ChatState.Playing;
							world.EnterRoom(player, String.Empty);
                            
                        }

                        
                    }

                    if (state == ChatState.Playing)
                    {
                        dataFromClient = reader.ReadLine();
                        try
                        {
                            player.SocketEvent.ProcessLine(dataFromClient.Replace("\0", String.Empty));
                        }
                         
                        catch(NullReferenceException)
                        {
                            clientSocket.Close();
                            world.RemoveConnection(player);
                            break;
                        }

                    }
                        
                }
                catch(ObjectDisposedException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" >> " + ex.ToString());
                }
            }
        }
    }

    public class SocketEvent
    {
        public delegate void LineReceivedHandler(string line);

        public event LineReceivedHandler lineReceived;

        public void ProcessLine(string line)
        {
                if (lineReceived != null)
                {
                    lineReceived(line);
                }
        }
    }
}
