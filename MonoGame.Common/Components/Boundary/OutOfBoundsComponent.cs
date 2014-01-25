using Microsoft.Xna.Framework;
using MonoGame.Common.Enums;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components.Boundary
{
    public class OutOfBoundsComponent : SimpleComponent, ISimpleUpdateable
    {
        private readonly ObjectEvent _publishEvent;
        private readonly int _leftPadding;
        private readonly int _topPadding;
        private readonly int _rightPadding;
        private readonly int _bottomPadding;        

        /// <summary>
        /// Raises an event if goes outside the game (scaled) boundaries
        /// </summary>
        public OutOfBoundsComponent(ObjectEvent publishEvent, int leftPadding = 0, int topPadding = 0, int rightPadding = 0, int bottomPadding = 0)
        {
            _publishEvent = publishEvent;
            _leftPadding = leftPadding;
            _topPadding = topPadding;
            _rightPadding = rightPadding;
            _bottomPadding = bottomPadding;         
        }

        public void Update(GameTime gameTime)
        {
            var maximumX = (GameHelper.GetRelativeScaleX(1f) - _rightPadding);
            var maximumY = (GameHelper.GetRelativeScaleY(1f) - _bottomPadding);
            var minimumX = _leftPadding;
            var minimumY = _topPadding;

            if (Owner.Centre.X > maximumX ||
                Owner.Centre.X < minimumX ||
                Owner.Centre.Y < minimumY ||
                Owner.Centre.Y > maximumY)
            {
                Owner.Event(_publishEvent);
            }
        }
    }
}
