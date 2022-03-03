using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace towerb.csv
{
    /**
     * <summary>csvを読み込む</summary>
     */
    public class CsvReader :
        IDisposable
    {
        /**
         * <value>内容</value>
         */
        public List<CsvRow> rows { private set; get; }

        /**
         * <summary>コンストラクタ</summary>
         * <param name="text">csvファイルのテキスト</param>
         */
        public CsvReader(string text)
        {
            using(var reader = new StringReader(text))
            {
                var line = reader.ReadLine();
                Debug.Assert(line != null);
                this.rows = new List<CsvRow>();
                var keys = line.Split(',');
                while((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',');
                    var row = new CsvRow();
                    for(int i = 0; i < keys.Length; ++i)
                    {
                        row.Add(keys[i], values[i]);
                    }
                    this.rows.Add(row);
                }
            }
        }

        /**
         */
        public void Dispose()
        {
            this.rows = null;
        }
    }
}
