using System;
using System.IO;
using BulletHell.bullet.factory;
using BulletHell.character;
using BulletHell.controls;
using BulletHell.director;
using BulletHell.gameEngine;
using BulletHell.graphics;
using BulletHell.gun;
using BulletHell.path;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
// using BulletHell.Graphics;
using System.IO;
using System.Collections.Generic;
using BulletHell.controls;
using BulletHell.ObjectCreation;
// using Path = BulletHell.GameEngine.Path;
using Path = BulletHell.path.Path;

namespace BulletHell.levels
{
    public class LevelCreator : IGameFactory
    {
        private Canvas canvas;
        private GameDirector director;
        private CollisionManager collisionManager;
        private GraphicsLoader graphicsLoader;
        private Parser xmlParser;
        private int SCREEN_WIDTH;
        private int SCREEN_HEIGHT;

        private enum MOVEMENT { DOWN_RIGHT, DOWN_LEFT, ZIGZAG_DOWN, SIN_DOWN }

        public Tuple<GameDirector, Canvas, CollisionManager> makeGame(GraphicsDevice graphicsDevice, Controller controller)
        {
            xmlParser = new XMLParser("test.xml");
            director = new GameDirector();
            canvas = new Canvas(new SpriteBatch(graphicsDevice));
            collisionManager = new CollisionManager();
            try
            {
                graphicsLoader = GraphicsLoader.makeGraphicsLoader(graphicsDevice);
            }
            catch(ArgumentException)
            {
                graphicsLoader = GraphicsLoader.getGraphicsLoader();
            }
            EnemyFactory enemyFactory = new EnemyFactory(); 

            xmlParser.Parse();
            List<Encounter> encounters = xmlParser.getEncounterList();
            foreach(var encounter in encounters)
            {
                EncounterEvent encounterEvent = new EncounterEvent(collisionManager, canvas, encounter);
                Console.WriteLine(encounter.timeInMS);
                director.addEvent(encounter.timeInMS, encounterEvent);
            }


            SCREEN_WIDTH = graphicsDevice.Viewport.Bounds.Width;
            SCREEN_HEIGHT = graphicsDevice.Viewport.Bounds.Height;


            int offset = 50;
            Vector2 topMiddle = new Vector2(SCREEN_WIDTH / 2 - offset, -100);
            Vector2 topLeft = new Vector2(SCREEN_WIDTH / 4 - offset, -100);
            Vector2 topRight = new Vector2(3 * SCREEN_WIDTH / 4 - offset, -100);

            // sin.healthbar = new HealthBar(sin.Location, new Vector2(8, 0), 85, 90, sin.Health);

            Player player = MakePlayer(controller);
            director.addEvent(0, new PlayerEnter(canvas, player));
            player.DeathEvent += canvas.OnPlayerDeath;

            director.addEvent(0, new PlayerEnter(canvas, player));
            
            return new Tuple<GameDirector, Canvas, CollisionManager>(director, canvas, collisionManager);
        }


        private Player MakePlayer(Controller controller)
        {
            Texture2D playerTexture = graphicsLoader.getTexture("player");
            Texture2D heartTexture = graphicsLoader.getTexture("heart");
            // Texture2D playerTexture = null;
            Player player = new Player(canvas, playerTexture, new Vector2(SCREEN_WIDTH / 2 - playerTexture.Width / 2, 300), controller, heartTexture);
            player.SetSize(72, 100);
            player.gunEquipped = new Gun(1, GraphicsLoader.getGraphicsLoader().getBulletTexture(),
                BulletFactoryFactory.make("surround"), TEAM.FRIENDLY);
            player.PropertyChanged += canvas.OnWeaponChange;
            player.gunEquipped.GunShotHandler += canvas.OnGunShot;
            player.Hitbox = new CollidingCircle(player.Location, new Vector2(player.Rect.Width / 2, player.Rect.Height / 2), 15);
            collisionManager.addToTeam(player, TEAM.FRIENDLY);
            return player;
        }
    }
}