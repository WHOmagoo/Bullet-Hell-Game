using System;
using BulletHell.director;
using BulletHell.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.Graphics;
using System.IO;
using System.Collections.Generic;
using BulletHell.controls;
using BulletHell.Pickups;
using Path = BulletHell.GameEngine.Path;

namespace BulletHell.levels
{
    public class TestLevelCreeator : IGameFactory
    {
        private Texture2D playerTexture;
        private Texture2D enemyATexture;
        private Texture2D enemyBTexture;
        private Texture2D midBossTexture;
        private Texture2D finalBossTexture;
        private Texture2D healthBarTexture;
        private Texture2D lifeBarTexture;
        private Texture2D mushroomTexture;
        private Canvas canvas;
        private GameDirector director;
        private int SCREEN_WIDTH;
        private int SCREEN_HEIGHT;
        private Texture2D bulletTexture;
        private CollisionManager collisionManager;

        private enum MOVEMENT { DOWN_RIGHT, DOWN_LEFT }
        
        public Tuple<GameDirector, Canvas, CollisionManager> makeGame(GraphicsDevice graphicsDevice, Controller controller)
        {
            director = new GameDirector();
            canvas = new Canvas(new SpriteBatch(graphicsDevice));

            SCREEN_WIDTH = graphicsDevice.Viewport.Bounds.Width;
            SCREEN_HEIGHT = graphicsDevice.Viewport.Bounds.Height;
            
            collisionManager = new CollisionManager();

            LoadTextures(graphicsDevice);

            Vector2 topMiddle = new Vector2(SCREEN_WIDTH / 2, -100);

            //Testing of sin wave movement
//            Enemy sin = new EnemyA(enemyATexture, topMiddle);
//            ILocationEquation sinEquation = new SinusoidalLocationEquation(16, 200, 25, .0001);
//            Path sinPath = new Path(sinEquation, sin.Location, Math.PI / 2);
//            sin.SetPath(sinPath);
//            sin.Hitbox = new CollidingRectangle(sin.Location, new Vector2(0, 0), 100, 72);
//            SubscribeEnemy(sin);
            
            //Testing of spiral gun
//            Enemy sin = new EnemyA(enemyATexture, new Vector2(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 2));
//            ILocationEquation noMove = StayStill.getStayStill();
//            Path sinPath = new Path(noMove, sin.Location, 0);
//            sin.SetPath(sinPath);
//            sin.gunEquipped = new BasicGun(1, new SpiralLocationEquation(Math.PI / 2 - .3, 10, 50), bulletTexture, 1000, TEAM.ENEMY);
//            sin.Hitbox = new CollidingRectangle(sin.Location, new Vector2(0, 0), 100, 72);
//            SubscribeEnemy(sin);
            
            //Testing of zigzag movement
            ILocationEquation zigZag = new ZigZag(Math.PI / 8, .1F, 1000, Math.PI - Math.PI / 8, .1F, 1000);
            Path sinPath = new Path(zigZag, new Vector2(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 2), 0);
            Enemy sin = new EnemyA(enemyATexture, sinPath);
            sin.gunEquipped = new BasicGun(1, new SpiralLocationEquation(Math.PI / 2 - .3, 10, 50), bulletTexture, 1000, TEAM.ENEMY);
            sin.Hitbox = new CollidingRectangle(sin.Location, new Vector2(0, 0), 100, 72);
            SubscribeEnemy(sin);
            
            Player player = MakePlayer(controller);

            //Pickup testing
            LifePickup pickup = new LifePickup(mushroomTexture, new Vector2(200,200), 80, 80);
            pickup.Hitbox = new CollidingCircle(pickup.Location, new Vector2(pickup.Rect.Width/2, pickup.Rect.Height/2), pickup.Rect.Width/2);
            canvas.AddToDrawList(pickup);
            collisionManager.addToTeam(pickup, TEAM.ENEMY);

            HealthBar healthbar= MakeHealthBar();
            LifeBar lifebar=MakeLifeBar();

            player.OnHit += lifebar.Update;     //update life bar

        director.addEvent(0, new PlayerEnter(canvas, player));
            player.DeathEvent += canvas.OnPlayerDeath;

            director.addEvent(0, new PlayerEnter(canvas, player));
            director.addEvent(0, new CreateEnemyEvent(collisionManager, canvas, sin));
            director.addEvent(125 * 10000, new GameWinEvent());

            return new Tuple<GameDirector, Canvas, CollisionManager>(director, canvas, collisionManager);
        }

        private void SubscribeEnemy(Enemy e)
        {
            e.PropertyChanged += canvas.OnWeaponChange;
            e.gunEquipped.GunShotHandler += canvas.OnGunShot;
        }

        private Player MakePlayer(Controller controller)
        {
            Player player = new Player(canvas, playerTexture, new Vector2(SCREEN_WIDTH / 2 - playerTexture.Width / 2, 300), controller);
            player.SetSize(72, 100);
            player.PropertyChanged += canvas.OnWeaponChange;
            player.gunEquipped.GunShotHandler += canvas.OnGunShot;
            player.Hitbox = new CollidingCircle(player.Location, new Vector2(player.Rect.Width / 2, player.Rect.Height / 2), 15);
            
//          Comment below to make player invulnerable permanently for testing
           collisionManager.addToTeam(player, TEAM.FRIENDLY);
            
            return player;
        }

        private HealthBar MakeHealthBar()
        {
            HealthBar healthbar;
            healthbar = new HealthBar(healthBarTexture, new Vector2(400, 10));
            healthbar.SetSize(390, 40);
            canvas.AddToDrawList(healthbar);
            return healthbar;
        }
        private LifeBar MakeLifeBar()
        {
            LifeBar lifebar;
            lifebar = new LifeBar(lifeBarTexture, new Vector2(400, 10));
            lifebar.SetSize(390, 40);
            canvas.AddToDrawList(lifebar);
            return lifebar;
        }

        private void LoadTextures(GraphicsDevice graphicsDevice)
        {
            playerTexture = Texture2D.FromStream(graphicsDevice,
                new FileStream("Content/sprites/shuttle.png", FileMode.Open));

            enemyATexture = Texture2D.FromStream(graphicsDevice,
                new FileStream("Content/sprites/enemyA.png", FileMode.Open));

            enemyBTexture = Texture2D.FromStream(graphicsDevice,
                new FileStream("Content/sprites/enemyB.png", FileMode.Open));

            midBossTexture = Texture2D.FromStream(graphicsDevice,
                new FileStream("Content/sprites/midboss.png", FileMode.Open));

            finalBossTexture = Texture2D.FromStream(graphicsDevice,
                new FileStream("Content/sprites/finalboss.png", FileMode.Open));

            healthBarTexture = Texture2D.FromStream(graphicsDevice,
                new FileStream("Content/sprites/healthBar.png", FileMode.Open));

            lifeBarTexture = Texture2D.FromStream(graphicsDevice,
                new FileStream("Content/sprites/lifeBar.png", FileMode.Open));
            
            bulletTexture = Texture2D.FromStream(graphicsDevice,
                new FileStream("Content/sprites/bullet.png", FileMode.Open));
            mushroomTexture = Texture2D.FromStream(graphicsDevice,
                new FileStream("Content/sprites/mushroom-1up.png", FileMode.Open));
        }
    }


}