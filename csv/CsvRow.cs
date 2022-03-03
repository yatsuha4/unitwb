using System.Collections.Generic;

namespace towerb.csv
{
    /**
     */
    public class CsvRow :
        Dictionary<string, string>
    {
        /**
         */
        public CsvRow()
        {
        }

        /**
         */
        public int GetInt(string key)
        {
            return int.Parse(this[key]);
        }
    }
}
