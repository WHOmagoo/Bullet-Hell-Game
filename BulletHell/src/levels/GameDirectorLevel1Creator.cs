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
    public class GameDirectorLevel1Creator : IGameFactory
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

//             SCREEN_WIDTH = graphicsDevice.Viewport.Bounds.Width;
//             SCREEN_HEIGHT = graphicsDevice.Viewport.Bounds.Height;


//             int offset = 50;
//             Vector2 topMiddle = new Vector2(SCREEN_WIDTH / 2 - offset, -100);
//             Vector2 topLeft = new Vector2(SCREEN_WIDTH / 4 - offset, -100);
//             Vector2 topRight = new Vector2(3 * SCREEN_WIDTH / 4 - offset, -100);


//             Player player = MakePlayer(controller);
//             //Wave 1 enemies
//             // Enemy e1 = MakeEnemy('a', MOVEMENT.DOWN_RIGHT, topMiddle);
//             // Enemy e1 = MakeEnemy('c', MOVEMENT.DOWN_RIGHT, topMiddle);
//             Enemy e2 = MakeEnemy('a', MOVEMENT.DOWN_RIGHT, topLeft);
//             Enemy e3 = MakeEnemy('a', MOVEMENT.DOWN_LEFT, topRight);
//             Enemy e4 = MakeEnemy('a', MOVEMENT.SIN_DOWN, topMiddle);
//             // Enemy e5 = MakeEnemy('b', MOVEMENT.ZIGZAG_DOWN, topLeft);
//             // //Wave 2 enemies
//             // Enemy e6 = MakeEnemy('a', MOVEMENT.DOWN_RIGHT, topLeft);
//             // Enemy e7 = MakeEnemy('a', MOVEMENT.DOWN_LEFT, topRight);
//             // Enemy e8 = MakeEnemy('c', MOVEMENT.DOWN_RIGHT, topMiddle);
//             // Enemy e9 = MakeEnemy('b', MOVEMENT.ZIGZAG_DOWN, topMiddle);
//             // Enemy e10 = MakeEnemy('b', MOVEMENT.ZIGZAG_DOWN, topRight);
//             // Enemy e11 = MakeEnemy('b', MOVEMENT.ZIGZAG_DOWN, topLeft);
//             // //Wave 3 enemies
//             // Enemy e12 = MakeEnemy('a', MOVEMENT.DOWN_RIGHT, topLeft);
//             // Enemy e13 = MakeEnemy('a', MOVEMENT.DOWN_LEFT, topRight);
//             // Enemy e14 = MakeEnemy('c', MOVEMENT.DOWN_RIGHT, topMiddle);
//             // Enemy e15 = MakeEnemy('b', MOVEMENT.ZIGZAG_DOWN, topMiddle);
//             // Enemy e16 = MakeEnemy('b', MOVEMENT.ZIGZAG_DOWN, topRight);
//             // Enemy e17 = MakeEnemy('b', MOVEMENT.ZIGZAG_DOWN, topLeft);

//             //Bosses
//             // Enemy midboss = MakeMidBoss();
//             // Enemy finalboss = MakeFinalBoss();

//             // HealthBar healthbar = MakeHealthBar();
//             // LifeBar lifebar = MakeLifeBar();


//             // player.OnHit += lifebar.Update;     //update life bar

//             director.addEvent(0, new PlayerEnter(canvas, player));
//             player.DeathEvent += canvas.OnPlayerDeath;

