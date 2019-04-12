using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.Graphics;

namespace BulletHell.GameEngine
{
    public class EnemyA : Enemy
    {
        public EnemyA(Texture2D texture, Path p) : base(texture, p, null)
        {
            InitializeEnemy();
        }
        private void InitializeEnemy()
        {
            healthPoints = 5;
            
            ILocationEquation down = new LinearLocationEquation(Math.PI / 2, .04F);
            ILocationEquation stayStill = StayStill.getStayStill();
            ILocationEquation right = new LinearLocationEquation(0, .04F);
            
            List<Tuple<ILocationEquation, long>> piecewiseLocationEquations = new List<Tuple<ILocationEquation, long>>();
            
            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(down, 1000 * 5));
            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(stayStill, 1000 * 7));
            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(right, 1000 * 100));
            
            PiecewiseLocationEquation locationEquation = new PiecewiseLocationEquation(piecewiseLocationEquations);
            //FIXME: Let Director give gun?
            this.gunEquipped = new BasicGun(3, new LinearLocationEquation(Math.PI / 2, .10f), 
                GraphicsLoader.getGraphicsLoader().getTexture("bullet"), 3000, TEAM.ENEMY);

            // this.Path = new Path(locationEquation, Location, 0);
//            this.path = new Path(right, Location, 0);
        }
    }
}
