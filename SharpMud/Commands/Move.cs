using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMud.Commands
{
   public class Move 
    {

		public void Execute(Connection c, List<string> line)
        {
            string dir = c.LastCommand;

            try
            {
                //var n = c.DB.Directions.FirstOrDefault(u => u.Name == dir);
                //var rm = c.DB.RoomMobs.FirstOrDefault(u => u.room == c.Room.Id && u.mob == c.Player.Mob);
                
                //var tmp = c.DB.RoomExits.FirstOrDefault(u => u.from == c.Room.Id && u.REDirection == c.DB.Directions.FirstOrDefault(d => d.from == dir));
                var tmp = c.Player.Mob.Room.Exits.FirstOrDefault(u => u.Direction.Name.Equals(dir,StringComparison.InvariantCultureIgnoreCase));
                if (tmp == null)
                {
                    c.WriteLine("Unknown Command");
                    return;
                }
               // rm.RMRoom = tmp.RERoom;
                c.Player.Mob.Room = c.DB.Exits.First(u => u.To == c.Player.Mob.Room.Id && u.Direction.From.Equals(dir,StringComparison.InvariantCultureIgnoreCase)).Room;
                c.DB.SaveChanges();
                c.World.EnterRoom(c, dir);
            }
            catch (InvalidOperationException)
            {

                c.WriteLine("Unknown Command");
            }
        }
    }
}