//             director.addEvent(0, new PlayerEnter(canvas, player));
//             /******************Wave 1************************* */
//             // director.addEvent(0, new CreateEnemyEvent(collisionManager, canvas, e1));
//             // director.addEvent(0 * 10000, new CreateEnemyEvent(collisionManager, canvas, e5));
//             director.addEvent(5 * 10000, new CreateEnemyEvent(collisionManager, canvas, e2));
//             director.addEvent(5 * 10000, new CreateEnemyEvent(collisionManager, canvas, e3));
//             director.addEvent(5 * 10000, new CreateEnemyEvent(collisionManager, canvas, e4));
//             /******************Wave 2************************* */
//             // director.addEvent(25 * 10000, new CreateEnemyEvent(collisionManager, canvas, e6));
//             // director.addEvent(25 * 10000, new CreateEnemyEvent(collisionManager, canvas, e7));
//             // director.addEvent(25 * 10000, new CreateEnemyEvent(collisionManager, canvas, e8));
//             // director.addEvent(30 * 10000, new CreateEnemyEvent(collisionManager, canvas, e10));
//             // director.addEvent(30 * 10000, new CreateEnemyEvent(collisionManager, canvas, e11));
//             // /******************MidBoss******************** */
//             // director.addEvent(45 * 10000, new CreateEnemyEvent(collisionManager, canvas, midboss));
//             // /******************Wave 3********************* */
//             // director.addEvent(70 * 10000, new CreateEnemyEvent(collisionManager, canvas, e12));
//             // director.addEvent(70 * 10000, new CreateEnemyEvent(collisionManager, canvas, e13));
//             // director.addEvent(70 * 10000, new CreateEnemyEvent(collisionManager, canvas, e14));
//             // director.addEvent(70 * 10000, new CreateEnemyEvent(collisionManager, canvas, e15));
//             // director.addEvent(70 * 10000, new CreateEnemyEvent(collisionManager, canvas, e16));
//             // /******************Final Boss***************** */
//             // director.addEvent(90 * 10000, new CreateEnemyEvent(collisionManager, canvas, finalboss));
//             // director.addEvent(125 * 10000, new GameWinEvent());

            return new Tuple<GameDirector, Canvas, CollisionManager>(director, canvas, collisionManager);
        }

//         private Enemy MakeEnemy(char enemyType, MOVEMENT movementType, Vector2 startLocation)
//         {
//             Enemy e;

//             GameEngine.Path p = MakePath(movementType, startLocation);
//             if (enemyType == 'a')
//             {
//                 Texture2D enemyATexture = graphicsLoader.getTexture("enemyA");
//                 e = new EnemyA(enemyATexture, p);
//                 e.Hitbox = new CollidingRectangle(e.Location, new Vector2(0, 0), 100, 72);
//             }
//             // else if (enemyType == 'b')
//             // {
//             //     Texture2D enemyBTexure = graphicsLoader.getTexture("enemyB");
//             //     e = new EnemyB(enemyBTexture, startLocation);
//             //     e.SetSize(100, 100);
//             //     e.Hitbox = new CollidingRectangle(e.Location, new Vector2(8, 0), 85, 90);
//             // }
//             // else if (enemyType == 'c')
//             // {
//             //     Texture2D enemyCTexure = graphicsLoader.getTexture("enemyC");
//             //     e = new EnemyC(enemyCTexture, startLocation);
//             //     e.SetSize(150, 150);
//             //     // e.Hitbox = new CollidingRectangle(e.Location, new Vector2(0, 0), 100, 100);
//             //     e.Hitbox = new CollidingCircle(e.Location, new Vector2(e.Rect.Width / 2, e.Rect.Height / 2), 55);

//             // }
//             else
//                 throw new NotImplementedException();

//             e.PropertyChanged += canvas.OnWeaponChange;
//             e.gunEquipped.GunShotHandler += canvas.OnGunShot;

//             return e;
//         }

//         private GameEngine.Path MakePath(MOVEMENT movementType, Vector2 location)
//         {
//             if (movementType == MOVEMENT.DOWN_RIGHT)
//             {
//                 ILocationEquation down = new LinearLocationEquation(Math.PI / 2, .06F);
//                 ILocationEquation stayStill = StayStill.getStayStill();
//                 ILocationEquation right = new LinearLocationEquation(0, .08F);
//                 List<Tuple<ILocationEquation, long>> piecewiseLocationEquations = new List<Tuple<ILocationEquation, long>>();
//                 piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(down, 1000 * 3));
//                 piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(stayStill, 1000 * 7));
//                 piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(right, 1000 * 100));
//                 PiecewiseLocationEquation locationEquation = new PiecewiseLocationEquation(piecewiseLocationEquations);
//                 return new GameEngine.Path(locationEquation, location, 0);
//             }
//             else if (movementType == MOVEMENT.DOWN_LEFT)
//             {
//                 ILocationEquation down = new LinearLocationEquation(Math.PI / 2, .06F);
//                 ILocationEquation stayStill = StayStill.getStayStill();
//                 ILocationEquation left = new LinearLocationEquation(Math.PI, .08F);
//                 List<Tuple<ILocationEquation, long>> piecewiseLocationEquations = new List<Tuple<ILocationEquation, long>>();
//                 piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(down, 1000 * 3));
//                 piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(stayStill, 1000 * 7));
//                 piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(left, 1000 * 100));
//                 PiecewiseLocationEquation locationEquation = new PiecewiseLocationEquation(piecewiseLocationEquations);
//                 return new GameEngine.Path(locationEquation, location, 0);

