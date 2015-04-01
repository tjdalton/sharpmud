using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SharpMud.Commands;

namespace SharpMud
{
    public class World
    {
        private readonly List<Connection> _connections;
        private readonly MultiValueDictionary<string, ICommand> _commands;
        static readonly MudDataContainer Md = new MudDataContainer();

        public World()
        {
            _connections = new List<Connection>();
            _commands = new MultiValueDictionary<string, ICommand>();
            LoadCommands();
        }

        public static  MudDataContainer Db
        {
            get
            {
                return Md;
            }
        }
        public void AddConnection(Connection c)
        {
            _connections.Add(c);
        }

        public void RemoveConnection(Connection c)
        {
            _connections.Remove(c);
        }

        public Connection FindConnection(string id)
        {
            return _connections.FirstOrDefault(item => item.Id == id);
        }

        public List<Connection> Connections
        {
            get
            {
                return _connections;
            }
        }

        private void LoadCommands()
        {
            var cmd = from t in Assembly.GetExecutingAssembly().GetTypes()
                        where t.GetInterfaces().Contains(typeof(ICommand))
                                 && t.GetConstructor(Type.EmptyTypes) != null
                        select Activator.CreateInstance(t) as ICommand;

            foreach (var item in cmd)
            {
                if (item.AccessWords.Any())
                    _usableCommands.Add(item.AccessWords[0]);
                foreach (var wd in item.AccessWords)
                {
                    _commands.Add(wd, item);
                }
            }
        }

        public ICommand CallCommand(string s)
        {
            if (_commands.ContainsKey(s))
                return _commands[s].First();
            else
                return null;
        }

        readonly List<string> _usableCommands = new List<string>();
        public List<string> UsableCommands
        {
            get
            {
                return _usableCommands;
            }
        }

        public void EnterRoom(Connection c, string dir)
        {
            var cmd = new List<string> {"LOOK"};
            c.World.CallCommand("LOOK").Execute(c,cmd);
            foreach (var item in _connections.Where(item => c.Id != item.Id && c.Room == item.Room))
            {
                item.WriteLine(dir == String.Empty
                    ? String.Format("A {0} appears from nowhere with a crack", c.Player.Mob.Description)
                    : String.Format("A {0} enters from the {1}", c.Player.Mob.Description, dir));
            }
        }
    }
}
