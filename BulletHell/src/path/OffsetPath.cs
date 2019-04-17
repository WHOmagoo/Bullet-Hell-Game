using Microsoft.Xna.Framework;

namespace BulletHell.path
{
    public class OffsetPath : Path
    {
        private Path pathToFollow;
        private Vector2 locationOffset;
        
        public OffsetPath(Path pathToFollow, Vector2 locationOffset)
        {
            this.pathToFollow = pathToFollow;
            this.locationOffset = locationOffset;
        }

        public override Vector2 UpdateLocation()
        {
            return pathToFollow.UpdateLocation() + locationOffset;
        }

        public override Path Copy()
        {
            return new OffsetPath(pathToFollow, locationOffset);
        }
    }
}