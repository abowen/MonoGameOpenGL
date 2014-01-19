using System.Linq;
using System.Linq.Expressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Components.Boundary;
using MonoGame.Common.Components.Graphics;
using MonoGame.Common.Components.Movement;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Events;
using MonoGame.Common.Extensions;
using MonoGame.Common.Interfaces;
using MonoGame.Graphics.Space;

namespace MonoGame.Common.Components
{
    public class BulletComponent : SimpleComponent
    {
        private readonly string _bulletGameType;
        private readonly Texture2D _texture2D;
        private readonly MovementComponent _movementComponent;
        private readonly ObjectEvent _fireEvent;
        private readonly ObjectEvent _stopEvent;
        private readonly ObjectEvent _startEvent;
        private readonly int _bulletSpeed;
        private readonly float _bulletAcceleration;
        private readonly Vector2 _offset;
        private readonly Color _color;
        private readonly string[] _ignoreCollisionTypes;
        private bool _canFire = true;



        public BulletComponent(string bulletGameType, Texture2D texture, MovementComponent movementComponent, ObjectEvent fireEvent = ObjectEvent.Fire, ObjectEvent stopEvent = ObjectEvent.Ignore, ObjectEvent startEvent = ObjectEvent.Ignore,
            int bulletSpeed = 3, Color? color = null, float bulletAcceleration = 0, Vector2? offset = null, params string[] ignoreCollisionTypes)
        {
            _bulletGameType = bulletGameType;
            _texture2D = texture;
            _movementComponent = movementComponent;
            _fireEvent = fireEvent;
            _stopEvent = stopEvent;
            _startEvent = startEvent;
            _bulletSpeed = bulletSpeed;
            _bulletAcceleration = bulletAcceleration;
            _offset = offset ?? Vector2.Zero;
            _color = color ?? Color.White;
            _ignoreCollisionTypes = ignoreCollisionTypes;
        }

        private void OwnerOnObjectEvent(object sender, ObjectEventArgs eventArgs)
        {
            if (eventArgs.Action == _fireEvent && _canFire)
            {
                Fire();
            }
            else if (eventArgs.Action == _stopEvent)
            {
                _canFire = false;
            }
            else if (eventArgs.Action == _startEvent)
            {
                _canFire = true;
            }
        }

        public void Fire()
        {
            var bulletTexture = _texture2D;
            var direction = _movementComponent.FaceDirection.GetVector2();
            direction.Normalize();

            var startLocation = Owner.Centre;
            startLocation += (direction * new Vector2(Owner.Width, Owner.Height));
            startLocation += (direction * new Vector2(bulletTexture.Width + 1, bulletTexture.Height + 1));
            startLocation += _offset;

            var bullet = new GameObject(_bulletGameType, startLocation);

            if (_bulletSpeed > 0)
            {
                var movement = new MovementComponent(_bulletSpeed, _movementComponent.FaceDirection, direction);
                bullet.AddComponent(movement);
            }
            if (_bulletAcceleration > 0)
            {
                var acceleration = new AccelerateMovementComponent(new Vector2(0, -_bulletAcceleration));
                bullet.AddComponent(acceleration);
            }

            var bulletSprite = new SpriteComponent(bulletTexture, color: _color);
            var bulletBoundary = new BoundaryComponent(SpaceGraphics.BoundaryAsset.First(), bulletTexture.Width, bulletTexture.Height, true, _ignoreCollisionTypes);
            var instanceComponent = new InstanceComponent();
            var bulletOutOfBounds = new OutOfBoundsComponent(ObjectEvent.RemoveEntity);
            
            bullet.AddComponent(bulletSprite);
            bullet.AddComponent(bulletBoundary);
            bullet.AddComponent(instanceComponent);
            bullet.AddComponent(bulletOutOfBounds);

            Owner.GameLayer.AddGameObject(bullet);
        }

        public override void SetOwner(GameObject owner)
        {
            Owner = owner;
            Owner.ObjectEvent += OwnerOnObjectEvent;
        }
    }
}
