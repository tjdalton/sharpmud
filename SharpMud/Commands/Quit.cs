using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMud.Commands
{
    public class Quit : ICommand
    {
        private string[] _accessWords = { "QUIT", "QUI", "Q" };
        public string[] AccessWords
        {
            get { return _accessWords; }
        }

        public void Execute(Connection c, List<string> line)
        {
            StreamWriter writer = new StreamWriter(c.Stream);
            writer.WriteLine("Are you sure you want to quit? yes/no");
            writer.Flush();
            StreamReader reader = new StreamReader(c.Stream);
            var x = reader.ReadLine();
            if (String.Equals(x, "yes", StringComparison.InvariantCultureIgnoreCase) || String.Equals(x, "y", StringComparison.InvariantCultureIgnoreCase))
            {
                writer.WriteLine("You have been logged out.");
                writer.Flush();
                c.Disconnect();
            }
                
        }

        public string Help
        {
            get { return "QUIT - Disconnect from the server"; }
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
