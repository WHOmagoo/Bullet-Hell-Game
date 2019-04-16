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
            PrefabRepo prefabRepo = PrefabRepo.getPrefabRepo();
            Enemy enemy = new Enemy(prefabRepo.getEnemyPrefab(encounter.enemyType));
            enemy.ResetPath();
            canvas.AddToDrawList(enemy);
            if (!ReferenceEquals(collisionManager, null))
            {
                collisionManager.addToTeam(enemy, TEAM.ENEMY);
            }
        }
    }
}