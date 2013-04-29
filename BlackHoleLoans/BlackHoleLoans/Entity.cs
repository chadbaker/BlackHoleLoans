using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BlackHoleLoans
{
  public class Entity
  {
    public List<Texture2D> EntityAvatar
    {
      get;
      set;
    }
    public string[] AvatarFileString //used for instantiation;
    {
      get;
      set;
    }
    protected OverWorld overworld;
    public Tile tile
    {
      get;
      set;  //not yet safe
    }
    public int Facing
    {
      get;
      set;
    }
    public Entity() {/* this is a bad idea to use, but the debugger was bugging me.*/ }
    public Entity(OverWorld ow, Tile t)
    {
      overworld = ow;
      if (t.entity == null)
      {
        tile = t;
        t.entity = this;
      }
      else
      {
        //Console.Write("Error, attempted to make entity in occupied tile");
        tile = null;
      }
      Facing = 0;
      EntityAvatar = new List<Texture2D>();
    }
    public Entity(OverWorld ow, Tile t, int f)
    {
      overworld = ow;
      if (t.entity == null)
      {
        tile = t;
        t.entity = this;
      }
      else
      {
        //Console.Write("Error, attempted to make entity in occupied tile");
        tile = null;
      }
      Facing = f;
      EntityAvatar = new List<Texture2D>();
    }

    public void LoadEntityAvatar(ContentManager content, params string[] fileNames)
    {
      Texture2D text;

      foreach (string fileName in fileNames)
      {
        text = content.Load<Texture2D>(fileName);
        EntityAvatar.Add(text);
      }
    }

    public void LoadEntityAvatar(ContentManager content)
    {
      Texture2D text;

      foreach (string fileName in AvatarFileString)
      {
        text = content.Load<Texture2D>(fileName);
        EntityAvatar.Add(text);
      }
    }

    virtual public void OnCollision()
    {
      //Console.Write("An entity has collided with a player.\n");
    }
    virtual public void OnInteract()
    {
      //Console.Write("The player interacted with an entity.\n");
    }
    virtual public void OnUpdate()
    {

    }

    protected bool MoveTo(Tile t)
    { //does not check collision with player, check before you call it
      if (t != tile && t.entity == null)
      {
        t.entity = this;
        tile.entity = null;
        tile = t;
        return true;
      }
      else
      {
        return false;
      }
    }

    public bool MoveAdjacent(int direction)
    {
      Facing = direction;
      Tile temp = overworld.getAdjacent(tile, direction);
      if (temp == overworld.getCurrent())
      {
        this.OnCollision();
        return false;
      }
      else
      {
        return MoveTo(temp);
      }
    }

    public void setAvatarFileString(params string[] fileNames)
    {
      if (fileNames == null)
      {
        //Console.Write("Invalid input to setAvatarFileString.\n");
      }
      else
      {
        AvatarFileString = new string[fileNames.Length];
        for (int i = 0; i < fileNames.Length; i++)
        {
          AvatarFileString[i] = fileNames[i];
        }
      }
    }

    public virtual bool remove()
    {
      tile.entity = null;
      overworld.EntityList.Remove(this);
      return true; // should return true if entity is "safe" to remove, other entities might return false if not safe.
    }

    public virtual bool IsEnemy()
    {
      return false;
    }

    public virtual bool StartCombat()
    {
      return false;
    }
  }
}
