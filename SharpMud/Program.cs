using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SharpMud
{
    class Program
    {
        static World _world;
        static void Main()
        {
            var i = IPAddress.Any;
            var serverSocket = new TcpListener(i, 8888);

            serverSocket.Start();
            Console.WriteLine(" >> " + "Server Started");
            _world = new World();
            InitDatabase();
            var counter = 0;
            while (true)
            {
                counter += 1;
                var clientSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine(" >> " + "Client No:" + Convert.ToString(counter) + " started!");
                var client = new HandleClient(_world);
                
                client.StartClient(clientSocket, Convert.ToString(counter));
            }

            // ReSharper disable once FunctionNeverReturns
        }

        public static void InitDatabase()
        {
            _world = new World();
            //var n = World.Db.Rooms.FirstOrDefault(u => u.Id == 1);
            var n = World.Db.Rooms.FirstOrDefault(u => u.Id == 1);
            if (n != null) return;
            var r = new Room {Description = "A featureless grey room"};
            World.Db.Rooms.Add(r);
            var d = new Direction {Name = "ne", From = "sw"};
            World.Db.Directions.Add(d);
            d = new Direction {Name = "sw", From = "ne"};
            World.Db.Directions.Add(d);
            d = new Direction {Name = "nw", From = "se"};
            World.Db.Directions.Add(d);
            d = new Direction {Name = "n", From = "s"};
            World.Db.Directions.Add(d);
            d = new Direction {Name = "s", From = "n"};
            World.Db.Directions.Add(d);
            d = new Direction {Name = "e", From = "w"};
            World.Db.Directions.Add(d);
            d = new Direction {Name = "w", From = "e"};
            World.Db.Directions.Add(d);
            d = new Direction {Name = "up", From = "down"};
            World.Db.Directions.Add(d);
            d = new Direction {Name = "down", From = "up"};
            World.Db.Directions.Add(d);
            var a = new Permission {Name = "none"};
            World.Db.Permissions.Add(a);
            a = new Permission {Name = "all"};
            World.Db.Permissions.Add(a);
            World.Db.SaveChanges();
        }
    }

    public enum ChatState
    {
        Login,
        CharacterCreate,
        Playing
    }
    //Class to handle each client request separatly
    public class HandleClient
    {
        TcpClient _clientSocket;
        Connection _player;
        readonly World _world;
        ChatState _state;
        string _username;


        public HandleClient(World w)
        {
            _world = w;
            _state = ChatState.Login;
        }

        public void StartClient(TcpClient inClientSocket, string clineNo)
        {
            _clientSocket = inClientSocket;
            _player = new Connection(clineNo, _world, inClientSocket) {SocketEvent = new SocketEvent()};
            _world.AddConnection(_player);
            var ctThread = new Thread(DoChat);
            ctThread.Start();
        }

        private void DoChat()
        {
            while ((true))
            {
                try
                {
                    var networkStream = _clientSocket.GetStream();
                    var reader = new StreamReader(networkStream);
                    _player.Stream = networkStream;
                    if (_state == ChatState.Login)
                    {
                        var writer = GetUserName(networkStream, reader);
                        try
                        {
                            PlayerLogin();
                        }
                        catch
                        {
                            NewPlayer(writer, reader);
                        }
                    }

                    if (_state != ChatState.Playing) continue;
                    var dataFromClient = reader.ReadLine();
                    try
                    {
                        if (dataFromClient != null)
                            _player.SocketEvent.ProcessLine(dataFromClient.Replace("\0", String.Empty));
                    }
                         
                    catch(NullReferenceException)
                    {
                        _clientSocket.Close();
                        _world.RemoveConnection(_player);
                        break;
                    }
                }
                catch(ObjectDisposedException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" >> " + ex);
                }
            }
        }

        private void PlayerLogin()
        {
            var p = World.Db.Players.First(u => u.Username == _username);
            _player.Player = p;
            World.Db.SaveChanges();
            _state = ChatState.Playing;
            _world.EnterRoom(_player, String.Empty);
        }

        private StreamWriter GetUserName(NetworkStream networkStream, StreamReader reader)
        {
            var writer = new StreamWriter(networkStream);
            writer.WriteLine("What is your username?");
            writer.Flush();
            var dataFromClient = reader.ReadLine();
            _username = dataFromClient;
            return writer;
        }

        private void NewPlayer(StreamWriter writer, StreamReader reader)
        {
            var p = new Player {Username = _username};
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
            World.Db.Permissions.GetByName("all").Players.Add(p);
            p.Lastname = reader.ReadLine();
            p.LastLogin = DateTime.Now;
            var startRoom = World.Db.Rooms.FirstOrDefault(u => u.Id == 1);
            var m = new Mob {Description = "A Player", Room = startRoom};
            World.Db.Mobs.Add(m);
            if (startRoom != null) startRoom.Mobs.Add(m);
            p.Mob = m;
            World.Db.Players.Add(p);
            _player.Player = p;
            World.Db.SaveChanges();
            _state = ChatState.Playing;
            _world.EnterRoom(_player, String.Empty);
        }
    }

    public class SocketEvent
    {
        public delegate void LineReceivedHandler(string line);

        public event LineReceivedHandler LineReceived;

        public void ProcessLine(string line)
        {
                if (LineReceived != null)
                {
                    LineReceived(line);
                }
        }
    }
}
