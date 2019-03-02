using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BulletHell.GameEngine {

    public abstract partial class Hitbox {

        public Vector2 absLoc; //Location of the GameObject's locations
        public Vector2 relLoc; //Location relative to the GameObject
        public Hitbox(Vector2 absLoc, Vector2 relLoc) {
            this.absLoc = absLoc;
            this.relLoc = relLoc;
        }
        // public abstract bool isColliding(Hitbox h);
    }
}