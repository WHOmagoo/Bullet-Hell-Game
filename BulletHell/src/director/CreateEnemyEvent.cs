using System;
using BulletHell.character;
using BulletHell.gameEngine;
using BulletHell.graphics;
using BulletHell.GameEngine;

namespace BulletHell.director
{
    public class CreateEnemyEvent : DirectorEvent
    {
        private Canvas canvas;
        private Enemy enemy;
        private CollisionManager collisionManager;
        
        public CreateEnemyEvent(CollisionManager collisionManager, Canvas canvas, Enemy enemy)
        {
            this.canvas = canvas;
            this.enemy = enemy;
            this.collisionManager = collisionManager;

            this.enemy.gunEquipped.GunShotHandler += this.canvas.OnGunShot;


        }

        public override void Execute()
        {
            enemy.ResetPath();
            canvas.AddToDrawList(enemy);
            if (!ReferenceEquals(collisionManager, null))
            {
                collisionManager.addToTeam(enemy, TEAM.ENEMY);
            }
        }
    }
}