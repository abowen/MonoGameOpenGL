using Microsoft.Xna.Framework;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
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
            if (Owner.Centre.X > (GameConstants.ScreenBoundary.Right - _rightPadding) ||
                Owner.Centre.X < (0 + _leftPadding) ||
                Owner.Centre.Y < (0 + _topPadding) ||
                Owner.Centre.Y > (GameConstants.ScreenBoundary.Bottom - _bottomPadding))
            {
                Owner.Event(_publishEvent);
            }
        }
    }
}
