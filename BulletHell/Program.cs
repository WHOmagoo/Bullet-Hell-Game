using System;
using BulletHell.controls;
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
            IGameFactory factory = new LevelCreator();
            //IGameFactory factory = new TestLevelCreeator();
            Controller controller = new Controller();
            
            
            for (int i = 0; i < args.Length; i++)
            {
                if (string.Equals(args[i], "-level", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (string.Equals(args[i+1], "test", StringComparison.OrdinalIgnoreCase))
                    {
                        factory = new TestLevelCreator();
                    } else if (string.Equals(args[i+1], "level1"))
                    {
                        factory = new LevelCreator();
                    }
                    else
                    {
                        throw new Exception("Invalid level specified");
                    }

                    //skip the next entry in args
                    i++;
                } else if (string.Equals(args[i], "-keybinding", StringComparison.CurrentCultureIgnoreCase))
                {
                    try
                    {
                        ControlLoader loader = new ControlLoader(args[i + 1]);
                        controller.OnRebind += loader.OnLoadKeys;
                        
                        loader.OnLoadKeys(controller, EventArgs.Empty);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Failed to load keys, default will be used");
                    }

                    //skip the next entry in args
                    i++;
                }
                else
                {
                    Console.WriteLine("Unknown parameter: " + args[i]);
                }  
            }

            if (args.Length > 0)
            {

                
            }
            
            using (var game = new BHGame(factory, controller))
            {
                game.Run();
            }
        }
    }
}