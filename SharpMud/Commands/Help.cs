using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMud.Commands
{
    public class Help : ICommand
    {
        private string[] _accessWords = { "HELP", "H" };

        public string[] AccessWords
        {
            get
            {
                return _accessWords;
            }
        }

		string ICommand.Help
		{
			get
			{
				return "HELP <COMMAND>";
			}
		}

		public void Execute(Connection c, List<string> line)
        {
            if (line.Count == 0)
            {
                c.WriteLine("HELP <COMMAND>");
                return;
            }
			var cmd = c.World.CallCommand(line.First().ToUpper());
			if (cmd != null && c.Player.Permissions.HasAny(PermissionsRequired))
				c.WriteLine(cmd.Help);
			else
				c.WriteLine("Unknown Command");
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
