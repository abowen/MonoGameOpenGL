using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Enums;
using MonoGame.Game.Common.Infrastructure;

namespace MonoGame.Game.Common.Entities
{
    internal class TimedSprite : Sprite
    {
        private readonly double _lifeTimeMilliseconds;
        private double _elapsedTimeMilliseconds;

        public TimedSprite(Texture2D texture, Vector2 location, FaceDirection faceDirection, double lifeTimeMilliseconds, GameLayer gameLayer)
            : base(texture, location, faceDirection, gameLayer)
        {
            _lifeTimeMilliseconds = lifeTimeMilliseconds;
            Speed = 1;            
        }


        public override void Update(GameTime gameTime)
        {
            _elapsedTimeMilliseconds += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_elapsedTimeMilliseconds > _lifeTimeMilliseconds)
            {
                IsRemoved = true;
            }
            base.Update(gameTime);
        }

        protected override void CheckBounds()
        {            
        }
    }
}
