using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpMud.Commands
{
    public class Dig : ICommand
    {
        private readonly string[] _accessWords = { "DIG" };
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
				return "DIG <direction> <room id>";
			}
		}

		public void Execute(Connection c, List<string> line)
        {
		    if (line.Count < 2)
			{
				c.WriteLine(Help);
				return;
			}
            var dir = line.First();
            line.Remove(dir);
            var rid = Int32.Parse(line.First());
            
            try
            {
               // var n = c.Db.Rooms.First(u => u.Id == c.Room.Id && u.Exits.First(x=>x.Direction.Name == dir));
            }
            catch (Exception)
            {

                try
                {
                    var newroom = c.Db.Rooms.First(u => u.Id == rid);
                    var curroom = c.Room;
                    try
                    {
                        var x = new Exit
                        {
                            Room = curroom,
                            To = newroom.Id,
                            Direction = c.Db.Directions.First(u => u.Name == dir)
                        };
                        curroom.Exits.Add(x);
                        x = new Exit
                        {
                            Room = newroom,
                            To = curroom.Id,
                            Direction = c.Db.Directions.First(u => u.From == dir)
                        };
                        newroom.Exits.Add(x);
                        c.Db.SaveChanges();
                        c.WriteLine(String.Format("An exit to {0} leading to room {1} has been created", c.Db.Directions.First(u => u.Name == dir).Name, rid));
                    }
                    catch (InvalidOperationException)
                    {

                        c.WriteLine("Invalid Direction");
                    }
                }
                catch (InvalidOperationException)
                {

                    c.WriteLine("Room not found");
                }
            }
           

        }


        public Permission[] PermissionsRequired
        {
            get
            {
                Permission[] p = { World.Db.Permissions.GetByName("dig"), World.Db.Permissions.GetByName("all") };
                return p;
            }
        }
    }
}
