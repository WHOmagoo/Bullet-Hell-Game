namespace BulletHellTests.GameEngine
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
            Player.speed.X = 1;
            Player.direction.X = moveR;
        }

        public static void MoveLeft()
        {
            Player.speed.X = 1;
            Player.direction.X = moveL;
        }

        public static void MoveUp()
        {
            Player.speed.Y = 1;
            Player.direction.Y = moveU;
        }

        public static void MoveDown()
        {
            Player.speed.Y = 1;
            Player.direction.Y = moveD;
        }

        public static void MoveUpLeft()
        {
            //up
            Player.speed.Y = 1;
            Player.direction.Y = moveU;
            //left
            Player.speed.X = 1;
            Player.direction.X = moveL;
        }

        public static void MoveUpRight()
        {
            //up
            Player.speed.Y = 1;
            Player.direction.Y = moveU;
            //right
            Player.speed.X = 1;
            Player.direction.X = moveR;
        }

        public static void MoveDownLeft()
        {
            //down
            Player.speed.Y = 1;
            Player.direction.Y = moveD;
            //left
            Player.speed.X = 1;
            Player.direction.X = moveL;
        }

        public static void MoveDownRight()
        {
            //down
            Player.speed.Y = 1;
            Player.direction.Y = moveD;
            //right
            Player.speed.X = 1;
            Player.direction.X = moveR;
        }
    }
}