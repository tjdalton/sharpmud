using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpMud.Commands
{
   public class Move 
    {

		public void Execute(Connection c, List<string> line)
        {
            var dir = c.LastCommand;

            try
            {
                var tmp = c.Player.Mob.Room.Exits.FirstOrDefault(u => u.Direction.Name.Equals(dir,StringComparison.InvariantCultureIgnoreCase));
                if (tmp == null)
                {
                    c.WriteLine("Unknown Command");
                    return;
                }
                c.Player.Mob.Room = c.Db.Exits.First(u => u.To == c.Player.Mob.Room.Id && u.Direction.From.Equals(dir,StringComparison.InvariantCultureIgnoreCase)).Room;
                c.Db.SaveChanges();
                c.World.EnterRoom(c, dir);
            }
            catch (InvalidOperationException)
            {

                c.WriteLine("Unknown Command");
            }
        }
    }
}
