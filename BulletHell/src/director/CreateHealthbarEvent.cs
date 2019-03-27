using System;
using BulletHell.GameEngine;
using BulletHell.Graphics;

namespace BulletHell.director
{
    public class CreateHealthbarEvent : DirectorEvent
    {
        private Canvas canvas;
        private HealthBar healthbar;
        private LifeBar lifebar;

        public CreateHealthbarEvent(Canvas cnv,HealthBar hb,LifeBar lb)
        {
            this.canvas = cnv;
            this.healthbar = hb;
            this.lifebar = lb;
        }

        public override void Execute()
        {
            healthbar.ResetPath();
            lifebar.ResetPath();
            canvas.AddToDrawList(healthbar);
            canvas.AddToDrawList(lifebar);
        }
    }
}