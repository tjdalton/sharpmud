using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpMud.Commands
{
    public class Look : ICommand       
    {
        readonly string[] _accessWords = { "LOOK", "L" };
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
				return "LOOK";
			}
		}

		public void Execute(Connection c, List<string> line)
        {
            c.WriteLine(c.Room.Description);
            var count = c.Db.Rooms.First(u => u.Id == c.Room.Id).Exits;
            if (count.Any())
                c.WriteLine("There are exits to the:");
            var exits = count.Select(item => item.Direction.Name).ToList();
		    if (exits.Count > 0)
                c.WriteLine(String.Join(", ",exits));
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
