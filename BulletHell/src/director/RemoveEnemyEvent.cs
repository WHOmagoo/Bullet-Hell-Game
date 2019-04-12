using System;
using BulletHell.GameEngine;
using BulletHell.Graphics;

namespace BulletHell.director
{
    public class RemoveEnemyEvent : DirectorEvent
    {
        private Canvas canvas;
        private Enemy enemy;
        private CollisionManager collisionManager;

        public RemoveEnemyEvent(CollisionManager collisionManager, Canvas canvas, Enemy enemy)
        {
            this.canvas = canvas;
            this.enemy = enemy;
            this.collisionManager = collisionManager;
        }

        public override void Execute()
        {
            enemy.ResetPath();
            canvas.RemoveFromDrawList(enemy);
            if (!ReferenceEquals(collisionManager, null))
            {
                collisionManager.removeFromTeam(enemy, TEAM.ENEMY);
            }
        }

    }
}