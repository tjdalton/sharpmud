//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Collections.Generic;

namespace SharpMud
{
    public partial class Room
    {
        public Room()
        {
            this.Mobs = new HashSet<Mob>();
            this.Exits = new HashSet<Exit>();
        }
    
        public int Id { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<Mob> Mobs { get; set; }
        public virtual ICollection<Exit> Exits { get; set; }
    }
}
