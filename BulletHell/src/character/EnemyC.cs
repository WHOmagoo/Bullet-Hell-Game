// using System;
// using System.Collections.Generic;
// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Graphics;
// using BulletHell.Graphics;

// namespace BulletHell.GameEngine
// {
//     public class EnemyC : Enemy
//     {
//         public EnemyC(Texture2D texture, Vector2 startLocation) : base(texture, startLocation, null, null)
//         {
//             InitializeEnemy();
//         }
//         private void InitializeEnemy()
//         {
//             healthPoints = 10;
//             // this.gunEquipped = new BasicGun(3, new LinearLocationEquation(Math.PI / 2, 1), 
//             //     GraphicsLoader.getGraphicsLoader().getBulletTexture(), 1000, TEAM.ENEMY);
//             // this.gunEquipped = new BasicGun(1, new SpiralLocationEquation(1,1,.1f), 
//            this.gunEquipped = new BasicGun(1, new SpiralLocationEquation(Math.PI / 2 - .1, 10, 50), 
//                 GraphicsLoader.getGraphicsLoader().getBulletTexture(), 2000, TEAM.ENEMY, Math.PI/2);
//         }
//     }
// }