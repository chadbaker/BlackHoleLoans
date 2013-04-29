using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackHoleLoans
{
  class Door : Entity
  {
    public Door sister;
    public TileMap destination
    {
      get;
      set;
    }



    public Door(OverWorld ow, Tile t, int f, OverWorld newdestination, Door newsister)
      : base(ow, t, f)
    {
      destination = newdestination.OWmap;
      sister = newsister;
    }

    public Door(OverWorld ow, Tile t, int f, TileMap newdestination, Door newsister)
      : base(ow, t, f)
    {
      destination = newdestination;
      sister = newsister;
    }

    public override void OnInteract()
    {
      //Console.Write("Player has moved through a door!\n");
      overworld.OWmap = destination;
      Tile target = overworld.getAdjacent(sister.tile, sister.Facing);
      overworld.Xpos = target.getX();
      overworld.Ypos = target.getY();
      overworld.Facing = sister.Facing;
    }
  }
}
