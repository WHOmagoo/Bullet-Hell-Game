using System;
using System.Collections.Generic;
using BulletHell.bullet.factory;
using BulletHell.character;
using BulletHell.controls;
using BulletHell.director;
using BulletHell.gameEngine;
using BulletHell.graphics;
using BulletHell.gun;
using BulletHell.ObjectCreation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
// using BulletHell.Graphics;
// using Path = BulletHell.GameEngine.Path;

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
        private bool hasCheatMode; 

        public Tuple<GameDirector, Canvas, CollisionManager> makeGame(GraphicsDevice graphicsDevice, Controller controller)
        {
            xmlParser = new XMLParser("test.xml");
            director = new GameDirector();
            canvas = new Canvas(new SpriteBatch(graphicsDevice));
            collisionManager = new CollisionManager();

            Hitbox top = new CollidingRectangle(new Vector2(-50, -100), Vector2.Zero, graphicsDevice.Viewport.Width+100, 50);
            Hitbox bottom = new CollidingRectangle(new Vector2(-50, graphicsDevice.Viewport.Height + 50), Vector2.Zero, graphicsDevice.Viewport.Width+100, 50);
            Hitbox left = new CollidingRectangle(new Vector2(-100,-50), Vector2.Zero, 50, graphicsDevice.Viewport.Height + 100);
            Hitbox right = new CollidingRectangle(new Vector2(graphicsDevice.Viewport.Width + 50,-50), Vector2.Zero, 50, graphicsDevice.Viewport.Height + 100);
            
            BoundingObject bTop = new BoundingObject(null, Vector2.Zero, canvas);
            BoundingObject bBottom = new BoundingObject(null, Vector2.Zero, canvas);
            BoundingObject bLeft = new BoundingObject(null, Vector2.Zero, canvas);
            BoundingObject bRight = new BoundingObject(null, Vector2.Zero, canvas);
            bTop.Hitbox = top;
            bBottom.Hitbox = bottom;
            bLeft.Hitbox = left;
            bRight.Hitbox = right;
            
            collisionManager.addToTeam(bTop, TEAM.UNASSIGNED);
            collisionManager.addToTeam(bBottom, TEAM.UNASSIGNED);
            collisionManager.addToTeam(bLeft, TEAM.UNASSIGNED);
            collisionManager.addToTeam(bRight, TEAM.UNASSIGNED);
            
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
                EncounterEvent encounterEvent = new EncounterEvent(collisionManager, canvas, encounter, director);
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
            player.invulnerable = hasCheatMode;
            director.addEvent(0, new PlayerEnter(canvas, player));
            player.DeathEvent += canvas.OnPlayerDeath;
            
            return new Tuple<GameDirector, Canvas, CollisionManager>(director, canvas, collisionManager);
        }


        private Player MakePlayer(Controller controller)
        {
            Texture2D playerTexture = graphicsLoader.getTexture("player");
            Texture2D heartTexture = graphicsLoader.getTexture("heart");
            // Texture2D playerTexture = null;
            Player player = new Player(canvas, playerTexture, new Vector2(SCREEN_WIDTH / 2 - playerTexture.Width / 2, 300), controller, heartTexture);
            player.SetSize(72, 100);
            player.gunEquipped = new Gun(.01f, GraphicsLoader.getGraphicsLoader().getTexture("player-bullet"),
                BulletFactoryFactory.make("basic"), TEAM.FRIENDLY);
            player.PropertyChanged += canvas.OnWeaponChange;
            player.gunEquipped.GunShotHandler += canvas.OnGunShot;
            player.Hitbox = new CollidingCircle(player.Location, new Vector2(player.Rect.Width / 2, player.Rect.Height / 2), 15);
            collisionManager.addToTeam(player, TEAM.FRIENDLY);
            return player;
        }
        public void setCheatMode(bool hasCheatMode)
        {
            this.hasCheatMode = hasCheatMode;
        }
    }
}