using BulletHell.GameEngine;

namespace BulletHell.director
{
    public class PlayerEnter : DirectorEvent
    {
        private Canvas canvas;
        private Player player;
        
        public PlayerEnter(Canvas canvas, Player player)
        {
            this.canvas = canvas;
            this.player = player;
        }

        public override void Execute()
        {
            canvas.AddToDrawList(player);
        }
    }
}