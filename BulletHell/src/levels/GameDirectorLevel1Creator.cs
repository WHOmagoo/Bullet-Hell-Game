using BulletHell.director;
using BulletHell.GameEngine;
using BulletHell.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;

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
        private Canvas canvas;
        private GameDirector director;
        private CollisionManager collisionManager;
        private int SCREEN_WIDTH;
        private int SCREEN_HEIGHT;

        private enum MOVEMENT { DOWN_RIGHT, DOWN_LEFT, FINALBOSSMOVEMENT }
        public Tuple<GameDirector, Canvas, CollisionManager> makeGame(GraphicsDevice graphicsDevice)
        {
            director = new GameDirector();
            canvas = new Canvas(new SpriteBatch(graphicsDevice));
            collisionManager = new CollisionManager();

            SCREEN_WIDTH = graphicsDevice.Viewport.Bounds.Width;
            SCREEN_HEIGHT = graphicsDevice.Viewport.Bounds.Height;

            LoadTextures(graphicsDevice);

            Vector2 topMiddle = new Vector2(SCREEN_WIDTH / 2, -100);

            Player player = MakePlayer();
            Enemy e1 = MakeEnemy('a', MOVEMENT.DOWN_RIGHT, topMiddle);
            Enemy e2 = MakeEnemy('a', MOVEMENT.DOWN_RIGHT, new Vector2(SCREEN_WIDTH / 4, -100));
            Enemy e3 = MakeEnemy('a', MOVEMENT.DOWN_LEFT, new Vector2(3 * SCREEN_WIDTH / 4, -100));
            Enemy midboss = MakeMidBoss(MOVEMENT.DOWN_RIGHT, new Vector2(100, 5));
            Enemy finalboss = MakeFinalBoss(MOVEMENT.FINALBOSSMOVEMENT, new Vector2(100, 5));

            HealthBar healthbar = MakeHealthBar();
            LifeBar lifebar = MakeLifeBar();

            HealthBar e1_healthbar = MakeEnemyHealthBar(MOVEMENT.DOWN_RIGHT, new Vector2(SCREEN_WIDTH / 2, -10));
            LifeBar e1_lifebar = MakeEnemyLifeBar(MOVEMENT.DOWN_RIGHT, new Vector2(SCREEN_WIDTH / 2, -10));

            HealthBar e2_healthbar = MakeEnemyHealthBar(MOVEMENT.DOWN_RIGHT, new Vector2(SCREEN_WIDTH / 4, -10));
            LifeBar e2_lifebar = MakeEnemyLifeBar(MOVEMENT.DOWN_RIGHT, new Vector2(SCREEN_WIDTH / 4, -10));

            HealthBar e3_healthbar = MakeEnemyHealthBar(MOVEMENT.DOWN_LEFT, new Vector2(3 * SCREEN_WIDTH / 4, -10));
            LifeBar e3_lifebar = MakeEnemyLifeBar(MOVEMENT.DOWN_LEFT, new Vector2(3 * SCREEN_WIDTH / 4, -10));

            HealthBar midboss_healthbar = MakeEnemyHealthBar(MOVEMENT.DOWN_RIGHT, new Vector2(100, 100));
            LifeBar midboss_lifebar = MakeEnemyLifeBar(MOVEMENT.DOWN_RIGHT, new Vector2(100, 100));

            HealthBar finalboss_healthbar = MakeEnemyHealthBar(MOVEMENT.FINALBOSSMOVEMENT, new Vector2(100, 100));
            LifeBar finalboss_lifebar = MakeEnemyLifeBar(MOVEMENT.FINALBOSSMOVEMENT, new Vector2(100, 100));


            player.OnHit += lifebar.Update;     //update life bar
            e1.OnHit += e1_lifebar.Update;
            e1.DeathEvent += e1_healthbar.isDead;
            e1.DeathEvent += e1_lifebar.isDead;

            e2.OnHit += e2_lifebar.Update;
            e2.DeathEvent += e2_healthbar.isDead;
            e2.DeathEvent += e2_lifebar.isDead;

            e3.OnHit += e3_lifebar.Update;
            e3.DeathEvent += e3_healthbar.isDead;
            e3.DeathEvent += e3_lifebar.isDead;

            midboss.OnHit += midboss_lifebar.Update;
            midboss.DeathEvent += midboss_healthbar.isDead;
            midboss.DeathEvent += midboss_lifebar.isDead;

            finalboss.OnHit += finalboss_lifebar.Update;
            finalboss.DeathEvent += finalboss_healthbar.isDead;
            finalboss.DeathEvent += finalboss_lifebar.isDead;

            director.addEvent(0, new PlayerEnter(canvas, player));
            player.DeathEvent += canvas.OnPlayerDeath;

            director.addEvent(0, new PlayerEnter(canvas, player));
            director.addEvent(0, new CreateEnemyEvent(collisionManager, canvas, e1));
            director.addEvent(0, new CreateHealthbarEvent(canvas, e1_healthbar, e1_lifebar));
            director.addEvent(5 * 10000, new CreateEnemyEvent(collisionManager, canvas, e2));
            director.addEvent(5 * 10000, new CreateHealthbarEvent(canvas, e2_healthbar, e2_lifebar));
            director.addEvent(5 * 10000, new CreateEnemyEvent(collisionManager, canvas, e3));
            director.addEvent(5 * 10000, new CreateHealthbarEvent(canvas, e3_healthbar, e3_lifebar));
            director.addEvent(20 * 10000, new CreateEnemyEvent(collisionManager, canvas, midboss));
            director.addEvent(20 * 10000, new CreateHealthbarEvent(canvas, midboss_healthbar, midboss_lifebar));
            director.addEvent(40 * 10000, new CreateEnemyEvent(collisionManager, canvas, finalboss));
            director.addEvent(40 * 10000, new CreateHealthbarEvent(canvas, finalboss_healthbar, finalboss_lifebar));
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
            else if (movementType == MOVEMENT.FINALBOSSMOVEMENT)
            {
                int i = 0;
                ILocationEquation down = new LinearLocationEquation(Math.PI / 2, .06F);
                ILocationEquation up = new LinearLocationEquation(Math.PI * -1 / 2, .06F);
                ILocationEquation left = new LinearLocationEquation(Math.PI, .08F);
                ILocationEquation right = new LinearLocationEquation(0, .08F);
                ILocationEquation stayStill = StayStill.getStayStill();
                List<Tuple<ILocationEquation, long>> piecewiseLocationEquations = new List<Tuple<ILocationEquation, long>>();
                piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(down, 1000 * 3));
                piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(right, 1000 * 3));
                piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(up, 1000 * 3));
                piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(left, 1000 * 3));
                for (; i < 150; i++)
                {
                    piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(down, 20));
                    piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(right, 20));
                }
                for (i=0; i < 100; i++)
                {
                    piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(up, 20));
                    piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(left, 20));
                }
                for (i=0; i < 50; i++)
                {
                    piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(right, 70));
                    piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(down, 70));
                    piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(right, 70));
                    piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(up, 70));
                    piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(right, 70));
                    piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(down, 70));
                    piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(right, 70));
                    piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(up, 70));

                }
                piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(up, 1000));
                PiecewiseLocationEquation locationEquation = new PiecewiseLocationEquation(piecewiseLocationEquations);
                return new GameEngine.Path(locationEquation, location, 0);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private Player MakePlayer()
        {
            Player player = new Player(canvas, playerTexture, new Vector2(SCREEN_WIDTH / 2 - playerTexture.Width / 2, 300));
            player.SetSize(72, 100);
            player.PropertyChanged += canvas.OnWeaponChange;
            player.gunEquipped.GunShotHandler += canvas.OnGunShot;
            player.Hitbox = new CollidingCircle(player.Location, new Vector2(player.Rect.Width / 2, player.Rect.Height / 2), 15);
            collisionManager.addToTeam(player, TEAM.FRIENDLY);
            return player;
        }

        private Enemy MakeMidBoss(MOVEMENT movementType, Vector2 startLocation)
        {
            MidBoss midboss = new MidBoss(midBossTexture, startLocation);
            midboss.SetSize(100, 100);
            GameEngine.Path p = MakePath(movementType, startLocation);
            midboss.SetPath(p);
            midboss.gunEquipped = new BasicShotgun((float)Math.PI / 2, (float)(Math.PI / 9), 1, new LinearLocationEquation((float)-Math.PI / 2, 1), GraphicsLoader.getGraphicsLoader().getBulletTexture(), 2500, TEAM.ENEMY);
            midboss.PropertyChanged += canvas.OnWeaponChange;
            midboss.gunEquipped.GunShotHandler += canvas.OnGunShot;
            midboss.Hitbox = new CollidingRectangle(midboss.Location, new Vector2(0, 0), 100, 100);
            return midboss;
        }
        private Enemy MakeFinalBoss(MOVEMENT movementType, Vector2 startLocation)
        {
            FinalBoss finalboss = new FinalBoss(finalBossTexture, startLocation);
            finalboss.SetSize(100, 100);
            GameEngine.Path p = MakePath(movementType, startLocation);
            finalboss.SetPath(p);
            finalboss.movePattern();
            finalboss.PropertyChanged += canvas.OnWeaponChange;
            finalboss.gunEquipped.GunShotHandler += canvas.OnGunShot;
            finalboss.Hitbox = new CollidingRectangle(finalboss.Location, new Vector2(0, 0), 100, 100);
            return finalboss;
        }
        private HealthBar MakeHealthBar()
        {
            HealthBar healthbar;
            ILocationEquation stayStill = StayStill.getStayStill();
            healthbar = new HealthBar(healthBarTexture, new Vector2(400, 10), new GameEngine.Path(stayStill, new Vector2(400, 10), 0));
            healthbar.SetSize(390, 40);
            canvas.AddToDrawList(healthbar);
            return healthbar;
        }
        private HealthBar MakeEnemyHealthBar(MOVEMENT movementType, Vector2 startLocation)
        {
            HealthBar healthbar;
            GameEngine.Path p = MakePath(movementType, startLocation);
            healthbar = new HealthBar(healthBarTexture, startLocation,p);
            healthbar.SetSize(100, 10);
            
            //canvas.AddToDrawList(healthbar);
            return healthbar;
        }
        private LifeBar MakeLifeBar()
        {
            LifeBar lifebar;
            ILocationEquation stayStill = StayStill.getStayStill();
            lifebar = new LifeBar(lifeBarTexture, new Vector2(400, 10), new GameEngine.Path(stayStill, new Vector2(400, 10), 0),390,40);
            lifebar.SetSize(390, 40);
            canvas.AddToDrawList(lifebar);
            return lifebar;
        }
        private LifeBar MakeEnemyLifeBar(MOVEMENT movementType, Vector2 startLocation)
        {
            LifeBar lifebar;
            GameEngine.Path p = MakePath(movementType, startLocation);
            lifebar = new LifeBar(lifeBarTexture, new Vector2(400, 10),p,100,10);
            lifebar.SetSize(100,10);
            //canvas.AddToDrawList(lifebar);
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
        }
    }


}
