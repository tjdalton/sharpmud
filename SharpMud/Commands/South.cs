using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMud.Commands
{
    public class South : Move,ICommand
    {
        private string[] _accessWords = { "SOUTH", "S" };
        public string[] AccessWords
        {
            get { return _accessWords; }
        }


        public string Help
        {
            get { return "SOUTH"; }
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
