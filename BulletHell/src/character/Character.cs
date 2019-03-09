using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BulletHell.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.Graphics;

namespace BulletHell.GameEngine
{
    public class Character : GameObject, INotifyPropertyChanged
    {
        private int healthPoints;
        private bool hitBox;    // bool representing whether or not to display hitbox

        private Gun _gunEquipped;

        public event PropertyChangedEventHandler PropertyChanged;

        public Gun gunEquipped
        {
            get
            {
                return _gunEquipped;
            }

            protected set
            {
                _gunEquipped = value;
                OnWeaponChanged(nameof(gunEquipped));
            }
        }  //need Gun class

        public Character(Texture2D texture, Vector2 startLocation, int width = 0, int height = 0) 
            : base(texture,startLocation,width,height)
        {
            hitBox = false;
            healthPoints = 1000;    // just chose a random value of 1000 for now (value may depend on which character)
        }

        public void Shoot()
        {
            //need gun class   TODO
            gunEquipped.Shoot(Location);
        }

        public bool ShowHitbox
        {
            get { return hitBox; }
            set { hitBox = value; }

        }

        /*public OnHit(Bullet bullet)
        {
            //need bullet class     TODO
        }*/


        [NotifyPropertyChangedInvocator]
        protected virtual void OnWeaponChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}