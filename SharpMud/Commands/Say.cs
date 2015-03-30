using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMud.Commands
{
    public class Say : ICommand
    {
        private string[] _accessWords = { "SAY"};
        public string[] AccessWords
        {
            get { return _accessWords; }
        }

        public void Execute(Connection c, List<string> line)
        {
            foreach (var item in c.World.Connections)
            {
                if (c.ID != item.ID && c.Room == item.Room)
                {
                    item.WriteLine(String.Format("{0} says '{1}'", c.Description, line));
                }
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
                Permission[] p = { World.DB.Permissions.GetByName("none"), World.DB.Permissions.GetByName("all") };
                return p;
            }
        }
    }
}
