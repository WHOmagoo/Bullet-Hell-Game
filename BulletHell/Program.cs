using System;
using BulletHell.director;
using BulletHell.levels;

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
