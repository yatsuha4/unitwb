using System;
using System.Linq;
using System.Collections.Generic;

namespace Towerb.Ranking
{
  /**
     <summary>ランキング</summary>
  */
  public class Ranking<Item> where Item : class {
    /**
       <value>要素</value>
    */
    private Item[] items;

    /**
       <value>自身の要素</value>
    */
    public Item self { private set; get; }

    /**
       <summary>コンストラクタ</summary>
    */
    public Ranking(Item[] items, Item self)
    {
      this.items = items;
      this.self = self;
    }

    /**
       <summary>ランキングを取得する</summary>
       <param name="filter">フィルター</param>
       <param name="order">並び順</param>
       <param name="max">要素数</param>
       <param name="near">自身の前後数</param>
       <returns>ランキング</returns>
    */
    public List<(int, Item)> GetRanking<Key>(Func<Item, bool> filter, 
                                             Func<Item, Key> order, 
                                             int max = 10, 
                                             int near = 2)
    {
      var ranking = new List<(int, Item)>();
      var items = this.items.
        Where(item => filter(item)).
        OrderBy(item => order(item)).
        ThenBy(item => ((item == this.self) ? 0 : 1)).
        ToArray();
      if(this.self != null)
      {
        var rank = Array.IndexOf(items, this.self);
        if(rank >= max)
        {
          for(int i = rank - near, 
                bottom = Math.Min(rank + near + 1, items.Length);
              i < bottom; i++)
          {
            ranking.Add((i, items[i]));
          }
        }
      }
      for(int i = 0, 
            bottom = Math.Min(max, items.Length) - ranking.Count;
          i < bottom; i++)
      {
        ranking.Insert(i, (i, items[i]));
      }
      return ranking;
    }      
  }
}
