using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SharpMud
{
    public class Connection
    {
        World world;
        public Connection(string id, World w, TcpClient t)
        {
            ID = id;
            world = w;
            ClientSocket = t;
        }

        public string ID
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
                return world;
            }
        }

        public MudDataContainer DB
        {
            get
            {
                return World.DB;
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
                var x = from p in DB.Players
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

        private SocketEvent _SEvent;
        public SocketEvent SocketEvent
        {
            get
            {
                return _SEvent;
            }
            set
            {
                _SEvent = value;
                _SEvent.lineReceived += SEvent_lineReceived;
            }
        }

        private void SEvent_lineReceived(string line)
        {
            var words = line.Split(' ');
            var n = words.Skip(1).Take(words.Count() - 1).ToList<string>();
            var cmd = world.CallCommand(words[0].ToUpper());
            LastCommand = words[0].ToUpper();
            if (cmd != null && this.Player.Permissions.HasAny(cmd.PermissionsRequired))
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
