using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMud.Commands
{
    public class Modify : ICommand
    {
        private string[] _accessWords = { "MODIFY", "MOD" };
        public string[] AccessWords
        {
           get { return _accessWords; }
        }

        public Permission[] PermissionsRequired
        {
            get
            {
                Permission[] p = { World.DB.Permissions.GetByName("modify"), World.DB.Permissions.GetByName("all") };
                return p;
            }
        }

        public void Execute(Connection c, List<string> line)
        {
            throw new NotImplementedException();
        }

        public string Help
        {
            get { return "MODIFY <type>"; }
        }
    }
}
