using System;

namespace BulletHell.director
{
    public class GameWinEvent : DirectorEvent
    {
        public override void Execute()
        {
            BHGame.OnWinCondition();
            //TODO modify this to display an on screen prompt instead of console output
            Console.WriteLine("Congratulations, you win!");
        }
    }
}