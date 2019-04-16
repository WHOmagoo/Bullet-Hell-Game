using System;
using System.Collections.Generic;
using BulletHell.path;
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

            public Piece(ILocationEquation equation, long duration, double angleOffset, double speed)
            {
                this.equation = equation;
                this.duration = duration;
                this.angleOffset = angleOffset;
                this.speed = speed;
            }
        }
        public Vector2 InitialLocation {get;}
        private Vector2 curLocation;
        private LinkedList<Piece> pieces;
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
            // locationEquations = new LinkedList<ILocationEquation>();
            pieces = new LinkedList<Piece>();
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
            Piece p = new Piece(equation, duration, angleOffset, speed);
            pieces.AddLast(p);
        }

        public Vector2 UpdateLocation()
        {
            //FIXME: For efficiency store the last position and time and do relative calculations from last path used
            //FIXME: Get speed to work
            long curTime = (long)(Clock.getClock().getTime());
            long relativeTime = curTime - StartTime;
            Vector2 addLocation = Vector2.Zero;
            Vector2 location = InitialLocation;

            LinkedListNode<Piece> curPiece = pieces.First;
            while(curPiece != null && curPiece.Value.duration < relativeTime)
            {
                addLocation = curPiece.Value.equation.GetLocation(curPiece.Value.duration);
                addLocation = VectorRotation.RotateVector(curPiece.Value.angleOffset, addLocation);
                location += addLocation;
                relativeTime -= (long)(curPiece.Value.duration);
                curPiece = curPiece.Next;
            }
            //Haven't reached the end else nothing will happen.
            if(curPiece != null)
            {
                addLocation = curPiece.Value.equation.GetLocation((long)(relativeTime));
                addLocation = VectorRotation.RotateVector(curPiece.Value.angleOffset, addLocation);
                location += addLocation;
                // Console.WriteLine(location);
            }
            return location;
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