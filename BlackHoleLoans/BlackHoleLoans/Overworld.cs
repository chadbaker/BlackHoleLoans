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
using BlackHoleLoans.PlayerRelated;

namespace BlackHoleLoans
{
  public class OverWorld
  {
    public List<Entity> EntityList
    {
      get
      {
        return OWmap.EntityList;
      }
      set
      {
        OWmap.EntityList = value;
      }
    }
    private List<Texture2D> tileTextures = new List<Texture2D>();
    private List<Texture2D> avatar = new List<Texture2D>();
    public TileMap OWmap // map information.
    {
      get;
      set;
    }
    public List<TileMap> mapList;
    public int Xpos // x position of player
    {
      get;
      set;
    }

    public int Ypos // y position of player
    {
      get;
      set;
    }

    private int facing;
    public int Facing // 0 = up, 1 = right, 2 = down, 3 = left
    {
      get
      {
        return facing;
      }
      set
      {
        facing = (value % 4);
      }
    }
    public Game1 Game_Ref
    {
      get;
      set;
    }

    bool inCombat = false;
    Enemy combatEnemy;

    public OverWorld(TileMap owm, int x, int y, Game1 g)
    {
      OWmap = owm;
      Xpos = x;
      Ypos = y;
      Facing = 2;
      EntityList = new List<Entity>();
      Game_Ref = g;
      mapList = new List<TileMap>();
      mapList.Add(OWmap);
    }

    public OverWorld(int[,] existingMap, bool[,] existingCollisionMap, int x, int y, Game1 g)
    {
      OWmap = new TileMap(existingMap, existingCollisionMap);
      Xpos = x;
      Ypos = y;
      Facing = 2;
      EntityList = new List<Entity>();
      Game_Ref = g;
      mapList = new List<TileMap>();
      mapList.Add(OWmap);
    }

    public OverWorld(int[,] existingMap, int[,] existingCollisionMap, int x, int y, Game1 g)
    {
      OWmap = new TileMap(existingMap, existingCollisionMap);
      Xpos = x;
      Ypos = y;
      Facing = 2;
      EntityList = new List<Entity>();
      Game_Ref = g;
      mapList = new List<TileMap>();
      mapList.Add(OWmap);
    }

    public void LoadTileTextures(ContentManager content, params string[] fileNames)
    {
      Texture2D tileTexture;

      foreach (string fileName in fileNames)
      {
        tileTexture = content.Load<Texture2D>(fileName);
        tileTextures.Add(tileTexture);
      }


    }

    public void LoadAvatar(ContentManager content, Texture2D[] playerSprites)
    {
      foreach (Texture2D pSprite in playerSprites)
      {
        avatar.Add(pSprite);
      }
    }

    public void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
    {
      spriteBatch.Begin();

      for (int x = 0; x < OWmap.Map.GetLength(0); x++)
      {

        for (int y = 0; y < OWmap.Map.GetLength(1); y++)
        {
          int index = OWmap.Map[x, y].Texture;

          Texture2D texture = tileTextures[index];

          spriteBatch.Draw(texture, new Rectangle(y * graphics.PreferredBackBufferWidth / OWmap.Map.GetLength(1),
                                    x * graphics.PreferredBackBufferHeight / OWmap.Map.GetLength(0),
                                    graphics.PreferredBackBufferWidth / OWmap.Map.GetLength(1),
                                    graphics.PreferredBackBufferHeight / OWmap.Map.GetLength(0)),
                                    Color.White);
        }
        spriteBatch.Draw(avatar[Facing], new Rectangle(Ypos * graphics.PreferredBackBufferWidth / OWmap.Map.GetLength(1),
                                      Xpos * graphics.PreferredBackBufferHeight / OWmap.Map.GetLength(0),
                                      graphics.PreferredBackBufferWidth / OWmap.Map.GetLength(1),
                                      graphics.PreferredBackBufferHeight / OWmap.Map.GetLength(0)),
                                      Color.White);
        foreach (Entity e in EntityList)
        {
          spriteBatch.Draw(e.EntityAvatar[e.Facing], new Rectangle(e.tile.getY() * graphics.PreferredBackBufferWidth / OWmap.Map.GetLength(1),
                                    e.tile.getX() * graphics.PreferredBackBufferHeight / OWmap.Map.GetLength(0),
                                    graphics.PreferredBackBufferWidth / OWmap.Map.GetLength(1),
                                    graphics.PreferredBackBufferHeight / OWmap.Map.GetLength(0)),
                                    Color.White);
        }
      }

      spriteBatch.End();
    }

