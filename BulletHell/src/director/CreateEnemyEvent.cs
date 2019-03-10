using BulletHell.GameEngine;
using BulletHell.Graphics;

namespace BulletHell.director
{
    public class CreateEnemyEvent : DirectorEvent
    {
        private Canvas canvas;
        private Enemy enemy;
        
        public CreateEnemyEvent(Canvas canvas, Enemy enemy)
        {
            this.canvas = canvas;
            this.enemy = enemy;
        }

        public override void Execute()
        {
            canvas.AddToDrawList(enemy);
        }
    }
}