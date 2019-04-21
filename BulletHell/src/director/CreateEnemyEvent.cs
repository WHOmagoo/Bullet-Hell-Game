using System;
using BulletHell.GameEngine;
using BulletHell.Graphics;

namespace BulletHell.director
{
    public class CreateEnemyEvent : DirectorEvent
    {
        private Canvas canvas;
        private Enemy enemy;
        private CollisionManager collisionManager;

        //---------------Set to create object on death

        
        public CreateEnemyEvent(CollisionManager collisionManager, Canvas canvas, Enemy enemy)
        {
            this.canvas = canvas;
            this.enemy = enemy;
            this.collisionManager = collisionManager;
            this.enemy.PropertyChanged += canvas.OnWeaponChange;
            this.enemy.gunEquipped.GunShotHandler += canvas.OnGunShot;
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