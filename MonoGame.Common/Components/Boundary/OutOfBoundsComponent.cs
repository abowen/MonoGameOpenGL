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
            var maximumX = (GameConstants.ScreenBoundary.Right - _rightPadding)/GameConstants.Scale;
            var maximumY = (GameConstants.ScreenBoundary.Bottom - _bottomPadding)/GameConstants.Scale;
            var minimumX = _leftPadding/GameConstants.Scale;
            var minimumY = _topPadding/GameConstants.Scale;
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
