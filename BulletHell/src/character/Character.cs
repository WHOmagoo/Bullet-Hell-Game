﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BulletHell.Annotations;
using BulletHell.bullet;
using BulletHell.gameEngine;
using BulletHell.gun;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.character
{
    public abstract class Character : GameObject, INotifyPropertyChanged
    {
        protected int healthPoints;

        protected Gun _gunEquipped;

        public event PropertyChangedEventHandler PropertyChanged;

        public Gun gunEquipped
        {
            get
            {
                return _gunEquipped;
            }

            set
            {
                _gunEquipped = value;
                OnWeaponChanged(nameof(gunEquipped));
            }
        }

        public int Health { get { return healthPoints; } }

        public Character(Texture2D texture, Vector2 startLocation, int width = 0, int height = 0) 
            : base(texture,startLocation,width,height)
        {
            healthPoints = 5;
        }

        public void Shoot()
        {
            if(!ReferenceEquals(null, gunEquipped))
                gunEquipped.Shoot(Location);
        }
        protected abstract void Die();

        protected virtual void CheckHealth()
        {
            // Console.WriteLine("health: " + healthPoints);
            if (healthPoints <= 0)
                Die();
        }
        protected virtual void TakeDamage(int damage)
        {
            healthPoints -= damage;
            CheckHealth();
        }

        public override void onCollision(GameObject hitby)
        {
            if(hitby is Bullet)
            {
                Bullet b = hitby as Bullet;
                TakeDamage(b.Damage);
            }
        } 

        [NotifyPropertyChangedInvocator]
        protected virtual void OnWeaponChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}