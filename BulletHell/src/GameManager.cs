using BulletHell.GameEngine;
using BulletHell.director;
using BulletHell.Graphics;
namespace BulletHell {
    public static class GameManager {
        private static BHGame game;
        public static CollisionManager collisionManager {get{return game.collisionManager;}}
        public static Canvas canvas {get{return game.canvas;}}

        // public static void MakeGame(Canvas canvas, GameDirector gameDirector, CollisionManager collisionManager)
        // {
        //     game = new BHGame(canvas, gameDirector, collisionManager);
        // }
        public static void SetGame(BHGame game)
        {
            GameManager.game = game;
        }

        public static void RunGame()
        {
            game?.Run();
        }
    }
}