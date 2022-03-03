using System.Collections.Generic;
using System.Linq;

namespace towerb
{
    /**
     */
    public static class Enum<T>
    {
        /**
         */
        public static IEnumerable<T> Values
        {
            get
            {
                return System.Enum.GetValues(typeof(T)).OfType<T>();
            }
        }

        /**
         */
        public static int Length
        {
            get
            {
                return System.Enum.GetValues(typeof(T)).Length;
            }
        }

        /**
         */
        public static T Parse(string value)
        {
            return (T)System.Enum.Parse(typeof(T), value);
        }
    }
}
