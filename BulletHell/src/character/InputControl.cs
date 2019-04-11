namespace BulletHell.character
{
    class InputControl
    {
        const int moveL = -1;
        const int moveU = -1;
        const int moveD = 1;
        const int moveR = 1;

        private static Player Player;
        
        public static void AssignPlayer(Player player)
        {
            Player = player;
        }
        
        public static void MoveRight()
        {
            Player.speed.X = 1000;
            Player.direction.X = moveR;
        }

        public static void MoveLeft()
        {
            Player.speed.X = 1000;
            Player.direction.X = moveL;
        }

        public static void MoveUp()
        {
            Player.speed.Y = 1000;
            Player.direction.Y = moveU;
        }

        public static void MoveDown()
        {
            Player.speed.Y = 1000;
            Player.direction.Y = moveD;
        }
    }
}