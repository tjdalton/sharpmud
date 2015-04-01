using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpMud.Commands
{
    public class Commands : ICommand
    {
        private readonly string[] _accessWords = { "COMMANDS" };
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
            c.WriteLine("UsableCommands available: ");
            c.WriteLine(String.Join(", ", c.World.UsableCommands.Where(u=>c.Player.Permissions.HasAny(c.World.CallCommand(u).PermissionsRequired))));
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
