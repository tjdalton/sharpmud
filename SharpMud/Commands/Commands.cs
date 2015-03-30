using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMud.Commands
{
    public class Commands : ICommand
    {
        private string[] _accessWords = { "COMMANDS" };
        public string[] AccessWords
        {
            get
            {
                return _accessWords;
            }
        }

		public string Help
		{
			get
			{
				return "COMMANDS";
			}
		}

		public void Execute(Connection c, List<string> line)
        {
            c.WriteLine("Commands available: ");
            c.WriteLine(String.Join(", ", c.World.Commands.Where(u=>c.Player.Permissions.HasAny(c.World.CallCommand(u).PermissionsRequired))));
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
