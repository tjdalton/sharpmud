using System.IO;
using System.Linq;
using System.Net.Sockets;

namespace SharpMud
{
    public class Connection
    {
        readonly World _world;
        public Connection(string id, World w, TcpClient t)
        {
            Id = id;
            _world = w;
            ClientSocket = t;
        }

        public string Id
        {
            get;
            set;
        }

        private TcpClient ClientSocket
        {
            get;
            set;
        }

        public void Disconnect()
        {
            ClientSocket.Close();
            World.RemoveConnection(this);
        }

        public Player Player
        {
            get;
            set;
        }

        public string Description
        {
            get
            {
                return Player.Mob.Description;
            }
        }

        public World World
        {
            get
            {
                return _world;
            }
        }

        public MudDataContainer Db
        {
            get
            {
                return World.Db;
            }
        }

        public string LastCommand
        {
            get;
            private set;
        }

        public Room Room
        {
            get
            {
                var x = from p in Db.Players
                        where p.Id == Player.Id
                        select p.Mob.Room;
                return x.First();
            }
        }

        public NetworkStream Stream
        {
            get;
            set;
        }

        private SocketEvent _sEvent;
        public SocketEvent SocketEvent
        {
            get
            {
                return _sEvent;
            }
            set
            {
                _sEvent = value;
                _sEvent.LineReceived += SEvent_lineReceived;
            }
        }

        private void SEvent_lineReceived(string line)
        {
            var words = line.Split(' ');
            var n = words.Skip(1).Take(words.Count() - 1).ToList();
            var cmd = _world.CallCommand(words[0].ToUpper());
            LastCommand = words[0].ToUpper();
            if (cmd != null && Player.Permissions.HasAny(cmd.PermissionsRequired))
                cmd.Execute(this, n);
            else
                WriteLine("Unknown Command");
        }

        public void WriteLine(string line)
        {
            StreamWriter writer = new StreamWriter(Stream);
            writer.WriteLine(line);
            writer.Flush();
        }

        public string Prompt(string s)
        {
            StreamWriter writer = new StreamWriter(Stream);
            writer.WriteLine(s);
            StreamReader reader = new StreamReader(Stream);
            return reader.ReadLine();
        }
    }
}
