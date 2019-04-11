using System;
using System.Collections.Generic;
using BulletHell.gameEngine;
using BulletHell.graphics;
using BulletHell.gun;
using BulletHell.path;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.character
{
    public class MidBoss : Enemy
    {
        private Gun[] guns;
        private int curGunIndex;
        public MidBoss(Texture2D texture, Vector2 startLocation) : base(texture, startLocation, null, null)
        {
            setGuns();
            InitializeEnemy();
        }

        private void setGuns()
        {
            guns = new Gun[2];
            // guns[0] = new BasicGun(1, new SinusoidalLocationEquation(90, 110, 200), 
            //     GraphicsLoader.getGraphicsLoader().getBulletTexture(), 2000, TEAM.ENEMY, Math.PI/2);
            guns[0] = new SurroundShotGun(16, (float) Math.PI / 2, (float) (Math.PI / 9), 1,
                   new  SinusoidalLocationEquation(90, 110, 200) , GraphicsLoader.getGraphicsLoader().getBulletTexture(), 3500, TEAM.ENEMY);
            guns[1] = new BasicShotgun((float) Math.PI / 2, (float) (Math.PI / 9), 1,
                //    new  ZigZag(Math.PI / 4, .1F, 200, Math.PI - Math.PI / 4, .1F, 200), 
                    new LinearLocationEquation(Math.PI/2, .2),
                   GraphicsLoader.getGraphicsLoader().getBulletTexture(), 3500, TEAM.ENEMY);
            curGunIndex = 0;
            this.gunEquipped = guns[curGunIndex];
        }

        // private void swapWeapons()
        // {
        //     Console.WriteLine("swap weapons");
        //     curGunIndex++;
        //     curGunIndex = (curGunIndex >= guns.Length ? 0 : curGunIndex);
        //     this.gunEquipped = guns[curGunIndex];
        // }

        //FIXME: For some reason doesn't shoot after weapon change
        // public override void Update()
        // {
        //     base.Update();
        //     if (healthPoints < 20)
        //     {
        //         this.gunEquipped = guns[0];
        //         Console.WriteLine("changed to 0");
        //     }
        // }

        private void InitializeEnemy()
        {
            healthPoints = 30;
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