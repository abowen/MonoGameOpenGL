using System.Security.Permissions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Entities;
using MonoGame.Game.Common.Interfaces;

namespace MonoGame.Game.Common.Components
{
    public class SpriteRepeaterComponent : IMonoGameComponent
    {
        internal Texture2D Texture;
        private readonly Vector2 _relativeLocation;
        private readonly int _repeat;
        private readonly bool _isVertical;

        public int Width
        {
            get { return Texture.Width; }
        }

        public int Height
        {
            get { return Texture.Height; }
        }

        public GameObject Owner { get; set; }

        public SpriteRepeaterComponent(Texture2D texture, int repeat, bool isVertical)
        {
            Texture = texture;
            _repeat = repeat;
            _isVertical = isVertical;
            _relativeLocation = Vector2.Zero;
        }

        // TODO: Use optional parameters, introduce gap
        public SpriteRepeaterComponent(Texture2D texture, Vector2 relativeLocation, int repeat, bool isVertical)
        {
            Texture = texture;
            _relativeLocation = relativeLocation;
            _repeat = repeat;
            _isVertical = isVertical;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (var count = 0; count < _repeat; count++)
            {
                var expansion = _isVertical ? new Vector2(0, 1) : new Vector2(1, 0);
                var newLocation = Owner.TopLeft;
                newLocation += _relativeLocation;
                newLocation += count * new Vector2(Texture.Width, Texture.Height) * expansion;
                spriteBatch.Draw(Texture, newLocation, Color.White);
            }
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
