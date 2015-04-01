using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpMud.Commands
{
	public class Create : ICommand
	{
		private readonly string[] _accessWords = { "CREATE" };
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
				return "CREATE <TYPE>";
			}
		}

		public void Execute(Connection c, List<string> line)
		{
            var type = line.First();
            if(type.Equals("ROOM", StringComparison.InvariantCultureIgnoreCase))
            {
                Room r = new Room();
                r.Description = String.Join(" ", line.Skip(1).Take(line.Count));
                c.Db.Rooms.Add(r);
                c.Db.SaveChanges();
                c.WriteLine(String.Format("Room {0} has been created", r.Id));

            }

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
