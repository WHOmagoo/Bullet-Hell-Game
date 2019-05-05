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
    public class TestLevelCreator : IGameFactory
    {
        private Canvas canvas;
        private GameDirector director;
        private CollisionManager collisionManager;
        private GraphicsLoader graphicsLoader;
        private Parser xmlParser;
        private int SCREEN_WIDTH;
        private int SCREEN_HEIGHT;
        private bool hasCheatMode = false;

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
            int lastTime = 0;
            foreach(var encounter in encounters)
            {
                EncounterEvent encounterEvent = new EncounterEvent(collisionManager, canvas, encounter, director);
                lastTime = encounter.timeInMS;
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

            //director.addEvent(0, new PlayerEnter(canvas, player));
            
            return new Tuple<GameDirector, Canvas, CollisionManager>(director, canvas, collisionManager);
        }

        private Player MakePlayer(Controller controller)
        {
            Texture2D playerTexture = graphicsLoader.getTexture("player");
            Texture2D heartTexture = graphicsLoader.getTexture("heart");
            // Texture2D playerTexture = null;
            Player player = new Player(canvas, playerTexture, new Vector2(SCREEN_WIDTH / 2 - playerTexture.Width / 2, 300), controller, heartTexture);
            player.SetSize(48, 66);
            player.gunEquipped = new Gun(1, GraphicsLoader.getGraphicsLoader().getBulletTexture(),
                BulletFactoryFactory.make("basic"), TEAM.FRIENDLY, -Math.PI / 2);
            player.PropertyChanged += canvas.OnWeaponChange;
            player.gunEquipped.GunShotHandler += canvas.OnGunShot;
            player.Hitbox = new CollidingCircle(player.Location, new Vector2(player.Rect.Width / 2, player.Rect.Height / 2), 8);
            collisionManager.addToTeam(player, TEAM.FRIENDLY);
            return player;
        }

        public void setCheatMode(bool hasCheatMode)
        {
            this.hasCheatMode = hasCheatMode;
        }
    }
}