//             }
//             else if (movementType == MOVEMENT.ZIGZAG_DOWN)
//             {
//                 ILocationEquation zigZag = new ZigZag(Math.PI / 16, .1F, 3000, Math.PI - Math.PI / 16, .1F, 3000);
//                 Path sinPath = new Path(zigZag, location, 0);
//                 return sinPath;
//             }
//             else if (movementType == MOVEMENT.SIN_DOWN)
//             {
//                 ILocationEquation sinEquation = new SinusoidalLocationEquation(10, 200, 25, .0001);
//                 Path sinPath = new Path(sinEquation, location, Math.PI / 2);
//                 return sinPath;
//             }
//             throw new NotImplementedException();
//         }

//         private Player MakePlayer(Controller controller)
//         {
//             Texture2D playerTexture = graphicsLoader.getTexture("player");
//             // Texture2D playerTexture = null;
//             Player player = new Player(canvas, playerTexture, new Vector2(SCREEN_WIDTH / 2 - playerTexture.Width / 2, 300), controller);
//             player.SetSize(72, 100);
//             player.PropertyChanged += canvas.OnWeaponChange;
//             player.gunEquipped.GunShotHandler += canvas.OnGunShot;
//             player.Hitbox = new CollidingCircle(player.Location, new Vector2(player.Rect.Width / 2, player.Rect.Height / 2), 15);
//             collisionManager.addToTeam(player, TEAM.FRIENDLY);
//             return player;
//         }

//         // private Enemy MakeMidBoss()
//         // {
//         //     Texture2D midBossTexture = graphicsLoader.getTexture("midBoss");
//         //     MidBoss midboss = new MidBoss(midBossTexture, new Vector2(100, 5));
//         //     midboss.SetSize(100, 100);
//         //     // midboss.gunEquipped = new BasicShotgun((float) Math.PI / 2, (float) (Math.PI / 9), 1, new LinearLocationEquation((float) -Math.PI / 2, 1), GraphicsLoader.getGraphicsLoader().getBulletTexture(), 2500, TEAM.ENEMY);
//         //     midboss.PropertyChanged += canvas.OnWeaponChange;
//         //     midboss.gunEquipped.GunShotHandler += canvas.OnGunShot;
//         //     midboss.Hitbox = new CollidingRectangle(midboss.Location, new Vector2(0, 0), 100, 100);
//         //     return midboss;
//         // }
//         // private Enemy MakeFinalBoss()
//         // {
//         //     Texture2D finalBossTexture = graphicsLoader.getTexture("finalBoss");
//         //     FinalBoss finalboss = new FinalBoss(finalBossTexture, new Vector2(100, 5));
//         //     finalboss.SetSize(100, 100);
//         //     finalboss.movePattern();
//         //     finalboss.PropertyChanged += canvas.OnWeaponChange;
//         //     finalboss.gunEquipped.GunShotHandler += canvas.OnGunShot;
//         //     finalboss.Hitbox = new CollidingRectangle(finalboss.Location, new Vector2(0, 0), 100, 100);
//         //     return finalboss;
//         // }
//         // private HealthBar MakeHealthBar()
//         // {
//         //     HealthBar healthbar;
//         //     healthbar = new HealthBar(healthBarTexture, new Vector2(400, 10));
//         //     healthbar.SetSize(390, 40);
//         //     canvas.AddToDrawList(healthbar);
//         //     return healthbar;
//         // }
//         // private LifeBar MakeLifeBar()
//         // {
//         //     LifeBar lifebar;
//         //     lifebar = new LifeBar(lifeBarTexture, new Vector2(400, 10));
//         //     lifebar.SetSize(390, 40);
//         //     canvas.AddToDrawList(lifebar);
//         //     return lifebar;
//         // }



    }


}