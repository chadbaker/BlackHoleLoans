﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackHoleLoans
{
  public class ContentRepository
  {
    static public OverWorld getMap(int index, Game1 g)
    {
      switch (index)
      {
        case 1: // initial test overworld
          return new OverWorld(new int[,]
                       {
                         {1,0,0,0,3,0,3,0},
                         {0,2,0,0,0,0,1,0},
                         {1,0,0,2,0,0,0,0},
                         {0,0,0,0,0,0,0,0},
                         {1,0,4,0,0,0,4,0},
                         {0,0,0,0,0,0,3,0}
                       },
                  new int[,]
                       {
                         {1,0,0,0,0,0,0,0},
                         {0,0,0,0,0,0,0,0},
                         {1,0,0,0,0,0,0,0},
                         {0,0,0,0,0,0,0,0},
                         {1,0,0,0,0,0,0,0},
                         {0,0,0,0,0,0,0,0}
                       }, 2, 2, g);
        case 2: // only grass
          return new OverWorld(new int[,]
                       {
                         {0,0,0,0,0,0,0,0},
                         {0,0,0,0,0,0,0,0},
                         {0,0,0,0,0,0,0,0},
                         {0,0,0,0,0,0,0,0},
                         {0,0,0,0,0,0,0,0},
                         {0,0,0,0,0,0,0,0}
                       },
                  new int[,]
                       {
                         {0,0,0,0,0,0,0,0},
                         {0,0,0,0,0,0,0,0},
                         {0,0,0,0,0,0,0,0},
                         {0,0,0,0,0,0,0,0},
                         {0,0,0,0,0,0,0,0},
                         {0,0,0,0,0,0,0,0}
                       }, 2, 2, g);
        case 3: // dungeon room
          return new OverWorld(new int[,]
                       {
                         {5,5,5,5,5,5,5,5},
                         {5,9,9,9,9,9,9,5},
                         {9,9,9,9,9,9,9,5},
                         {5,9,9,9,9,9,9,5},
                         {5,9,9,9,9,9,9,5},
                         {5,5,5,5,5,5,5,5}
                       },
                  new int[,]
                       {
                         {1,1,1,1,1,1,1,1},
                         {1,0,0,0,0,0,0,1},
                         {0,0,0,0,0,0,0,0},
                         {1,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,1},
                         {1,1,1,1,1,1,1,1}
                       }, 2, 0, g);
        case 4: // big dungeon room
          return new OverWorld(new int[,]
                       {
                         {5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5},
                         {1,6,6,6,6,6,6,6,6,6,6,6,6,6,6,5},
                         {5,6,6,6,6,6,6,6,6,6,6,6,6,6,6,5},
                         {5,6,6,6,6,6,6,6,6,6,6,6,6,6,6,5},
                         {5,6,6,6,6,6,6,6,6,6,6,6,6,6,6,5},
                         {5,6,6,6,6,6,6,6,6,6,6,6,6,6,6,5},
                         {5,6,6,6,6,6,6,6,6,6,6,6,6,6,6,5},
                         {5,6,6,6,6,6,6,6,6,6,6,6,6,6,6,5},
                         {5,6,6,6,6,6,6,6,6,6,6,6,6,6,6,5},
                         {5,6,6,6,6,6,6,6,6,6,6,6,6,6,6,5},
                         {5,6,6,6,6,6,6,6,6,6,6,6,6,6,6,1},
                         {5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5}
                       },
                  new int[,]
                       {
                         {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                         {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                         {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
                       }, 1, 1, g);
        case 5:
          return new OverWorld(new int[,]
                       {
                         {5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5},
                         {5,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                         {5,1,1,1,1,1,1,1,1,1,1,1,1,1,1,5},
                         {5,1,1,1,1,1,1,1,1,1,1,1,1,1,1,5},
                         {5,1,1,1,1,1,1,1,1,1,1,1,1,1,1,5},
                         {5,1,1,1,1,1,1,1,1,1,1,1,1,1,1,5},
                         {5,1,1,1,1,1,1,1,1,1,1,1,1,1,1,5},
                         {5,1,1,1,1,1,1,1,1,1,1,1,1,1,1,5},
                         {5,1,1,1,1,1,1,1,1,1,1,1,1,1,1,5},
                         {5,1,1,1,1,1,1,1,1,1,1,1,1,1,1,5},
                         {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,5},
                         {5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5}
                       },
                  new int[,]
                       {
                         {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                         {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
                       },10,0,g);

        case 6:
          return new OverWorld(new int[,]
                       {
                         {5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5},
                         {1,7,7,7,7,7,7,7,7,7,7,7,7,7,7,1},//door here
                         {5,7,7,7,7,7,7,7,7,7,7,7,7,7,7,5},
                         {5,7,7,7,7,7,7,7,7,7,7,7,7,7,7,5},
                         {5,7,7,7,7,7,7,7,7,7,7,7,7,7,7,5},
                         {5,7,7,7,7,7,7,7,7,7,7,7,7,7,7,5},
                         {5,7,7,7,7,7,7,7,7,7,7,7,7,7,7,5},
                         {5,7,7,7,7,7,7,7,7,7,7,7,7,7,7,5},
                         {5,7,7,7,7,7,7,7,7,7,7,7,7,7,7,5},
                         {5,7,7,7,7,7,7,7,7,7,7,7,7,7,7,5},
                         {5,7,7,7,7,7,7,7,7,7,7,7,7,7,7,1},//door here
                         {5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5}
                       },
                  new int[,]
                       {
                         {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                         {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
                       }, 10, 0, g);

        case 7:
          return new OverWorld(new int[,]
                       {
                         {5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5},
                         {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,5},//door here
                         {5,8,8,8,8,8,8,8,8,8,8,8,8,8,8,5},
                         {5,8,8,8,8,8,8,8,8,8,8,8,8,8,8,5},
                         {5,8,8,8,8,8,8,8,8,8,8,8,8,8,8,5},
                         {5,8,8,8,8,8,8,8,8,8,8,8,8,8,8,5},
                         {5,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},//door here
                         {5,8,8,8,8,8,8,8,8,8,8,8,8,8,8,5},
                         {5,8,8,8,8,8,8,8,8,8,8,8,8,8,8,5},
                         {5,8,8,8,8,8,8,8,8,8,8,8,8,8,8,5},
                         {5,8,8,8,8,8,8,8,8,8,8,8,8,8,8,5},
                         {5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5}
                       },
                  new int[,]
                       {
                         {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                         {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
                       }, 10, 0, g);

        case 8:
          return new OverWorld(new int[,]
                       {
                         {5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5},
                         {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,5},//door here
                         {5,8,8,8,8,8,8,8,8,8,8,8,8,8,8,5},
                         {5,8,8,8,8,8,8,8,8,8,8,8,8,8,8,5},
                         {5,8,8,8,8,8,8,8,8,8,8,8,8,8,8,5},
                         {5,8,8,8,8,8,8,8,8,8,8,8,8,8,8,5},
                         {5,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},//door here
                         {5,8,8,8,8,8,8,8,8,8,8,8,8,8,8,5},
                         {5,8,8,8,8,8,8,8,8,8,8,8,8,8,8,5},
                         {5,8,8,8,8,8,8,8,8,8,8,8,8,8,8,5},
                         {5,8,8,8,8,8,8,8,8,8,8,8,8,8,8,5},
                         {5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5}
                       },
                  new int[,]
                       {
                         {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                         {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
                       }, 10, 0, g);

        default:
          //Console.WriteLine("Tried to get a bad map.");
          return null;
      }
    }

    static public OverWorld getMap(String input, Game1 g)
    {
      switch (input)
      {
        case "test":
          return getMap(1, g);
        case "grassy":
          return getMap(2, g);
        case "dungeon1":
          return getMap(3, g);
        default:
          //Console.WriteLine("Tried to get a bad map.");
          return null;
      }
    }

    static public TileMap getMap(int index)
    {
      switch (index)
      {
        case 1: // initial test overworld
          return new TileMap(new int[,]
                       {
                         {1,0,0,0,3,0,3,0},
                         {0,2,0,0,0,0,1,0},
                         {1,0,0,2,0,0,0,0},
                         {0,0,0,0,0,0,0,0},
                         {1,0,4,0,0,0,4,0},
                         {0,0,0,0,0,0,3,0}
                       },
                  new int[,]
                       {
                         {1,0,0,0,0,0,0,0},
                         {0,0,0,0,0,0,0,0},
                         {1,0,0,0,0,0,0,0},
                         {0,0,0,0,0,0,0,0},
                         {1,0,0,0,0,0,0,0},
                         {0,0,0,0,0,0,0,0}
                       });
        case 2: // only grass
          return new TileMap(new int[,]
                       {
                         {0,0,0,0,0,0,0,0},
                         {0,0,0,0,0,0,0,0},
                         {0,0,0,0,0,0,0,0},
                         {0,0,0,0,0,0,0,0},
                         {0,0,0,0,0,0,0,0},
                         {0,0,0,0,0,0,0,0}
                       },
                  new int[,]
                       {
                         {0,0,0,0,0,0,0,0},
                         {0,0,0,0,0,0,0,0},
                         {0,0,0,0,0,0,0,0},
                         {0,0,0,0,0,0,0,0},
                         {0,0,0,0,0,0,0,0},
                         {0,0,0,0,0,0,0,0}
                       });
        case 3: // dungeon room
          return new TileMap(new int[,]
                       {
                         {5,5,5,5,5,5,5,5},
                         {5,1,1,1,1,1,1,5},
                         {1,1,1,1,1,1,1,1},
                         {5,1,1,1,1,1,1,5},
                         {5,1,1,1,1,1,1,5},
                         {5,5,5,5,5,5,5,5}
                       },
                  new int[,]
                       {
                         {1,1,1,1,1,1,1,1},
                         {1,0,0,0,0,0,0,1},
                         {0,0,0,0,0,0,0,0},
                         {1,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,1},
                         {1,1,1,1,1,1,1,1}
                       });
        case 4: // big dungeon room
          return new TileMap(new int[,]
                       {
                         {5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5},
                         {7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,5},
                         {5,7,7,7,7,7,7,7,7,7,7,7,7,7,7,5},
                         {5,7,7,7,7,7,7,7,7,7,7,7,7,7,7,5},
                         {5,7,7,7,7,7,7,7,7,7,7,7,7,7,7,5},
                         {5,7,7,7,7,7,7,7,7,7,7,7,7,7,7,5},
                         {5,7,7,7,7,7,7,7,7,7,7,7,7,7,7,5},
                         {5,7,7,7,7,7,7,7,7,7,7,7,7,7,7,5},
                         {5,7,7,7,7,7,7,7,7,7,7,7,7,7,7,5},
                         {5,7,7,7,7,7,7,7,7,7,7,7,7,7,7,5},
                         {5,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7},
                         {5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5}
                       },
                  new int[,]
                       {
                         {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                         {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                         {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
                       });

        case 5:
          return new TileMap(new int[,]
                       {
                         {5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5},
                         {5,6,6,6,6,6,6,6,6,6,6,6,6,6,6,1},
                         {5,6,6,6,6,6,6,6,6,6,6,6,6,6,6,5},
                         {5,6,6,6,6,6,6,6,6,6,6,6,6,6,6,5},
                         {5,6,6,6,6,6,6,6,6,6,6,6,6,6,6,5},
                         {5,6,6,6,6,6,6,6,6,6,6,6,6,6,6,5},
                         {5,6,6,6,6,6,6,6,6,6,6,6,6,6,6,5},
                         {5,6,6,6,6,6,6,6,6,6,6,6,6,6,6,5},
                         {5,6,6,6,6,6,6,6,6,6,6,6,6,6,6,5},
                         {5,6,6,6,6,6,6,6,6,6,6,6,6,6,6,5},
                         {1,6,6,6,6,6,6,6,6,6,6,6,6,6,6,5},
                         {5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5}
                       },
                  new int[,]
                       {
                         {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
                       });
        case 6:
          return new TileMap(new int[,]
                       {
                         {5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5},
                         {5,7,7,7,7,7,7,7,7,7,7,7,7,7,7,1},//door here
                         {5,7,7,7,7,7,7,7,7,7,7,7,7,7,7,5},
                         {5,7,7,7,7,7,7,7,7,7,7,7,7,7,7,5},
                         {5,7,7,7,7,7,7,7,7,7,7,7,7,7,7,5},
                         {5,7,7,7,7,7,7,7,7,7,7,7,7,7,7,5},
                         {5,7,7,7,7,7,7,7,7,7,7,7,7,7,7,5},
                         {5,7,7,7,7,7,7,7,7,7,7,7,7,7,7,5},
                         {5,7,7,7,7,7,7,7,7,7,7,7,7,7,7,5},
                         {5,7,7,7,7,7,7,7,7,7,7,7,7,7,7,5},
                         {1,7,7,7,7,7,7,7,7,7,7,7,7,7,7,1},//door here
                         {5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5}
                         
                       },
                  new int[,]
                       {
                         {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
                       });
        case 7:
          return new TileMap(new int[,]
                       {
                         {7,9,7,9,7,9,9,9,9,7,9,7,7,9,7,7},
                         {7,9,7,9,7,9,7,7,9,7,9,7,7,9,7,7},
                         {7,7,9,7,7,9,7,7,9,7,9,7,7,9,7,7},
                         {7,7,9,7,7,9,9,9,9,7,9,9,9,9,7,7},
                         {7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7},
                         {1,7,7,7,7,7,7,7,7,7,7,7,7,7,9,7},
                         {7,9,7,7,7,9,7,9,7,9,7,7,9,7,9,7},
                         {7,9,7,7,7,9,7,9,7,9,9,7,9,7,9,7},
                         {7,9,7,9,7,9,7,9,7,9,7,9,9,7,9,7},
                         {7,9,9,9,9,9,7,9,7,9,7,7,9,7,7,7},
                         {7,7,7,7,7,7,7,7,7,7,7,7,7,7,9,7},
                         {7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7}
                       },
                  new int[,]
                       {
                         {0,1,0,1,0,1,1,1,1,0,1,0,0,1,0,0},
                         {0,1,0,1,0,1,0,0,1,0,1,0,0,1,0,0},
                         {0,0,1,0,0,1,0,0,1,0,1,0,0,1,0,0},
                         {0,0,1,0,0,1,1,1,1,0,1,1,1,1,0,0},
                         {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                         {0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0},
                         {0,1,0,0,0,1,0,1,0,1,0,0,1,0,1,0},
                         {0,1,0,0,0,1,0,1,0,1,1,0,1,0,1,0},
                         {0,1,0,1,0,1,0,1,0,1,0,1,1,0,1,0},
                         {0,1,1,1,1,1,0,1,0,1,0,0,1,0,0,0},
                         {0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0},
                         {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
                       });
        case 8:
          return new TileMap(new int[,]
                       {
                         {5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5},
                         {5,8,8,8,8,8,8,8,8,8,8,8,8,8,8,5},//door here
                         {5,8,8,8,8,8,8,8,8,8,8,8,8,8,8,5},
                         {5,8,8,8,8,8,8,8,8,8,8,8,8,8,8,5},
                         {5,8,8,8,8,8,8,8,8,8,8,8,8,8,8,5},
                         {5,8,8,8,8,8,8,8,8,8,8,8,8,8,8,5},
                         {5,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},//door here
                         {5,8,8,8,8,8,8,8,8,8,8,8,8,8,8,5},
                         {5,8,8,8,8,8,8,8,8,8,8,8,8,8,8,5},
                         {5,8,8,8,8,8,8,8,8,8,8,8,8,8,8,5},
                         {5,8,8,8,8,8,8,8,8,8,8,8,8,8,8,5},
                         {5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5}
                       },
                  new int[,]
                       {
                         {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                         {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
                       });

        default:
          //Console.WriteLine("Tried to get a bad map.");
          return null;
      }


    }

    static public TileMap getMap(string input)
    {
      switch (input)
      {
        case "test":
          return getMap(1);
        case "grassy":
          return getMap(2);
        case "dungeon1":
          return getMap(3);
        case "large_dungeon1":
          return getMap(4);
        default:
          //Console.WriteLine("Tried to get a bad map.");
          return null;
      }
    }
  }
}