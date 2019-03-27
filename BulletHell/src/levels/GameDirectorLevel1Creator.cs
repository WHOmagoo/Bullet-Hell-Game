using System;
using BulletHell.director;
using BulletHell.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.Graphics;
using System.IO;
using System.Collections.Generic;
using BulletHell.controls;
using Path = BulletHell.GameEngine.Path;

namespace BulletHell.levels
{
    public class GameDirectorLevel1Creator : IGameFactory
    {
        private Texture2D playerTexture;
        private Texture2D enemyATexture;
        private Texture2D enemyBTexture;
        private Texture2D midBossTexture;
        private Texture2D finalBossTexture;
        private Texture2D healthBarTexture;
        private Texture2D lifeBarTexture;
        private Texture2D bulletTexture;
        private Canvas canvas;
        private GameDirector director;
        private CollisionManager collisionManager;
        private int SCREEN_WIDTH;
        private int SCREEN_HEIGHT;

        private enum MOVEMENT { DOWN_RIGHT, DOWN_LEFT }
        
        public Tuple<GameDirector, Canvas, CollisionManager> makeGame(GraphicsDevice graphicsDevice, Controller controller)
        {
            director = new GameDirector();
            canvas = new Canvas(new SpriteBatch(graphicsDevice));
            collisionManager = new CollisionManager();

            SCREEN_WIDTH = graphicsDevice.Viewport.Bounds.Width;
            SCREEN_HEIGHT = graphicsDevice.Viewport.Bounds.Height;

            LoadTextures(graphicsDevice);

            Vector2 topMiddle = new Vector2(SCREEN_WIDTH / 2, -100);

            //Testing of sin wave movement
            Enemy sin = new EnemyA(enemyATexture, topMiddle);
            sin.PropertyChanged += canvas.OnWeaponChange;
            sin.gunEquipped.GunShotHandler += canvas.OnGunShot;
            ILocationEquation sinEquation = new SinusoidalLocationEquation(16, 200, 25, .0001);
            Path sinPath = new Path(sinEquation, sin.Location, Math.PI / 2);
            sin.SetPath(sinPath);
            sin.Hitbox = new CollidingRectangle(sin.Location, new Vector2(0, 0), 100, 72);
            
            
            Player player = MakePlayer(controller);
            Enemy e1 = MakeEnemy('a', MOVEMENT.DOWN_RIGHT, topMiddle);
            Enemy e2 = MakeEnemy('a', MOVEMENT.DOWN_RIGHT, new Vector2(SCREEN_WIDTH / 4, -100));
            Enemy e3 = MakeEnemy('a', MOVEMENT.DOWN_LEFT, new Vector2(3 * SCREEN_WIDTH / 4, -100));
            Enemy midboss = MakeMidBoss();
            Enemy finalboss = MakeFinalBoss();

            HealthBar healthbar= MakeHealthBar();
            LifeBar lifebar=MakeLifeBar();

            player.OnHit += lifebar.Update;     //update life bar

        director.addEvent(0, new PlayerEnter(canvas, player));
            player.DeathEvent += canvas.OnPlayerDeath;

            director.addEvent(0, new PlayerEnter(canvas, player));
            director.addEvent(0, new CreateEnemyEvent(collisionManager, canvas, sin));
            director.addEvent(0, new CreateEnemyEvent(collisionManager, canvas, e1));
            director.addEvent(5 * 10000, new CreateEnemyEvent(collisionManager, canvas, e2));
            director.addEvent(5 * 10000, new CreateEnemyEvent(collisionManager, canvas, e3));
            director.addEvent(20 * 10000, new CreateEnemyEvent(collisionManager, canvas, midboss));
            director.addEvent(40 * 10000, new CreateEnemyEvent(collisionManager, canvas, finalboss));
            director.addEvent(125 * 10000, new GameWinEvent());

            return new Tuple<GameDirector, Canvas, CollisionManager>(director, canvas, collisionManager);
        }

