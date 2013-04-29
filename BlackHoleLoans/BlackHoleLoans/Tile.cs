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
  public class Tile
  {

    public Entity entity
    {
      set;
      get;
    }
    public bool Impassable
    {
      set;
      get;
    }
    public int Texture
    {
      set;
      get;
    }
    public int TileID //(in format xxxyyy, for three digit x, y coordinates)
    {
      private set;
      get;
    }
    public Tile(bool impass, int text, int tid)
    {
      Impassable = impass;
      Texture = text;
      TileID = tid;
    }
    public Tile(int impass, int text, int tid)
    {
      if (0 == impass)
        Impassable = false;
      else
        Impassable = true;
      Texture = text;
      TileID = tid;
    }
    public static int calcID(int x, int y)
    {
      return (x * 1000 + y);
    }
    public static int getX(int id)
    {
      return (int)(id / 1000);
    }
    public static int getY(int id)
    {
      return (int)(id % 1000);
    }
    public int getX()
    {
      return (int)(TileID / 1000);
    }
    public int getY()
    {
      return (int)(TileID % 1000);
    }
  }
}
