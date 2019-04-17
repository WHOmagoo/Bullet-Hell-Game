using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BulletHell.path
{
    public class PiecewiseLocationEquation : ILocationEquation
    {
        private LinkedList<Tuple<ILocationEquation, long>> equationAndTimePairs;
        
        
        /// <summary>
        ///     Creates a new PiecewiseLocationEquation
        /// </summary>
        /// <param name="equationAndTimePairs">Should be a list that contains tuples, each tuple dictates the
        ///     ILocationEquation to use, and a long representing the amount of time to use each equation for.
        ///     The order of the list dictates the order in which the equations will be used.
        /// </param>
        public PiecewiseLocationEquation(List<Tuple<ILocationEquation, long>> equationAndTimePairs)
        {
            this.equationAndTimePairs = new LinkedList<Tuple<ILocationEquation, long>>(equationAndTimePairs);
        }
        
        public Vector2 GetLocation(long ticksElapsed)
        {
            long ticksCheckedInList = 0;


            Vector2 result = Vector2.Zero;

            int loop = 0;
            foreach (var equationAndTimePair in equationAndTimePairs)
            {
                if (ticksCheckedInList + equationAndTimePair.Item2 > ticksElapsed)
                {
                    result += equationAndTimePair.Item1.GetLocation(ticksElapsed - ticksCheckedInList);
                    break;
                }

                ticksCheckedInList += equationAndTimePair.Item2;
                result += equationAndTimePair.Item1.GetLocation(equationAndTimePair.Item2);
            }

            return result;
        }
    }
}