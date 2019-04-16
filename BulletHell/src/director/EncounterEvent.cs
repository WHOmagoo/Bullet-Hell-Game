using System;
using BulletHell.character;
using BulletHell.gameEngine;
using BulletHell.GameEngine;
using BulletHell.graphics;

namespace BulletHell.director
{
    public class EncounterEvent : DirectorEvent
    {
        private Canvas canvas;
        private Enemy enemy;
        private CollisionManager collisionManager;
        
        public EncounterEvent(CollisionManager collisionManager, Canvas canvas, Encounter encounter)
        {
            this.canvas = canvas;
            this.enemy = enemy;
            this.collisionManager = collisionManager;
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