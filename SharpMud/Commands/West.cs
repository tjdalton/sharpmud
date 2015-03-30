using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMud.Commands
{
    public class West : Move, ICommand
    {
        private string[] _accessWords = { "WEST", "W" };
        public string[] AccessWords
        {
            get { return _accessWords; }
        }

        public string Help
        {
            get { return "WEST"; }
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
