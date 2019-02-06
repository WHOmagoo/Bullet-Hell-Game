using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace BulletHell.GameEngine
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        Canvas canvas;
        SpriteBatch spriteBatch;
        // Texture2D shuttle;
        Player e;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            spriteBatch = new SpriteBatch(GraphicsDevice);
            canvas = new Canvas(spriteBatch);
            FileStream fileStream = new FileStream("Content/sprites/shuttle.png", FileMode.Open);
            Texture2D texture = Texture2D.FromStream(GraphicsDevice, fileStream);
            fileStream.Dispose();
            Vector2 loc = new Vector2(20,20);
            e = new Player(canvas, texture, loc);
            // Entity e2 = new Entity(canvas, texture, new Rectangle(100,300,20,20));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // shuttle = Content.Load<Texture2D>("shuttle");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            e.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            canvas.Draw();

            base.Draw(gameTime);
        }
    }
}