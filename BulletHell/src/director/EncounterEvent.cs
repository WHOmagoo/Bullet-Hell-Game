using System;
using BulletHell.character;
using BulletHell.gameEngine;
using BulletHell.GameEngine;
using BulletHell.graphics;
using Microsoft.Xna.Framework;

namespace BulletHell.director
{
    public class EncounterEvent : DirectorEvent
    {
        private Canvas canvas;
        private Encounter encounter;
        private CollisionManager collisionManager;
        
        public EncounterEvent(CollisionManager collisionManager, Canvas canvas, Encounter encounter)
        {
            this.encounter = encounter;
            this.canvas = canvas;
            this.collisionManager = collisionManager;
        }

        public override void Execute()
        {
            Rectangle screen = canvas.GetBounds();
            PrefabRepo prefabRepo = PrefabRepo.getPrefabRepo();
            Enemy enemy = new Enemy(prefabRepo.getEnemyPrefab(encounter.enemyType), 
                new Vector2(encounter.locationPercentages.X * screen.Width, (1 - encounter.locationPercentages.Y) * screen.Height));

            canvas.AddToDrawList(enemy);
            if (!ReferenceEquals(collisionManager, null))
            {
                collisionManager.addToTeam(enemy, TEAM.ENEMY);
            }
        }
    }
}