using System;
using System.Collections.Generic;
using System.Linq;
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
  /// <summary>
  /// This is the main type for your game
  /// </summary>
  public class Game1 : Microsoft.Xna.Framework.Game
  {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    MainMenu mainMenu;
    CharacterCreation characterCreation;
    OverWorld OW;
    OWMenu ingameMenu;
    Player[] party;
    int currentGameState;

    //Eric's code start
    int OWcontrolspeed = 4;
    int OWentityspeed = 2;
    int action_timer = 0;
    int player_timer=0, enemy_timer = 0;
    //End

    //Chad
    public Combat combat;
    public Enemy[] enemy;
    //end chad
    bool createdCombat, pausedGame;
    int combatBackgroundID;
    bool paralyzed = false;//Player ran away -> used to paralyzed enemy for a tiny bit
    KeyboardState keyState, prevKeystate;

    public Game1()
    {
      graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";

      graphics.PreferredBackBufferWidth = 800;
      graphics.PreferredBackBufferHeight = 600;
      IsMouseVisible = true;

      mainMenu = new MainMenu(Content, graphics.PreferredBackBufferWidth,
        graphics.PreferredBackBufferHeight);
      characterCreation = new CharacterCreation();

      currentGameState = 0;//change back to 0

      createdCombat = pausedGame = false;
      keyState = Keyboard.GetState();
      prevKeystate = keyState;
    }
    /// <summary>
    /// Allows the game to perform any initialization it needs to before starting to run.
    /// This is where it can query for any required services and load any non-graphic
    /// related content.  Calling base.Initialize will enumerate through any components
    /// and initialize them as well.
    /// </summary>
    protected override void Initialize()
    {
      // TODO: Add your initialization logic here

      base.Initialize();
    }

    /// <summary>
    /// LoadContent will be called once per game and is the place to load
    /// all of your content.
    /// </summary>
    protected override void LoadContent()
    {
      // Create a new SpriteBatch, which can be used to draw textures.
      spriteBatch = new SpriteBatch(GraphicsDevice);
      // TODO: use this.Content to load your game content here
      mainMenu.setSpriteBatch(spriteBatch);
      characterCreation.setSpriteBatch(spriteBatch);

      mainMenu.LoadContent();
      characterCreation.LoadContent(Content);
    }

    protected void LoadOverWorldContent()
    {

      //Eric's code start
      OW.LoadTileTextures(Content, "Textures/grass", "Textures/dirt", "Textures/ground",
            "Textures/mud", "Textures/road", "Textures/bricks", "Textures/blackYellowFloor",
            "Textures/brownFloor", "Textures/redFloor", "Textures/grayFloor");
      OW.LoadAvatar(Content, (party[0].GetPlayerSprites()));

      foreach (TileMap map in OW.mapList)
      {
        foreach (Entity current in map.EntityList)
        {
          current.LoadEntityAvatar(Content);
        }
      }
      //end

    }

    private void CreateMainMenu()
    {
      mainMenu = new MainMenu(Content, graphics.PreferredBackBufferWidth,
         graphics.PreferredBackBufferHeight);
    }

    private void CreateCharacterCreation()
    {
      characterCreation = new CharacterCreation();
    }

    private void CreateOverWorld()
    {
      //Eric's code start

      OW = ContentRepository.getMap(3, this);
      //OWlist = new List<OverWorld>();
      //OWlist.Add(OW);
      graphics.PreferredBackBufferWidth = 800;
      graphics.PreferredBackBufferHeight = 600;

      TileMap tempTileMap = ContentRepository.getMap(4);
      TileMap tempTileMap2 = ContentRepository.getMap(5);
      TileMap tempTileMap3 = ContentRepository.getMap(8);
      TileMap tempTileMap4 = ContentRepository.getMap(7);
      OW.mapList.Add(tempTileMap);
      OW.mapList.Add(tempTileMap2);
      OW.mapList.Add(tempTileMap3);
      OW.mapList.Add(tempTileMap4);

      #region Enemy Creation

      Entity enemy1 = new Enemy(OW, tempTileMap.getTile(5, 5), 0, new int[] { 0, 1, 2, 3 }, "Blue Spider");
      enemy1.setAvatarFileString("EnemySprites/BlueCreatureRight", "EnemySprites/BlueCreatureRight",
        "EnemySprites/BlueCreatureLeft", "EnemySprites/BlueCreatureLeft");
      tempTileMap.EntityList.Add(enemy1);

      Entity enemy2 = new Enemy(OW, tempTileMap.getTile(8, 8), 0, new int[] { 2, 3, 0, 1 }, "Blue Spider");
      enemy2.setAvatarFileString("EnemySprites/BlueCreatureRight", "EnemySprites/BlueCreatureRight",
        "EnemySprites/BlueCreatureLeft", "EnemySprites/BlueCreatureLeft");
      tempTileMap.EntityList.Add(enemy2);

      Entity enemy3 = new Enemy(OW, tempTileMap2.getTile(8, 8), 0, 
        new int[] { 1, 1, 1, 1, 3, 3, 3, 3 }, 
        "Purple Spider");
      enemy3.setAvatarFileString("EnemySprites/PurpleSpiderRight", "EnemySprites/PurpleSpiderRight",
        "EnemySprites/PurpleSpiderLeft", "EnemySprites/PurpleSpiderLeft");
      tempTileMap2.EntityList.Add(enemy3);


      Entity enemy4 = new Enemy(OW, tempTileMap2.getTile(5, 4), 0,
  new int[] { 0, 0, 2, 2, 2, 2, 0, 0 },
  "Purple Spider");
      enemy4.setAvatarFileString("EnemySprites/PurpleSpiderRight", "EnemySprites/PurpleSpiderRight",
        "EnemySprites/PurpleSpiderLeft", "EnemySprites/PurpleSpiderLeft");
      tempTileMap2.EntityList.Add(enemy4);


      Entity enemy5 = new Enemy(OW, tempTileMap3.getTile(8, 14), 0,
new int[] { 0, 0, 0, 0, 2, 2, 2, 2 },
"Purple Monster");
      enemy5.setAvatarFileString("EnemySprites/PurpleMonsterRight", "EnemySprites/PurpleMonsterRight",
        "EnemySprites/PurpleMonsterLeft", "EnemySprites/PurpleMonsterLeft");
      tempTileMap3.EntityList.Add(enemy5);

      Entity enemy6 = new Enemy(OW, tempTileMap3.getTile(4, 13), 0,
new int[] { 2, 2, 2, 2, 0, 0, 0, 0},
"Purple Monster");
      enemy6.setAvatarFileString("EnemySprites/PurpleMonsterRight", "EnemySprites/PurpleMonsterRight",
        "EnemySprites/PurpleMonsterLeft", "EnemySprites/PurpleMonsterLeft");
      tempTileMap3.EntityList.Add(enemy6);


      #endregion

      #region Map Creation (includes doors)

      Entity temp;

      temp = new Door(OW, OW.OWmap.getTile(2, 7), 3, tempTileMap, null);
      temp.setAvatarFileString("EntityAvatar/Door/Door_0", "EntityAvatar/Door/Door_1",
          "EntityAvatar/Door/Door_2", "EntityAvatar/Door/Door_3");
      Door tempSister = new Door(OW, tempTileMap.getTile(1, 0), 1, OW.OWmap, (Door)temp);
      tempSister.setAvatarFileString("EntityAvatar/Door/Door_0", "EntityAvatar/Door/Door_1",
          "EntityAvatar/Door/Door_2", "EntityAvatar/Door/Door_3");
      ((Door)temp).sister = tempSister;
      OW.EntityList.Add(temp);
      tempTileMap.EntityList.Add(tempSister);
      //end


      Entity temp2 = new Door(OW, tempTileMap.getTile(10, 15), 3, tempTileMap2, null);//10 15
      temp2.setAvatarFileString("EntityAvatar/Door/Door_0", "EntityAvatar/Door/Door_1",
     "EntityAvatar/Door/Door_2", "EntityAvatar/Door/Door_3");

      Door tempSister2 = new Door(OW, tempTileMap2.getTile(10, 0), 1, tempTileMap, (Door)temp2);
      tempSister2.setAvatarFileString("EntityAvatar/Door/Door_0", "EntityAvatar/Door/Door_1",
          "EntityAvatar/Door/Door_2", "EntityAvatar/Door/Door_3");
      ((Door)temp2).sister = tempSister2;
      tempTileMap.EntityList.Add(temp2);
      tempTileMap2.EntityList.Add(tempSister2);



      Entity temp3 = new Door(OW, tempTileMap2.getTile(1, 15), 3, tempTileMap3, null);//10 15
      temp3.setAvatarFileString("EntityAvatar/Door/Door_0", "EntityAvatar/Door/Door_1",
     "EntityAvatar/Door/Door_2", "EntityAvatar/Door/Door_3");

      Door tempSister3 = new Door(OW, tempTileMap3.getTile(10, 0), 1, tempTileMap2, (Door)temp3);
      tempSister3.setAvatarFileString("EntityAvatar/Door/Door_0", "EntityAvatar/Door/Door_1",
          "EntityAvatar/Door/Door_2", "EntityAvatar/Door/Door_3");
      ((Door)temp3).sister = tempSister3;
      tempTileMap2.EntityList.Add(temp3);
      tempTileMap3.EntityList.Add(tempSister3);



      Entity temp4 = new Door(OW, tempTileMap3.getTile(6, 15), 3, tempTileMap4, null);//10 15
      temp4.setAvatarFileString("EntityAvatar/Door/Door_0", "EntityAvatar/Door/Door_1",
     "EntityAvatar/Door/Door_2", "EntityAvatar/Door/Door_3");

      Door tempSister4 = new Door(OW, tempTileMap4.getTile(5, 0), 1, tempTileMap3, (Door)temp4);
      tempSister4.setAvatarFileString("EntityAvatar/Door/Door_0", "EntityAvatar/Door/Door_1",
          "EntityAvatar/Door/Door_2", "EntityAvatar/Door/Door_3");
      ((Door)temp4).sister = tempSister4;
      tempTileMap3.EntityList.Add(temp4);
      tempTileMap4.EntityList.Add(tempSister4);

      #endregion
    }

    private void CreateCombat(Enemy e)
    {
      PlayerStatistics playerStats = party[0].GetPlayerStats();
      int[] pToE = new int[4] //player to enemy stats
      {
        playerStats.Attack, playerStats.Defence, playerStats.Concentration, playerStats.Health
      };

      e.SetEnemySprites();
      Texture2D enemySprite = e.EnemySprite();
      string enemyName = e.Name;
      /*
      enemy = new Enemy[3]
            {
                new Enemy(pToE[0]-5,pToE[1]-5,pToE[2]-5,pToE[3]-75, enemyName+" 1", enemySprite),//Can also add skills
                new Enemy(pToE[1]-3,pToE[1]-3,pToE[2]-3,pToE[3]-50, enemyName+" 2", enemySprite),
                new Enemy(pToE[0]-1,pToE[1]-1,pToE[2]-1,pToE[3]-25, enemyName+" 3", enemySprite)
            };
       * */
      enemy = new Enemy[3]
            {
                new Enemy(pToE[0]-5,pToE[1]-5,pToE[2]-5,100, enemyName+" 1", enemySprite),//Can also add skills
                new Enemy(pToE[1]-3,pToE[1]-3,pToE[2]-3,100, enemyName+" 2", enemySprite),
                new Enemy(pToE[0]-1,pToE[1]-1,pToE[2]-1,100, enemyName+" 3", enemySprite)
            };//Change health back to normal

      combat = new Combat(Content, graphics.PreferredBackBufferHeight,
          graphics.PreferredBackBufferWidth, this, party, enemy);
      combat.LoadContent();
      combat.SetSpriteBatch(spriteBatch);

      combatBackgroundID = OW.OWmap.getTile(OW.Xpos, OW.Ypos).Texture;

    }

    private void RestartFromMainMenu()
    {
      CreateMainMenu();
      CreateCharacterCreation();
      CreateOverWorld();
      LoadContent();
    }

    /// <summary>
    /// UnloadContent will be called once per game and is the place to unload
    /// all content.
    /// </summary>
    /// 
    protected override void UnloadContent()
    {
      // TODO: Unload any non ContentManager content here
    }

    /// <summary>
    /// Allows the game to run logic such as updating the world,
    /// checking for collisions, gathering input, and playing audio.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Update(GameTime gameTime)
    {
      // Allows the game to exit
      if (Keyboard.GetState().IsKeyDown(Keys.Escape))
        this.Exit();
      // TODO: Add your update logic here
      keyState = Keyboard.GetState();


      #region main menu
      if (currentGameState == 0)
      {
        currentGameState = mainMenu.Update();
        if (currentGameState == -1)
          this.Exit();
      }
      #endregion

      #region Character Creation

      else if (currentGameState == 1)
      {
        characterCreation.Update();
        currentGameState = characterCreation.BackToMM();
        if (characterCreation.StartOverworld())
        {
          party = characterCreation.CreatePlayer();
          currentGameState = 2;
          CreateOverWorld();
          LoadOverWorldContent();
        }
      }
      #endregion

      #region OW

      else if (currentGameState == 2)//Overworld change back to 2
      {
        if (keyState.IsKeyDown(Keys.M))
        {
          currentGameState = 4;
        }
        //action_timer++;
        enemy_timer++;
        player_timer++;

        if (action_timer >= 60)//change back to 60!!!
        {
          action_timer = 0;
        }
        if(enemy_timer >= 60){
          enemy_timer = 0;
        }
        if (player_timer >= 60)
        {
          player_timer = 0;
        }
        //Eric's code start
        //Console.WriteLine("Player x position"+OW.Xpos+" Player y position"+OW.Ypos);
        #region Tapping directional keys
        if (prevKeystate.IsKeyUp(Keys.Up) && keyState.IsKeyDown(Keys.Up))
        {
          this.OW.playerStep(0);
          //action_timer = 0;
          player_timer = 0;
        }

        else if (prevKeystate.IsKeyUp(Keys.Right) && keyState.IsKeyDown(Keys.Right))
        {
          this.OW.playerStep(1);
          //action_timer = 0;
          player_timer = 0;
        }

        else if (prevKeystate.IsKeyUp(Keys.Down) && keyState.IsKeyDown(Keys.Down))
        {
          this.OW.playerStep(2);
          player_timer = 0;
          //action_timer = 0;
        }

        else if (prevKeystate.IsKeyUp(Keys.Left) && keyState.IsKeyDown(Keys.Left))
        {
          this.OW.playerStep(3);
          player_timer = 0;
          //action_timer = 0;
        }
        #endregion

        #region Holding down directional keys
        else if (player_timer % (60 / OWcontrolspeed) == 0)
        {
          if (keyState.IsKeyDown(Keys.Up))
          {
            this.OW.playerStep(0);
          }
          else if (keyState.IsKeyDown(Keys.Right))
          {
            this.OW.playerStep(1);
          }
          else if (keyState.IsKeyDown(Keys.Down))
          {
            this.OW.playerStep(2);
          }
          else if (keyState.IsKeyDown(Keys.Left))
          {
            this.OW.playerStep(3);
          }
          else
          {
            player_timer = 0;
          }
          if (keyState.IsKeyDown(Keys.Z))
          {
            this.OW.playerInteract();
          }
          //end
        }
        #endregion

        if (enemy_timer % (60 / OWentityspeed) == 0)
        {
          foreach (Entity current in OW.EntityList)
          {
            current.OnUpdate();
          }
        }
        OW.Draw(spriteBatch, graphics);

        if (OW.IsInCombat() || OW.CheckForEnemyCollision())
        {
          currentGameState = 3;
        }


      }
      #endregion

      #region Combat

      else if (currentGameState == 3)//Combat
      {
        if (!createdCombat)
        {
          CreateCombat(OW.GetOpponent());
          createdCombat = true;
        }

        combat.Update(gameTime);

        if (combat.WonFight() && combat.messageQueue.Count==0)
        {
          currentGameState = 2;
          OW.GetOpponent().remove();//added
          OW.FinishedCombat();

          foreach (Player player in party)
          {
            player.isFainted = false;
            player.GetPlayerStats().FullHeal();
            player.hasGone = false;
          }
          createdCombat = false;
        }
          
        else if (combat.LostFight())
        {
          if (combat.messageQueue.Count == 0)
          {
            RestartFromMainMenu();
            currentGameState = 0;
            createdCombat = false;
          }
        }

        else if (combat.RanAway())
        {
          currentGameState = 2;
          OW.GetOpponent().ParalyzeEnemy();
          OW.FinishedCombat();
          createdCombat = false;

          foreach (Player player in party)
          {
            player.isFainted = false;
            player.GetPlayerStats().FullHeal();
            player.hasGone = false;
          }
        }
      }

      #endregion

      #region OWMenu
      else if (currentGameState == 4)//In-game menu
      {
        if (!pausedGame)
        {
          ingameMenu = new OWMenu(party);
          ingameMenu.LoadContent(Content);
          pausedGame = true;
        }
        currentGameState = ingameMenu.Update(this);
        if (currentGameState == 0)
          RestartFromMainMenu();
        if (currentGameState != 4)
        {
          pausedGame = false;
        }
      }
      #endregion
      //Eric's Code start
      else
      {
        //Console.WriteLine("Error, Unknown gamestate reached: gamestate = " + currentGameState);
        this.Exit();
      }
      //End

      prevKeystate = keyState;
      base.Update(gameTime);
    }

 
    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
      //GraphicsDevice.Clear(Color.Brown);

      // TODO: Add your drawing code here
      spriteBatch.Begin();

      if (currentGameState == 0)
      {
        mainMenu.Draw();
      }
      else if (currentGameState == 1)
      {
        characterCreation.Draw();
      }
      else if (currentGameState == 2)//change back to 2
      {
        spriteBatch.End();
        OW.Draw(spriteBatch, graphics);

      }

      else if (currentGameState == 3)
      {
        if (createdCombat)
          combat.Draw(gameTime, combatBackgroundID);
      }

      else if (currentGameState == 4)
      {
        if (pausedGame)
          ingameMenu.Draw(spriteBatch);
      }

      if (currentGameState != 2)//change back to 2
        spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
