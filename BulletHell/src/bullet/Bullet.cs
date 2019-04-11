using BulletHell.gameEngine;
using BulletHell.GameEngine;
using BulletHell.path;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.bullet
{
    public class Bullet : GameObject
    {
        int damage;
        protected internal Path pathToFollow;
        public int Damage {get => damage;}
        private TEAM team;
        public double SpeedModifier { set; get; }

        //TODO decide if we should take in ILocationEquation and make a path or accept a Path object within bullet
        public Bullet(int damage, ILocationEquation locationEquation, Texture2D texture, Vector2 startLocation, TEAM team, double speedModifier=1) : base(texture, startLocation)
        {
            this.damage = damage;
            this.pathToFollow = new Path(locationEquation, startLocation, 0);
            this.team = team;
            // BHGame.CollisionManager.addToTeam(this, TEAM.ENEMY);
            this.SpeedModifier = speedModifier;
        }
        public Bullet(int damage, Path path, Texture2D texture, TEAM team) : base(texture, path.InitialLocation)
        {
            this.damage = damage;
            this.pathToFollow = path;
            this.team = team;
        }

        public override void Update()
        {
            //TODO, modify clock to not be a singleton so that we can change the speed by modifying the time given to ILocationEquation
            Location = pathToFollow.UpdateLocation();
        }

        public override void onCollision(GameObject hitby)
        {
            if (hitby is Bullet)
                return;
            BHGame.CollisionManager.removeFromTeam(this, team);
            BHGame.Canvas.RemoveFromDrawList(this);
        }

    }
}