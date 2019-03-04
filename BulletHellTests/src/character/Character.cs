using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHellTests.GameEngine
{
    public class Character : GameObject
    {
        private int healthPoints;
        private bool hitBox;    // bool representing whether or not to display hitbox
        //protected Gun gunEquipped;  //need Gun class

        public Character(Canvas canvas, Texture2D texture, Vector2 startLocation, int width = 0, int height = 0) 
            : base(canvas,texture,startLocation,width,height)
        {
            hitBox = false;
            healthPoints = 1000;    // just chose a random value of 1000 for now (value may depend on which character)
        }
        
        /*public void Shoot()
        {
            //need gun class   TODO
            gun.Shoot();
        }*/

        public bool ShowHitbox
        {
            get { return hitBox; }
            set { hitBox = value; }

        }

        /*public OnHit(Bullet bullet)
        {
            //need bullet class     TODO
        }*/


    }
}