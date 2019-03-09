using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.GameEngine
{
    public class MidBoss : Enemy
    {
        public MidBoss(Texture2D texture, Vector2 startLocation) : base(texture, startLocation, null, null)
        {
            InitializeEnemy();
        }

        private void InitializeEnemy()
        {
            List<Tuple<ILocationEquation, long>> piecewiseLocationEquations2 = new List<Tuple<ILocationEquation, long>>();

            ILocationEquation downrightbullet = new LinearLocationEquation((Math.PI) / 4, .50F);
            ILocationEquation downleftbullet = new LinearLocationEquation((3 * Math.PI) / 4, .50F);
            ILocationEquation rightbullet = new LinearLocationEquation(0, .50F);
            ILocationEquation diagonalleftbullet = new LinearLocationEquation((7 * Math.PI) / 6, .50F);
            
            piecewiseLocationEquations2.Add(new Tuple<ILocationEquation, long>(downleftbullet, 1000 * 3));
            piecewiseLocationEquations2.Add(new Tuple<ILocationEquation, long>(diagonalleftbullet, 1000 * 3));
            piecewiseLocationEquations2.Add(new Tuple<ILocationEquation, long>(downleftbullet, 1000 * 3));
            piecewiseLocationEquations2.Add(new Tuple<ILocationEquation, long>(downrightbullet, 1000 * 3));
            
            //this.gunEquipped = new BasicGun(3, new LinearLocationEquation(Math.PI / 2, 1), GraphicsLoader.getGraphicsLoader().getBulletTexture(), 1000, false);
            this.gunEquipped = new BasicGun(3, new PiecewiseLocationEquation(piecewiseLocationEquations2), GraphicsLoader.getGraphicsLoader().getBulletTexture(), 1000, false);
            
            ILocationEquation down = new LinearLocationEquation(Math.PI / 2, .08F);
            ILocationEquation stayStill = StayStill.getStayStill();
            ILocationEquation right = new LinearLocationEquation(0, .08F);
            ILocationEquation left = new LinearLocationEquation(Math.PI, .08F);
            ILocationEquation up = new LinearLocationEquation((3 * Math.PI) / 2, .08F);
            ILocationEquation upright = new LinearLocationEquation((7 * Math.PI) / 4, .08F);
            ILocationEquation upleft = new LinearLocationEquation((5 * Math.PI) / 4, .08F);
            ILocationEquation downright = new LinearLocationEquation((Math.PI) / 4, .08F);
            ILocationEquation downleft = new LinearLocationEquation((3 * Math.PI) / 4, .08F);
            ILocationEquation diagonalright = new LinearLocationEquation((11*Math.PI) / 6, .08F);
            ILocationEquation diagonalleft = new LinearLocationEquation((7*Math.PI) / 6, .08F);
            
            List<Tuple<ILocationEquation, long>> piecewiseLocationEquations = new List<Tuple<ILocationEquation, long>>();

            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(downright, 1000 * 3));
            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(stayStill, 1000 * 3));
            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(diagonalleft, 1000 * 3));
            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(stayStill, 1000 * 6));
            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(right, 1000 * 6));
            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(stayStill, 1000 * 3));
            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(upleft, 1000 * 5));
            //total time on screen from 48 seconds to 115 seconds, so 27 seconds total
            
            PiecewiseLocationEquation locationEquation = new PiecewiseLocationEquation(piecewiseLocationEquations);

            this.Path = new Path(locationEquation, Location, 0);          
        }
    }
}