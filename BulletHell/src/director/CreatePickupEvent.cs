using System;
using BulletHell.GameEngine;
using BulletHell.Graphics;
using BulletHell.Pickups;
namespace BulletHell.director
{
    public class CreateLifePickupEvent : DirectorEvent
    {
        private Canvas canvas;
        private LifePickup lifep;
        private CollisionManager collisionManager;

        public CreateLifePickupEvent(CollisionManager collisionManager, Canvas canvas, LifePickup lp)
        {
            this.canvas = canvas;
            this.lifep = lp;
            this.collisionManager = collisionManager;
        }

        public override void Execute()
        {
            
            canvas.AddToDrawList(lifep);
            if (!ReferenceEquals(collisionManager, null))
            {
                collisionManager.addToTeam(lifep, TEAM.ENEMY);
            }
        }
    }
}