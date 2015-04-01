using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SharpMud
{

    public static class Extensions
    {
        public static bool HasAny<T>(this ICollection<T> array, T[] p)
        {
            return p.Any(item => array.Contains(item));
        }

        public static Permission GetByName(this DbSet<Permission> set, string s)
        {
            return set.FirstOrDefault(u => u.Name == s);
        }
    }


}
