using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpMud.Commands
{
    public class Say : ICommand
    {
        private readonly string[] _accessWords = { "SAY"};
        public string[] AccessWords
        {
            get { return _accessWords; }
        }

        public void Execute(Connection c, List<string> line)
        {
            foreach (var item in c.World.Connections.Where(item => c.Id != item.Id && c.Room == item.Room))
            {
                item.WriteLine(String.Format("{0} says '{1}'", c.Description, line));
            }

            c.WriteLine(String.Format("you say '{0}'", line));
        }

        public string Help
        {
            get { return "SAY <something> -- Say something to the current room"; }
        }


        public Permission[] PermissionsRequired
        {
            get
            {
                Permission[] p = { World.Db.Permissions.GetByName("none"), World.Db.Permissions.GetByName("all") };
                return p;
            }
        }
    }
}
