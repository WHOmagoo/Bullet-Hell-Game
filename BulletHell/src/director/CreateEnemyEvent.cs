using BulletHell.GameEngine;

namespace BulletHell.director
{
    public class CreateEnemyEvent : DirectorEvent
    {
        private Canvas game;
        private Enemy enemy;
        
        public CreateEnemyEvent(Canvas game, Enemy enemy)
        {
            this.game = game;
            this.enemy = enemy;
        }

        public override void Execute()
        {
            game.AddToDrawList(enemy);
        }
    }
}