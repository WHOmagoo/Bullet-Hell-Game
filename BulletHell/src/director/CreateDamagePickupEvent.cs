using System;
using BulletHell.gameEngine;
using BulletHell.GameEngine;
using BulletHell.graphics;
using BulletHell.Pickups;
namespace BulletHell.director
{
    public class CreateDamagePickupEvent : DirectorEvent
    {
        private Canvas canvas;
        private DamagePickup damagep;
        private CollisionManager collisionManager;

        public CreateDamagePickupEvent(CollisionManager collisionManager, Canvas canvas, DamagePickup lp)
        {
            this.canvas = canvas;
            this.damagep = lp;
            this.collisionManager = collisionManager;
        }

        public override void Execute()
        {

            canvas.AddToDrawList(damagep);
            if (!ReferenceEquals(collisionManager, null))
            {
                collisionManager.addToTeam(damagep, TEAM.ENEMY);
            }
        }
    }
}