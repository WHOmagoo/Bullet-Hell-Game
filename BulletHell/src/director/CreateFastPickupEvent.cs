using System;
using BulletHell.gameEngine;
using BulletHell.GameEngine;
using BulletHell.graphics;
using BulletHell.Pickups;
namespace BulletHell.director
{
    public class CreateFastPickupEvent : DirectorEvent
    {
        private Canvas canvas;
        private FastPickup fastp;
        private CollisionManager collisionManager;

        public CreateFastPickupEvent(CollisionManager collisionManager, Canvas canvas, FastPickup lp)
        {
            this.canvas = canvas;
            this.fastp = lp;
            this.collisionManager = collisionManager;
        }

        public override void Execute()
        {

            canvas.AddToDrawList(fastp);
            if (!ReferenceEquals(collisionManager, null))
            {
                collisionManager.addToTeam(fastp, TEAM.ENEMY);
            }
        }
    }
}