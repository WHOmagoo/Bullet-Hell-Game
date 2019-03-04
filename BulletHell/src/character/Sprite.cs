using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;



namespace BulletHell.GameEngine
{
    class Sprite
    {
        public Vector2 Position = new Vector2(0, 0);

        public Rectangle Size;

        public string name;

        private float mScale = 1.0f;
        private Texture2D spriteTexture;

        public float Scale
        {
            get { return mScale; }
            set
            {
                mScale = value;
                Size = new Rectangle(0, 0, (int)(spriteTexture.Width * Scale), (int)(spriteTexture.Height * Scale));
            }
        }

        public void LoadContent(ContentManager theContentManager, string theName)
        {
            spriteTexture = theContentManager.Load<Texture2D>(theName);
            name = theName;
            Size = new Rectangle(0, 0,(int)(spriteTexture.Width * Scale), (int)(spriteTexture.Height * Scale));
            
        }

        public void Update(GameTime theGameTime, Vector2 speed, Vector2 direction)
        {
            Position += direction * speed * (float)theGameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch theSprite)
        {
            theSprite.Draw(spriteTexture, Position,
                new Rectangle(0, 0, spriteTexture.Width, spriteTexture.Height),
                Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }

    }
}
