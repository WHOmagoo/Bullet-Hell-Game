using System;
using System.IO;
using BulletHell.director;
using BulletHell.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.Graphics;

namespace BulletHell.levels
{
    public class GameDirectorLevel1Creator : IGameFactory
    {
        public Tuple<GameDirector, Canvas> makeGame(GraphicsDevice graphicsDevice)
        {
            GameDirector director = new GameDirector();
            Canvas canvas = new Canvas(new SpriteBatch(graphicsDevice));
            
            Texture2D playerTexture = Texture2D.FromStream(graphicsDevice,
                new FileStream("Content/sprites/shuttle.png", FileMode.Open));

            Texture2D enemyATexture = Texture2D.FromStream(graphicsDevice,
                new FileStream("Content/sprites/enemyA.png", FileMode.Open));

            Texture2D enemyBTexture = Texture2D.FromStream(graphicsDevice,
                new FileStream("Content/sprites/enemyB.png", FileMode.Open));
            
            Player player = new Player(canvas, playerTexture, new Vector2(graphicsDevice.Viewport.Bounds.Width / 2 - playerTexture.Width / 2, 300));
            player.SetSize(72, 100);
            player.PropertyChanged += canvas.OnWeaponChange;
            player.gunEquipped.GunShotHandler += canvas.OnGunShot;
            
            Enemy enemy1 = new EnemyA(enemyATexture, new Vector2(graphicsDevice.Viewport.Bounds.Width / 2 - enemyATexture.Width / 2, -100));
            enemy1.PropertyChanged += canvas.OnWeaponChange;
            enemy1.gunEquipped.GunShotHandler += canvas.OnGunShot;
            
            Enemy enemy2 = new EnemyB(enemyBTexture, new Vector2(graphicsDevice.Viewport.Bounds.Width / 2 - 50, -100));
            enemy2.SetSize(100, 100);
            enemy2.PropertyChanged += canvas.OnWeaponChange;
            enemy2.gunEquipped.GunShotHandler += canvas.OnGunShot;


            Texture2D midBossTexture = Texture2D.FromStream(graphicsDevice,
                new FileStream("Content/sprites/midboss.png", FileMode.Open));
            // midboss = new MidBoss(midBossTexture, new Vector2(100,5), 100, 100);
            MidBoss midboss = new MidBoss(midBossTexture, new Vector2(100, 5));
            midboss.SetSize(100, 100);
            midboss.PropertyChanged += canvas.OnWeaponChange;
            midboss.gunEquipped.GunShotHandler += canvas.OnGunShot;


            Texture2D finalBossTexture = Texture2D.FromStream(graphicsDevice,
                new FileStream("Content/sprites/finalboss.png", FileMode.Open));
            // finalboss = new FinalBoss(finalBossTexture, new Vector2(100, 5), 100, 100);
            FinalBoss finalboss = new FinalBoss(finalBossTexture, new Vector2(100, 5));
            finalboss.SetSize(100, 100);
            finalboss.movePattern();
            finalboss.PropertyChanged += canvas.OnWeaponChange;
            finalboss.gunEquipped.GunShotHandler += canvas.OnGunShot;

            
            director.addEvent(0, new CreateEnemyEvent(canvas, enemy1));
            director.addEvent(0, new PlayerEnter(canvas, player));
            director.addEvent(15 * 10000, new CreateEnemyEvent(canvas, enemy2));
            director.addEvent(44 * 10000, new CreateEnemyEvent(canvas, midboss));
            director.addEvent(80 * 10000, new CreateEnemyEvent(canvas, finalboss));
            
            return new Tuple<GameDirector, Canvas>(director, canvas);
        }
    }


}