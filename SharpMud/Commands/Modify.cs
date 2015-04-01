using System;
using System.Collections.Generic;

namespace SharpMud.Commands
{
    public class Modify : ICommand
    {
        private readonly string[] _accessWords = { "MODIFY", "MOD" };
        public string[] AccessWords
        {
           get { return _accessWords; }
        }

        public Permission[] PermissionsRequired
        {
            get
            {
                Permission[] p = { World.Db.Permissions.GetByName("modify"), World.Db.Permissions.GetByName("all") };
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
