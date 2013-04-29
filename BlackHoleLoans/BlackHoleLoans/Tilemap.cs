using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BlackHoleLoans
{
  public class TileMap
  {
    public List<Entity> EntityList;
    public Tile[,] Map
    {
      get;
      private set;
    }

    /*public TileMap(Tile[,] m)
    {
        Map = m;
    }*/

    public TileMap(int[,] existingMap, bool[,] existingCollisionMap)
    {
      Map = new Tile[existingMap.GetLength(0), existingMap.GetLength(1)];
      for (int x = 0; x < Map.GetLength(0); x++)
      {
        for (int y = 0; y < Map.GetLength(1); y++)
        {
          Map[x, y] = new Tile(existingCollisionMap[x, y], existingMap[x, y], Tile.calcID(x, y));
        }
      }
      EntityList = new List<Entity>();
    }
    public TileMap(int[,] existingMap, int[,] existingCollisionMap)
    {
      Map = new Tile[existingMap.GetLength(0), existingMap.GetLength(1)];
      for (int x = 0; x < Map.GetLength(0); x++)
      {
        for (int y = 0; y < Map.GetLength(1); y++)
        {
          Map[x, y] = new Tile(existingCollisionMap[x, y], existingMap[x, y], Tile.calcID(x, y));
          //Console.Write(existingMap[x, y] + ", ");
        }
        //Console.Write("\n");
      }
      EntityList = new List<Entity>();
    }
    public Tile getTile(int x, int y)
    {
      return Map[x, y];
    }

  }
}
