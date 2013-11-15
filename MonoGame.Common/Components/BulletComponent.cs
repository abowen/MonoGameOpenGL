﻿using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Events;
using MonoGame.Common.Extensions;
using MonoGame.Common.Interfaces;
using MonoGame.Graphics.Space;

namespace MonoGame.Common.Components
{
    public class BulletComponent : ISimpleComponent
    {
        private readonly Texture2D[] _texture2D;
        private readonly MovementComponent _movementComponent;
        private readonly ObjectEvent _fireEvent;
        private readonly ObjectEvent _stopEvent;
        private readonly ObjectEvent _startEvent;
        private bool _canFire = true;

        
        public BulletComponent(GameObject gameObject, Texture2D[] texture, MovementComponent movementComponent, ObjectEvent fireEvent = ObjectEvent.Fire, ObjectEvent stopEvent = ObjectEvent.Ignore, ObjectEvent startEvent = ObjectEvent.Ignore)
        {
            _texture2D = texture;
            _movementComponent = movementComponent;
            _fireEvent = fireEvent;
            _stopEvent = stopEvent;
            _startEvent = startEvent;
            Owner = gameObject;
            Owner.ObjectEvent += OwnerOnObjectEvent;
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
            var bulletTexture = _texture2D.First();
            var direction = _movementComponent.FaceDirection.GetVector2();
            direction.Normalize();
            var startLocation = Owner.Centre;
            startLocation += (direction * new Vector2(Owner.Width, Owner.Height));
            startLocation += (direction * new Vector2(bulletTexture.Width + 1, bulletTexture.Height + 1));
            var bullet = new GameObject(Owner.GameLayer, startLocation);
            bullet.GameType = "Bullet";
            var bulletMovement = new MovementComponent(3, _movementComponent.FaceDirection, direction);
            var bulletSprite = new SpriteComponent(bulletTexture);
            var bulletBoundary = new BoundaryComponent(bullet, SpaceGraphics.BoundaryAsset.First(), bulletTexture.Width,
                bulletTexture.Height);
            var instanceComponent = new InstanceComponent(bullet);
            var bulletOutOfBounds = new OutOfBoundsComponent(bullet);
            bullet.AddPhysicsComponent(bulletMovement);
            bullet.AddGraphicsComponent(bulletSprite);
            bullet.AddPhysicsComponent(bulletBoundary);
            bullet.AddPhysicsComponent(instanceComponent);
            bullet.AddPhysicsComponent(bulletOutOfBounds);

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
