using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SharpMud
{
    public class World
    {
        private List<Connection> connections;
        private MultiValueDictionary<string, Commands.ICommand> commands;
        static MudDataContainer md = new MudDataContainer();

        public World()
        {
            connections = new List<Connection>();
            commands = new MultiValueDictionary<string, Commands.ICommand>();
            LoadCommands();
        }

        public static  MudDataContainer DB
        {
            get
            {
                return md;
            }
        }
        public void AddConnection(Connection c)
        {
            connections.Add(c);
        }

        public void RemoveConnection(Connection c)
        {
            connections.Remove(c);
        }

        public Connection FindConnection(string id)
        {
            foreach (var item in connections)
            {
                if (item.ID == id)
                {
                    return item;
                }
            }

            return null;
        }

        public List<Connection> Connections
        {
            get
            {
                return connections;
            }
        }

        private void LoadCommands()
        {
            var cmd = from t in Assembly.GetExecutingAssembly().GetTypes()
                        where t.GetInterfaces().Contains(typeof(Commands.ICommand))
                                 && t.GetConstructor(Type.EmptyTypes) != null
                        select Activator.CreateInstance(t) as Commands.ICommand;

            foreach (var item in cmd)
            {
                if (item.AccessWords.Count() >= 1)
                    _Commands.Add(item.AccessWords[0]);
                foreach (var wd in item.AccessWords)
                {
                    commands.Add(wd, item);
                }
            }
        }

        public Commands.ICommand CallCommand(string s)
        {
            if (commands.ContainsKey(s))
                return commands[s].First();
            else
                return null;
        }
        List<string> _Commands = new List<string>();
        public List<string> Commands
        {
            get
            {
                return _Commands;
            }
        }

        public void EnterRoom(Connection c, string dir)
        {
            var cmd = new List<string>();
            cmd.Add("LOOK");
            c.World.CallCommand("LOOK").Execute(c,cmd);
            foreach (var item in connections)
            {
                if (c.ID != item.ID && c.Room == item.Room)
                {
                    if (dir != String.Empty)
                        item.WriteLine(String.Format("A {0} enters from the {1}", c.Player.Mob.Description, dir));
                    else
                        item.WriteLine(String.Format("A {0} appears from nowhere with a crack", c.Player.Mob.Description));
                }
            }
        }
    }
}
