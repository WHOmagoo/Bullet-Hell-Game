using System;
using BulletHell.director;
using BulletHell.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.Graphics;
using System.IO;
using System.Collections.Generic;

namespace BulletHell.levels
{
    public class GameDirectorLevel1Creator : IGameFactory
    {
        private Texture2D playerTexture;
        private Texture2D enemyATexture;
        private Texture2D enemyBTexture;
        private Canvas canvas;
        private GameDirector director;
        private CollisionManager collisionManager;
        public Tuple<GameDirector, Canvas, CollisionManager> makeGame(GraphicsDevice graphicsDevice)
        {
            director = new GameDirector();
            canvas = new Canvas(new SpriteBatch(graphicsDevice));
            collisionManager = new CollisionManager();

            playerTexture = Texture2D.FromStream(graphicsDevice,
                new FileStream("Content/sprites/shuttle.png", FileMode.Open));

            enemyATexture = Texture2D.FromStream(graphicsDevice,
                new FileStream("Content/sprites/enemyA.png", FileMode.Open));

            enemyBTexture = Texture2D.FromStream(graphicsDevice,
                new FileStream("Content/sprites/enemyB.png", FileMode.Open));

            Player player = new Player(canvas, playerTexture, new Vector2(graphicsDevice.Viewport.Bounds.Width / 2 - playerTexture.Width / 2, 300));
            player.SetSize(72, 100);
            player.PropertyChanged += canvas.OnWeaponChange;
            player.gunEquipped.GunShotHandler += canvas.OnGunShot;
            player.Hitbox = new CollidingCircle(player.Location, new Vector2(player.Rect.Width / 2, player.Rect.Height / 2), 15);
            collisionManager.addToTeam(player, TEAM.FRIENDLY);

            Vector2 topMiddle = new Vector2(graphicsDevice.Viewport.Bounds.Width / 2, -100);

            // Enemy e1 = MakeEnemy('a', 0, new Vector2(500,0));
            Enemy e1 = MakeEnemy('a', 0, topMiddle);
            Enemy e2 = MakeEnemy('a', 0, new Vector2(0,0));

            // Enemy enemy2 = new EnemyB(enemyBTexture, new Vector2(graphicsDevice.Viewport.Bounds.Width / 2 - 50, -100));
            // enemy2.SetSize(100, 100);
            // enemy2.PropertyChanged += canvas.OnWeaponChange;
            // enemy2.gunEquipped.GunShotHandler += canvas.OnGunShot;
            // collisionManager.addToTeam(enemy1, TEAM.ENEMY);

            Texture2D midBossTexture = Texture2D.FromStream(graphicsDevice,
                new FileStream("Content/sprites/midboss.png", FileMode.Open));
            // midboss = new MidBoss(midBossTexture, new Vector2(100,5), 100, 100);
            MidBoss midboss = new MidBoss(midBossTexture, new Vector2(100, 5));
            midboss.SetSize(100, 100);
            midboss.PropertyChanged += canvas.OnWeaponChange;
            midboss.gunEquipped.GunShotHandler += canvas.OnGunShot;
            // collisionManager.addToTeam(midboss, TEAM.ENEMY);


            Texture2D finalBossTexture = Texture2D.FromStream(graphicsDevice,
                new FileStream("Content/sprites/finalboss.png", FileMode.Open));
            // finalboss = new FinalBoss(finalBossTexture, new Vector2(100, 5), 100, 100);
            FinalBoss finalboss = new FinalBoss(finalBossTexture, new Vector2(100, 5));
            finalboss.SetSize(100, 100);
            finalboss.movePattern();
            finalboss.PropertyChanged += canvas.OnWeaponChange;
            finalboss.gunEquipped.GunShotHandler += canvas.OnGunShot;
            // collisionManager.addToTeam(finalboss, TEAM.ENEMY);


            director.addEvent(0, new CreateEnemyEvent(collisionManager, canvas, e1));
            director.addEvent(0, new PlayerEnter(canvas, player));
            director.addEvent(15 * 10000, new CreateEnemyEvent(collisionManager, canvas, e2));
            director.addEvent(44 * 10000, new CreateEnemyEvent(collisionManager, canvas, midboss));
            director.addEvent(80 * 10000, new CreateEnemyEvent(collisionManager, canvas, finalboss));

            return new Tuple<GameDirector, Canvas, CollisionManager>(director, canvas, collisionManager);
        }

        private Enemy MakeEnemy(char enemyType, int movementType, Vector2 startLocation)
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
                e.SetSize(100,100);
                e.Hitbox = new CollidingRectangle(e.Location, new Vector2(0, 0), 100, 100);
            }
            else
                throw new NotImplementedException();

            e.PropertyChanged += canvas.OnWeaponChange;
            e.gunEquipped.GunShotHandler += canvas.OnGunShot;
            
            GameEngine.Path p = MakePath(0, startLocation);
            e.SetPath(p);
            return e;
        }

        private GameEngine.Path MakePath(int movementType, Vector2 location)
        {
            if (movementType == 0)
            {
                ILocationEquation down = new LinearLocationEquation(Math.PI / 2, .04F);
                ILocationEquation stayStill = StayStill.getStayStill();
                ILocationEquation right = new LinearLocationEquation(0, .04F);
                List<Tuple<ILocationEquation, long>> piecewiseLocationEquations = new List<Tuple<ILocationEquation, long>>();
                piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(down, 1000 * 5));
                piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(stayStill, 1000 * 7));
                piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(right, 1000 * 100));
                PiecewiseLocationEquation locationEquation = new PiecewiseLocationEquation(piecewiseLocationEquations);
                return new GameEngine.Path(locationEquation, location, 0);
            }
            else if (movementType == 1)
            {

            }
            throw new NotImplementedException();
        }
    }


}