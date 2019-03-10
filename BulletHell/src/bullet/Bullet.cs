using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BulletHell;

namespace BulletHell.GameEngine
{
    public class Bullet : GameObject
    {
        int damage;
        Path pathToFollow;
        public int Damage {get => damage;}
        private TEAM team;

        //TODO decide if we should take in ILocationEquation and make a path or accept a Path object within bullet
        public Bullet(int damage, ILocationEquation locationEquation, Texture2D texture, Vector2 startLocation, TEAM team) : base(texture, startLocation)
        {
            this.damage = damage;
            this.pathToFollow = new Path(locationEquation, startLocation, 0);
            this.team = team;
            // BHGame.CollisionManager.addToTeam(this, TEAM.ENEMY);
        }

        public override void Update()
        {
            Location = pathToFollow.UpdateLocation();
        }

        public override void onCollision(GameObject hitby)
        {
            BHGame.CollisionManager.removeFromTeam(this, team);
            BHGame.Canvas.RemoveFromDrawList(this);
        }

    }
}