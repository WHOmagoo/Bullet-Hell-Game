using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace test4
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        Canvas canvas;
        SpriteBatch spriteBatch;
        // Texture2D shuttle;
        Player p;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            // p = new Player(GraphicsDevice);
            FileStream fileStream = new FileStream("Content/sprites/shuttle.png", FileMode.Open);
            Texture2D texture = Texture2D.FromStream(GraphicsDevice, fileStream);
            fileStream.Dispose();
            Vector2 loc = new Vector2(20,20);
            Entity e = new Entity(canvas, texture, loc);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            canvas = new Canvas(spriteBatch);
            // shuttle = Content.Load<Texture2D>("shuttle");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            // spriteBatch.Begin();
            // spriteBatch.Draw(p.texture, p.rect, Color.White);
            // spriteBatch.End();
            canvas.Draw();

            base.Draw(gameTime);
        }
    }
}