    public Tile getCurrent()
    {
      return OWmap.getTile(Xpos, Ypos);
    }

    public Tile getAdjacent(int direction)
    {
      int dir = direction % 4;
      if (dir == 0 && (Xpos - 1 >= 0))
        return OWmap.getTile(Xpos - 1, Ypos);
      else if (dir == 1 && (Ypos + 1 < OWmap.Map.GetLength(1)))
        return OWmap.getTile(Xpos, Ypos + 1);
      else if (dir == 2 && (Xpos + 1 < OWmap.Map.GetLength(0)))
        return OWmap.getTile(Xpos + 1, Ypos);
      else if (dir == 3 && (Ypos - 1 >= 0))
        return OWmap.getTile(Xpos, Ypos - 1);
      else
      {
        return null;
      }
    }

    public Tile getAdjacent(Tile t, int direction)
    {
      int x = t.getX();
      int y = t.getY();
      int dir = direction % 4;
      if (dir == 0 && (x - 1 >= 0))
        return OWmap.getTile(x - 1, y);
      else if (dir == 1 && (y + 1 < OWmap.Map.GetLength(1)))
        return OWmap.getTile(x, y + 1);
      else if (dir == 2 && (x + 1 < OWmap.Map.GetLength(0)))
        return OWmap.getTile(x + 1, y);
      else if (dir == 3 && (y - 1 >= 0))
        return OWmap.getTile(x, y - 1);
      else
      {
        return null;
      }
    }

    public Tile getForward()
    {
      return getAdjacent(Facing);
    }

    public bool playerStep() //defaults to current direction
    {
      Tile forward = getForward();
      if (forward == null || forward.Impassable)
        return false;

      else if (forward.entity != null)
      {
        if (forward.entity.IsEnemy())
        {
          inCombat = true;
          combatEnemy = (Enemy)forward.entity;
          return false;
        }
        forward.entity.OnCollision();
        return false;
      }
      else
      {
        if (Facing == 0)
          Xpos--;
        else if (Facing == 1)
          Ypos++;
        else if (Facing == 2)
          Xpos++;
        else if (Facing == 3)
          Ypos--;
        else
          return false;
        return true;
      }
    }

    public bool playerStep(int direction)
    {
      int dir = direction % 4;
      if (dir != Facing)
      {
        Facing = dir;
        return true;
      }
      else
        return playerStep();
    }

    public bool playerInteract()
    {
      Tile forwardTile = getForward();//added this
      if (forwardTile == null)//added a check when trying to access tile out of bounds
      {
        //Console.WriteLine("Tried to access tile out of bounds");//added this
        return false;
      }

      Entity target = forwardTile.entity;
      if (target == null)
      {
        //Console.WriteLine("Player attempted to interact with entity in tile without an entity.");
        return false;
      }
      else
      {
        target.OnInteract();
        return true;
      }
    }

    public bool IsInCombat()
    {
      return inCombat;
    }

    public Enemy GetOpponent()
    {
      return combatEnemy;
    }

    public void FinishedCombat()
    {
      inCombat = false;
    }

    public bool CheckForEnemyCollision()
    {

      foreach (Entity e in EntityList)
      {
        if (e.IsEnemy() && e.StartCombat())
        {
          ((Enemy)e).StopCombat();
          combatEnemy = (Enemy)e;
          return true;
        }
      }
      return false;
    }
  }
}
