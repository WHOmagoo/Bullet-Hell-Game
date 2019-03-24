using System;
using BulletHell.director;
using BulletHell.levels;

namespace BulletHell.GameEngine
{
    public static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            //Default case goes here
            IGameFactory factory = new GameDirectorLevel1Creator();

            if (args.Length > 0)
            {
                if (string.Equals(args[0], "test", StringComparison.OrdinalIgnoreCase))
                {
                    factory = new TestLevelCreeator();
                } else if (string.Equals(args[0], "level1"))
                {
                    factory = new GameDirectorLevel1Creator();
                }
            }
            
            using (var game = new BHGame(factory))
            {
                game.Run();
            }
        }
    }
}
