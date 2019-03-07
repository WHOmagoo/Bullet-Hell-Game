using System;
using BulletHell.director;

namespace BulletHell.GameEngine
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            GameDirector gD = new GameDirector();

            using (var game = new Game1(new GameDirectorLevel1Creator()))
            {
                game.Run();
            }
        }
    }
}
