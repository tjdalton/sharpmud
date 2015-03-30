using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMud.Commands
{
    public class Look : ICommand       
    {
        string[] _accessWords = { "LOOK", "L" };
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
            var count = c.DB.Rooms.First(u => u.Id == c.Room.Id).Exits;
            if (count.Count() > 0)
                c.WriteLine("There are exits to the:");
            List<string> exits = new List<string>();
            foreach (var item in count)
            {
                exits.Add(item.Direction.Name);
            }
            if (exits.Count > 0)
                c.WriteLine(String.Join(", ",exits));
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
