using System;

namespace BulletHell.GameEngine
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
            {
                Console.WriteLine("Running");
                game.Run();
            }
        }
    }
}
