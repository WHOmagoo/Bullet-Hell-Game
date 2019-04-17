using System;
using BulletHell.director;
using BulletHell.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.Graphics;
using System.IO;
using System.Collections.Generic;
using BulletHell.controls;
using BulletHell.ObjectCreation;
using BulletHell.Pickups;
using Path = BulletHell.GameEngine.Path;

namespace BulletHell.levels
{
    public class TestLevelCreator : IGameFactory
    {
        private Canvas canvas;
        private GameDirector director;
        private CollisionManager collisionManager;
        private GraphicsLoader graphicsLoader;
        private int SCREEN_WIDTH;
        private int SCREEN_HEIGHT;

        private enum MOVEMENT { DOWN_RIGHT, DOWN_LEFT, ZIGZAG_DOWN, SIN_DOWN }

        public Tuple<GameDirector, Canvas, CollisionManager> makeGame(GraphicsDevice graphicsDevice, Controller controller)
        {
            director = new GameDirector();
            canvas = new Canvas(new SpriteBatch(graphicsDevice));
            collisionManager = new CollisionManager();
            graphicsLoader = GraphicsLoader.makeGraphicsLoader(graphicsDevice);
            EnemyFactory enemyFactory = new EnemyFactory();

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

            PathData pData1 = new PathData("linear", 4000, 0, .1);
            PathData pData2 = new PathData("sinusoidal", 5000, 3 * Math.PI / 2, 1);
            PathData pData3 = new PathData("linear", 4000, 3 * Math.PI / 2, .1);
            List<PathData> pData = new List<PathData>();
            pData.Add(pData1);
            pData.Add(pData2);
            pData.Add(pData3);
            Enemy e1 = enemyFactory.makeEnemy("enemyA", 1, new Vector2(50, 50), pData1, null);
            Enemy e2 = enemyFactory.makeEnemy("enemyA", 1, new Vector2(50, 50), pData1, null);
            Enemy e3 = enemyFactory.makeEnemy("enemyA", 1, new Vector2(50, 50), pData3, null);
            Enemy e4 = enemyFactory.makeEnemy("enemyA", 1, new Vector2(50, 50), pData3, null);


            director.addEvent(0, new PlayerEnter(canvas, player));
            /******************Wave 1************************* */
            director.addEvent(0, new CreateEnemyEvent(collisionManager, canvas, e1));
            director.addEvent(10 * 1000, new CreateEnemyEvent(collisionManager, canvas, e2));
            //director.addEvent(10 * 1000, new CreateEnemyEvent(collisionManager, canvas, e3));
            //director.addEvent(15 * 1000, new CreateEnemyEvent(collisionManager, canvas, e4));

            //FastPickup fp = MakeFastPickup();
            //DamagePickup dp = MakeDamagePickup();
            //director.addEvent(20 * 1000, new CreateFastPickupEvent(collisionManager, canvas, fp));
            //director.addEvent(1000, new CreateDamagePickupEvent(collisionManager, canvas, dp));


            return new Tuple<GameDirector, Canvas, CollisionManager>(director, canvas, collisionManager);
        }


        private Player MakePlayer(Controller controller)
        {
            Texture2D playerTexture = graphicsLoader.getTexture("player");
            Texture2D heartTexture = graphicsLoader.getTexture("heart");
            // Texture2D playerTexture = null;
            Player player = new Player(canvas, playerTexture, new Vector2(SCREEN_WIDTH / 2 - playerTexture.Width / 2, 300), controller, heartTexture);
            player.SetSize(72, 100);
            player.PropertyChanged += canvas.OnWeaponChange;
            player.gunEquipped.GunShotHandler += canvas.OnGunShot;
            player.Hitbox = new CollidingCircle(player.Location, new Vector2(player.Rect.Width / 2, player.Rect.Height / 2), 15);
            collisionManager.addToTeam(player, TEAM.FRIENDLY);
            return player;
        }

        private FastPickup MakeFastPickup()
        {
            Texture2D fastTexture = graphicsLoader.getTexture("fastPickup");
            FastPickup fp = new FastPickup(fastTexture, new Vector2(200, 200), 80, 80);
            fp.Hitbox = new CollidingCircle(fp.Location, new Vector2(fp.Rect.Width / 2, fp.Rect.Height / 2), 15);
            collisionManager.addToTeam(fp, TEAM.ENEMY);
            return fp;
        }

        private DamagePickup MakeDamagePickup()
        {
            Texture2D dmgTexture = graphicsLoader.getTexture("dmgPickup");
            DamagePickup dp = new DamagePickup(dmgTexture, new Vector2(100, 400), 80, 80);
            dp.Hitbox = new CollidingCircle(dp.Location, new Vector2(dp.Rect.Width / 2, dp.Rect.Height / 2), 15);
            collisionManager.addToTeam(dp, TEAM.ENEMY);
            return dp;
        }
    }
}