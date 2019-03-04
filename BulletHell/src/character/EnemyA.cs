using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BulletHell.Graphics;

namespace BulletHell.GameEngine
{
    public class EnemyA : Enemy
    {
        public EnemyA(Canvas canvas, Texture2D texture, Vector2 startLocation) : base(canvas, texture, startLocation, null, null)
        {
            InitializeEnemy();
        }
        private void InitializeEnemy()
        {
            
            ILocationEquation down = new LinearLocationEquation(Math.PI / 2, .04F);
            ILocationEquation stayStill = StayStill.getStayStill();
            ILocationEquation right = new LinearLocationEquation(0, .04F);
            
            List<Tuple<ILocationEquation, long>> piecewiseLocationEquations = new List<Tuple<ILocationEquation, long>>();
            
            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(down, 1000 * 5));
            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(stayStill, 1000 * 7));
            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(right, 1000 * 100));
            
            PiecewiseLocationEquation locationEquation = new PiecewiseLocationEquation(piecewiseLocationEquations);
            
            this.path = new Path(locationEquation, Location, 0);
            this.gunEquipped = new BasicGun(3, new LinearLocationEquation(Math.PI / 2, 1), GraphicsLoader.getGraphicsLoader().getBulletTexture(), 1000, false);
//            this.path = new Path(right, Location, 0);
        }
    }
}