        private Enemy MakeEnemy(char enemyType, MOVEMENT movementType, Vector2 startLocation)
        {
            Enemy e;
            if (enemyType == 'a')
            {
                e = new EnemyA(enemyATexture, startLocation);
                e.Hitbox = new CollidingRectangle(e.Location, new Vector2(0, 0), 100, 72);
            }
            else if (enemyType == 'b')
            {
                e = new EnemyB(enemyBTexture, startLocation);
                e.SetSize(100, 100);
                e.Hitbox = new CollidingRectangle(e.Location, new Vector2(0, 0), 100, 100);
            }
            else
                throw new NotImplementedException();

            e.PropertyChanged += canvas.OnWeaponChange;
            e.gunEquipped.GunShotHandler += canvas.OnGunShot;

            GameEngine.Path p = MakePath(movementType, startLocation);
            e.SetPath(p);
            return e;
        }

        private GameEngine.Path MakePath(MOVEMENT movementType, Vector2 location)
        {
            if (movementType == MOVEMENT.DOWN_RIGHT)
            {
                ILocationEquation down = new LinearLocationEquation(Math.PI / 2, .06F);
                ILocationEquation stayStill = StayStill.getStayStill();
                ILocationEquation right = new LinearLocationEquation(0, .08F);
                List<Tuple<ILocationEquation, long>> piecewiseLocationEquations = new List<Tuple<ILocationEquation, long>>();
                piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(down, 1000 * 3));
                piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(stayStill, 1000 * 7));
                piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(right, 1000 * 100));
                PiecewiseLocationEquation locationEquation = new PiecewiseLocationEquation(piecewiseLocationEquations);
                return new GameEngine.Path(locationEquation, location, 0);
            }
            else if (movementType == MOVEMENT.DOWN_LEFT)
            {
                ILocationEquation down = new LinearLocationEquation(Math.PI / 2, .06F);
                ILocationEquation stayStill = StayStill.getStayStill();
                ILocationEquation left = new LinearLocationEquation(Math.PI, .08F);
                List<Tuple<ILocationEquation, long>> piecewiseLocationEquations = new List<Tuple<ILocationEquation, long>>();
                piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(down, 1000 * 3));
                piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(stayStill, 1000 * 7));
                piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(left, 1000 * 100));
                PiecewiseLocationEquation locationEquation = new PiecewiseLocationEquation(piecewiseLocationEquations);
                return new GameEngine.Path(locationEquation, location, 0);

            }
            throw new NotImplementedException();
        }

        private Player MakePlayer(Controller controller)
        {
            Player player = new Player(canvas, playerTexture, new Vector2(SCREEN_WIDTH / 2 - playerTexture.Width / 2, 300), controller);
            player.SetSize(72, 100);
            player.PropertyChanged += canvas.OnWeaponChange;
            player.gunEquipped.GunShotHandler += canvas.OnGunShot;
            player.Hitbox = new CollidingCircle(player.Location, new Vector2(player.Rect.Width / 2, player.Rect.Height / 2), 15);
            collisionManager.addToTeam(player, TEAM.FRIENDLY);
            return player;
        }

        private Enemy MakeMidBoss()
        {
            MidBoss midboss = new MidBoss(midBossTexture, new Vector2(100, 5));
            midboss.SetSize(100, 100);
            midboss.gunEquipped = new BasicShotgun((float) Math.PI / 2, (float) (Math.PI / 9), 1, new LinearLocationEquation((float) -Math.PI / 2, 1), GraphicsLoader.getGraphicsLoader().getBulletTexture(), 2500, TEAM.ENEMY);
            midboss.PropertyChanged += canvas.OnWeaponChange;
            midboss.gunEquipped.GunShotHandler += canvas.OnGunShot;
            midboss.Hitbox = new CollidingRectangle(midboss.Location, new Vector2(0,0), 100, 100);
            return midboss;
        }
        private Enemy MakeFinalBoss()
        {
            FinalBoss finalboss = new FinalBoss(finalBossTexture, new Vector2(100, 5));
            finalboss.SetSize(100, 100);
            finalboss.movePattern();
            finalboss.PropertyChanged += canvas.OnWeaponChange;
            finalboss.gunEquipped.GunShotHandler += canvas.OnGunShot;
            finalboss.Hitbox = new CollidingRectangle(finalboss.Location, new Vector2(0,0), 100, 100);
            return finalboss;
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
        }
    }


}