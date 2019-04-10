using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BulletHell.GameEngine
{
    public class PiecewisePath : Path
    {
        private struct Piece {
            public ILocationEquation equation;
            public long duration;
            public double angleOffset;
            public double speed;
        }
        public Vector2 InitialLocation {get;}
        private Vector2 curLocation;
        private LinkedList<Piece> pieces;
        private double _speedRatio;
        private Vector2 Offset;
        private double AngleOffset;
        private long StartTime;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="locationEquation"> The ILocationEquation to use to update the location</param>
        /// <param name="initialLocation"> The starting location of the object</param>
        /// <param name="AngleOffset"> The input angle should be in radians and will go clockwise starting in the x direction</param>
        
        public PiecewisePath(Vector2 initialLocation)
        {
            locationEquations = new LinkedList<ILocationEquation>();
            InitialLocation = initialLocation;
            curLocation = initialLocation;
            // Offset = initialLocation - locationEquation.GetLocation(0);
            StartTime = Clock.getClock().getTime();
        }
        /// <summary>
        /// Adds onto the end of the path.
        /// </summary>
        /// <param name="equation"></param> Equation that will be used during it's time period.
        /// <param name="duration"></param> How long the path should be used for.
        /// <param name="angleOffset"></param> Angle in radians.
        /// <param name="speed"></param> Speed ratio, 1 is normal speed.
        public void AddToPath(ILocationEquation equation, long duration, double angleOffset, double speed)
        {
            // locationEquations.AddLast()
        }

        public Vector2 UpdateLocation()
        {
            //FIXME:
            long curTime = (long)(Clock.getClock().getTime() * _speedRatio);
            long relativeTime = curTime - StartTime;

            LinkedListNode<Piece> curPiece = pieces.First;
            while(curPiece != null && curPiece.Value.duration < relativeTime)
            {
                relativeTime -= curPiece.Value.duration;
                curPiece = curPiece.Next;
            }
            //Reached the end of the paths. do nothing
            if(curPiece == null)
            {

            }
            else
            {

            }

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

            Vector2 newLocation = _locationEquation.GetLocation(curTime - StartTime);
            newLocation = VectorRotation.RotateVector(AngleOffset, newLocation);
            
            return newLocation + Offset;
        }

        /*
            Resets the start time to current time
         */
        public void Reset()
        {
            this.StartTime = Clock.getClock().getTime();
        }
    }
}