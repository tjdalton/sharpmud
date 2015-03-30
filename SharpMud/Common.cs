using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMud
{

    public static class Extensions
    {
        public static bool HasAny<T>(this ICollection<T> array, T[] p)
        {
            foreach (var item in p)
            {
                if (array.Contains(item))
                    return true;
            }
            return false;
        }

        public static Permission GetByName(this DbSet<Permission> set, string s)
        {
            return set.FirstOrDefault(u => u.Name == s);
        }
    }


}
