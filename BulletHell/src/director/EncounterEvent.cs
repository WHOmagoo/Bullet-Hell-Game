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

        private GameDirector director;
        
        public EncounterEvent(CollisionManager collisionManager, Canvas canvas, Encounter encounter, GameDirector director)
        {
            this.encounter = encounter;
            this.canvas = canvas;
            this.collisionManager = collisionManager;
            this.director = director;
        }

        public override void Execute()
        {
            Rectangle screen = canvas.GetBounds();
            PrefabRepo prefabRepo = PrefabRepo.getPrefabRepo();
            Enemy enemy;

            if(!encounter.isBoss){
                enemy = new Enemy(prefabRepo.getEnemyPrefab(encounter.enemyType), 
                    new Vector2(encounter.locationPercentages.X * screen.Width, (1 - encounter.locationPercentages.Y) * screen.Height));
            }
            else{
                Console.WriteLine("making boss");
                Boss boss = new Boss(prefabRepo.getEnemyPrefab(encounter.enemyType), 
                    new Vector2(encounter.locationPercentages.X * screen.Width, (1 - encounter.locationPercentages.Y) * screen.Height));
                boss.BossDeathEvent += director.OnBossDeath;
                enemy = boss;
            }

            canvas.AddToDrawList(enemy);
            if (!ReferenceEquals(collisionManager, null))
            {
                collisionManager.addToTeam(enemy, TEAM.ENEMY);
            }
        }
    }
}