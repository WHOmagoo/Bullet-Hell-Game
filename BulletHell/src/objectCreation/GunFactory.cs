using System;
using System.Collections;
using System.Collections.Generic;
using BulletHell;
using BulletHell.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.ObjectCreation
{
    public class GunFactory 
    {
        private Hashtable gunTable;
        public GunFactory()
        {
        }

        public Gun makeGun(string type)
        {
            Texture2D t = GraphicsLoader.getGraphicsLoader().getTexture("bullet");
            BossGun g = new BossGun(1, 0, 10, 1, new SpiralLocationEquation(Math.PI / 2, 6),t, 10000, TEAM.ENEMY);
            //BossGun g = new BossGun(1, 0, 10, 1, new SinusoidalLocationEquation(10, 200, 25, .0001), t, 10000, TEAM.ENEMY);
            return g;
        }

        public Gun makeGun2(string type)
        {
            Texture2D t = GraphicsLoader.getGraphicsLoader().getTexture("bullet");

            SpiralLocationEquation s1 = new SpiralLocationEquation(Math.PI / 2, 6);
            SpiralLocationEquation s2 = new SpiralLocationEquation(Math.PI / 2, 6);
            LinearLocationEquation s3 = new LinearLocationEquation(Math.PI / 2, 1);
            //SinusoidalLocationEquation s3 = new SinusoidalLocationEquation(10, 200, 25, .0001);
            List<Tuple<ILocationEquation, long>> l2 = new List<Tuple<ILocationEquation, long>>();
            //l2.Add(<s1, 10>);
            l2.Add(new Tuple<ILocationEquation, long>(s1, 5000));
            l2.Add(new Tuple<ILocationEquation, long>(s3, 5000));
            PiecewiseLocationEquation p = new PiecewiseLocationEquation(l2);
            BossGun g = new BossGun(5, 0,10,1,p,t, 10000, TEAM.ENEMY);
            return g;

        }


    }
}