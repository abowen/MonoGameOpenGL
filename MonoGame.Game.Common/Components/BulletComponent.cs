using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Entities;
using MonoGame.Game.Common.Enums;
using MonoGame.Game.Common.Events;
using MonoGame.Game.Common.Extensions;
using MonoGame.Game.Common.Interfaces;
using MonoGame.Graphics.Space;

namespace MonoGame.Game.Common.Components
{
    public class BulletComponent : IMonoGameComponent
    {
        private readonly Texture2D[] _texture2D;
        private readonly MovementComponent _movementComponent;


        public BulletComponent(GameObject gameObject, Texture2D[] texture, MovementComponent movementComponent)
        {
            _texture2D = texture;
            _movementComponent = movementComponent;
            Owner = gameObject;

            Owner.ObjectEvent += OwnerOnObjectEvent;
        }

        private void OwnerOnObjectEvent(object sender, ObjectEventArgs eventArgs)
        {
            if (eventArgs.Action == ObjectEvent.Fire)
            {
                Fire();
            }
        }


        public void Fire()
        {
            // Get vector from FaceDirection and use that with width & height to determine start position
            var bulletTexture = _texture2D.First();
            var bullet = new GameObject(Owner.GameLayer, new Vector2(Owner.Centre.X, Owner.TopLeft.Y));
            var bulletMovement = new MovementComponent(3, _movementComponent.FaceDirection,
                _movementComponent.FaceDirection.GetVector2());
            var bulletSprite = new SpriteComponent(bulletTexture);
            var bulletBoundary = new BoundaryComponent(bullet, SpaceGraphics.BoundaryAsset.First(), bulletTexture.Width,
                bulletTexture.Height);
            bullet.AddPhysicsComponent(bulletMovement);
            bullet.AddGraphicsComponent(bulletSprite);
            bullet.AddPhysicsComponent(bulletBoundary);

            Owner.GameLayer.GameObjects.Add(bullet);
        }

        public GameObject Owner { get; set; }

        public void Update(GameTime gameTime)
        {
            //throw new System.NotImplementedException();
        }

        public void Draw(SpriteBatch gameTime)
        {
            //throw new System.NotImplementedException();
        }
    }
}
