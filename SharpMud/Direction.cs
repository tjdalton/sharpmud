//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SharpMud
{
    using System;
    using System.Collections.Generic;
    
    public partial class Direction
    {
        public Direction()
        {
            this.Exits = new HashSet<Exit>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string From { get; set; }
    
        public virtual ICollection<Exit> Exits { get; set; }
    }
}
