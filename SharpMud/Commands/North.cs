using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMud.Commands
{
    public class North : Move,ICommand
    {
        private string[] _accessWords = { "NORTH", "N" };
        public string[] AccessWords
        {
            get { return _accessWords; }
        }


        public string Help
        {
            get { return "NORTH"; }
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
