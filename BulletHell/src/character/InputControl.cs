using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BulletHell.GameEngine
{
    class InputControl
    {
        const int moveL = -1;
        const int moveU = -1;
        const int moveD = 1;
        const int moveR = 1;
        public static void MoveRight()
        {
            
            Player.speed.X = 160;
            Player.direction.X = moveR;
        }

        public static void MoveLeft()
        {
            Player.speed.X = 160;
            Player.direction.X = moveL;
        }

        public static void MoveUp()
        {
            Player.speed.Y = 160;
            Player.direction.Y = moveU;
        }

        public static void MoveDown()
        {
            Player.speed.Y = 160;
            Player.direction.Y = moveD;
        }



    }
}