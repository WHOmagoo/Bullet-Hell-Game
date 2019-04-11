using System;
using System.Collections;
using System.Collections.Generic;
using BulletHell;
using BulletHell.GameEngine;
using Microsoft.Xna.Framework;

namespace BulletHell.ObjectCreation
{
    public class PathFactory
    {
        private Hashtable equationTable;
        public PathFactory()
        {
            loadEquationTable();
        }
        public Path makePath(Vector2 startLocation, PathData pathData)
        {
            ILocationEquation locationEquation = getEquation(pathData.equationType);
            Path p = new BasicPath(locationEquation, startLocation, pathData.angleOffset, pathData.speed);
            return p;
        }
        public Path makePath(Vector2 startLocation, List<PathData> pathData)
        {
            PiecewisePath p = new PiecewisePath(startLocation);
            foreach (var pData in pathData)
            {
                ILocationEquation equation = getEquation(pData.equationType);
                p.AddToPath(equation, pData.pathDuration, pData.angleOffset, pData.speed);
            }
            return p;
        }

        private ILocationEquation getEquation(string equation)
        {
            try
            {
                return (ILocationEquation)equationTable[equation];
            }
            catch
            {
                throw new ArgumentException("Invalid equation specified");
            }
        }
        private void loadEquationTable()
        {
            equationTable.Add("sinusoidal", new SinusoidalLocationEquation(10, 200, 25, .0001));
            equationTable.Add("zigzag", new ZigZag(Math.PI / 16, .1F, 3000, Math.PI - Math.PI / 16, .1F, 3000));
            equationTable.Add("linear", new LinearLocationEquation(0, .1f));
        }
    }
}