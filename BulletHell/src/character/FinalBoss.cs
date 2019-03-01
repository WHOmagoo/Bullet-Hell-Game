using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.GameEngine
{
    public class FinalBoss : Enemy
    {
        public FinalBoss(Texture2D texture, Vector2 startLocation) : base(texture, startLocation, null, null)
        {
            InitializeEnemy();
        }
        private void InitializeEnemy()
        {

        }

        public void movePattern()
        {
            ILocationEquation down = new LinearLocationEquation(Math.PI / 2, .08F);
            ILocationEquation stayStill = StayStill.getStayStill();
            ILocationEquation right = new LinearLocationEquation(0, .08F);
            ILocationEquation left = new LinearLocationEquation(Math.PI, .08F);
            ILocationEquation up = new LinearLocationEquation((3 * Math.PI) / 2, .08F);
            ILocationEquation upright = new LinearLocationEquation((10 * Math.PI) / 6, .08F);
            ILocationEquation upleft = new LinearLocationEquation((8 * Math.PI) / 6, .08F);
            ILocationEquation downright = new LinearLocationEquation((2*Math.PI) / 6, .08F);
            ILocationEquation downleft = new LinearLocationEquation((4 * Math.PI) / 6, .08F);
            ILocationEquation diagonalright = new LinearLocationEquation((11 * Math.PI) / 6, .08F);
            ILocationEquation diagonalleft = new LinearLocationEquation((7 * Math.PI) / 6, .08F);


            List<Tuple<ILocationEquation, long>> piecewiseLocationEquations = new List<Tuple<ILocationEquation, long>>();

            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(downright, 1000 * 2));
            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(stayStill, 1000 * 6));
            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(downleft, 1000 * 2));
            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(stayStill, 1000 * 6));
            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(upleft, 1000 * 2));
            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(stayStill, 1000 * 6));
            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(upright, 1000 * 2));

            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(stayStill, 1000 * 6));

            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(downright, 1000 * 2));
            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(stayStill, 1000 * 6));
            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(downleft, 1000 * 2));
            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(stayStill, 1000 * 6));
            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(upleft, 1000 * 2));
            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(stayStill, 1000 * 6));
            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(upright, 1000 * 2));

            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(stayStill, 1000 * 6));

            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(downright, 1000 * 2));
            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(stayStill, 1000 * 6));
            piecewiseLocationEquations.Add(new Tuple<ILocationEquation, long>(upright, 1000 * 10));
            //total time from 90 seconds to 162 seconds, so 72 seconds on screen total
            
            PiecewiseLocationEquation locationEquation = new PiecewiseLocationEquation(piecewiseLocationEquations);

            this.path = new Path(locationEquation, Location, 0);

            this.gunEquipped = new BasicGun(3, new LinearLocationEquation(Math.PI / 2, 1), GraphicsLoader.getGraphicsLoader().getBulletTexture(), 1000, false);
        }

        //shooting methods: (will cause bullets to shoot in different directions)
        public void shootMethod1()
        {
            List<Tuple<ILocationEquation, long>> piecewiseLocationEquations2 = new List<Tuple<ILocationEquation, long>>();
            ILocationEquation downleftbullet = new LinearLocationEquation((3 * Math.PI) / 4, .50F);
            piecewiseLocationEquations2.Add(new Tuple<ILocationEquation, long>(downleftbullet, 1000 * 3));            
            this.gunEquipped = new BasicGun(3, new PiecewiseLocationEquation(piecewiseLocationEquations2), GraphicsLoader.getGraphicsLoader().getBulletTexture(), 1000, false);
        }
        public void shootMethod2()
        {
            List<Tuple<ILocationEquation, long>> piecewiseLocationEquations2 = new List<Tuple<ILocationEquation, long>>();
            ILocationEquation downrightbullet = new LinearLocationEquation((Math.PI) / 4, .50F);
            piecewiseLocationEquations2.Add(new Tuple<ILocationEquation, long>(downrightbullet, 1000 * 3));            
            this.gunEquipped = new BasicGun(3, new PiecewiseLocationEquation(piecewiseLocationEquations2), GraphicsLoader.getGraphicsLoader().getBulletTexture(), 1000, false);
        }
        public void shootMethod3()
        {
            List<Tuple<ILocationEquation, long>> piecewiseLocationEquations2 = new List<Tuple<ILocationEquation, long>>();
            ILocationEquation downbullet = new LinearLocationEquation(Math.PI / 2, .50F);
            piecewiseLocationEquations2.Add(new Tuple<ILocationEquation, long>(downbullet, 1000 * 3));
            this.gunEquipped = new BasicGun(3, new PiecewiseLocationEquation(piecewiseLocationEquations2), GraphicsLoader.getGraphicsLoader().getBulletTexture(), 1000, false);
        }
        public void shootMethod4()
        {
            List<Tuple<ILocationEquation, long>> piecewiseLocationEquations2 = new List<Tuple<ILocationEquation, long>>();
            ILocationEquation diagonalleftbullet = new LinearLocationEquation((7 * Math.PI) / 6, .50F);
            piecewiseLocationEquations2.Add(new Tuple<ILocationEquation, long>(diagonalleftbullet, 1000 * 3));
            this.gunEquipped = new BasicGun(3, new PiecewiseLocationEquation(piecewiseLocationEquations2), GraphicsLoader.getGraphicsLoader().getBulletTexture(), 1000, false);
        }
    }
}