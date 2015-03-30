using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMud.Commands
{
    public class Dig : ICommand
    {
        private string[] _accessWords = { "DIG" };
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
            Room newroom;
            Room curroom;
			if (line.Count < 2)
			{
				c.WriteLine(this.Help);
				return;
			}
            var dir = line.First();
            line.Remove(dir);
            var rid = Int32.Parse(line.First());
            
            try
            {
               // var n = c.DB.Rooms.First(u => u.Id == c.Room.Id && u.Exits.First(x=>x.Direction.Name == dir));
                var tmp = c.Player.Mob.Room.Exits.First(u => u.Direction.Name == dir);
            }
            catch (Exception)
            {

                try
                {
                    newroom = c.DB.Rooms.First(u => u.Id == rid);
                    curroom = c.Room;
                    //RoomExit re = new RoomExit();
                   // re.room = newroom.id;
                   // re.from = curroom.id;
                    try
                    {
                       // re.REDirection = c.DB.Directions.First(u => u.from == dir);
                       // c.DB.RoomExits.Add(re);
                       // RoomExit re2 = new RoomExit();
                       // re2.room = curroom.Id;
                       // re2.from = newroom.Id;
                       // re2.REDirection = c.DB.Directions.First(u => u.to == dir);
                       // c.DB.RoomExits.Add(re2);
                        var x = new Exit();
                        x.Room = curroom;
                        x.To = newroom.Id;
                        x.Direction = c.DB.Directions.First(u => u.Name == dir);
                        curroom.Exits.Add(x);
                        x = new Exit();
                        x.Room = newroom;
                        x.To = curroom.Id;
                        x.Direction = c.DB.Directions.First(u => u.From == dir);
                        newroom.Exits.Add(x);
                        c.DB.SaveChanges();
                        c.WriteLine(String.Format("An exit to {0} leading to room {1} has been created", c.DB.Directions.First(u => u.Name == dir).Name, rid));
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
                Permission[] p = { World.DB.Permissions.GetByName("dig"), World.DB.Permissions.GetByName("all") };
                return p;
            }
        }
    }
}
