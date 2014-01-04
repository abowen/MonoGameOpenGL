using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Components;
using MonoGame.Common.Enums;
using MonoGame.Common.Events;
using MonoGame.Common.Infrastructure;
using MonoGame.Common.Interfaces;
using MonoGame.Common.Networking;

namespace MonoGame.Common.Entities
{
    public class GameObject : ISimpleDrawable, ISimpleNetworking, ISimpleUpdateable
    {
        private readonly List<SimpleComponent> _components = new List<SimpleComponent>();
        private readonly List<ISimpleUpdateable> _updateableComponents = new List<ISimpleUpdateable>();
        private readonly List<ISimpleDrawable> _drawableComponents = new List<ISimpleDrawable>();
        private readonly List<ISimpleNetworking> _networkingComponents = new List<ISimpleNetworking>();

        public GameLayer GameLayer;
        public string GameType { get; private set; }

        public void AddComponent(SimpleComponent component)
        {
            _components.Add(component);
            if (component is ISimpleUpdateable)
            {
                _updateableComponents.Add(component as ISimpleUpdateable);
            }
            if (component is ISimpleDrawable)
            {
                _drawableComponents.Add(component as ISimpleDrawable);
            }
            if (component is ISimpleNetworking)
            {
                _networkingComponents.Add(component as ISimpleNetworking);
            }
            component.SetOwner(this);
        }

        public event EventHandler<EventArgs> ObjectState;

        public void SetState(string state)
        {
            State = state;
            if (ObjectState != null)
            {
                ObjectState(this, null);
            }
        }

        public string State { get; private set; }

        public event EventHandler<ObjectEventArgs> ObjectEvent;

        public void Event(ObjectEvent action)
        {
            if (action == Enums.ObjectEvent.RemoveEntity)
            {
                GameLayer.RemoveGameObject(this);
            }
            if (action == Enums.ObjectEvent.ResetEntity)
            {
                TopLeft = _originalTopLeftLocation;
                Rotation = _originalRotation;
            }
            if (ObjectEvent != null)
            {
                ObjectEvent(this, new ObjectEventArgs { Action = action });
            }
        }

        private readonly Vector2 _originalTopLeftLocation;
        private readonly float _originalRotation;

        public Vector2 Velocity { get; set; }



        public GameObject(string typeName, Vector2 topLeftLocation, float rotation = 0)
        {
            GameType = typeName;
            TopLeft = topLeftLocation;
            _originalTopLeftLocation = topLeftLocation;
            _originalRotation = rotation;
            Rotation = rotation;
            Enable();
        }

        public void RemoveGameObject()
        {
            GameLayer.RemoveGameObject(this);
        }

        /// <summary>
        /// Top left co-ordinates for the object
        /// </summary>
        public Vector2 TopLeft { get; set; }

        /// <summary>
        /// Global Centre co-ordinates of the object
        /// </summary>
        public Vector2 Centre
        {
            get
            {
                var x = (TopLeft.X + Width / 2);
                var y = (TopLeft.Y + Height / 2);
                return new Vector2(x, y);
            }
        }

        /// <summary>
        /// Local centre co-ordinators of the object
        /// </summary>
        public Vector2 CentreLocal
        {
            get
            {
                var x = (Width / 2);
                var y = (Height / 2);
                return new Vector2(x, y);
            }
        }

        public int Width
        {
            get
            {
                // TODO: Use Sprite Component if BoundaryComponent not found
                if (BoundaryComponent != null)
                {
                    return BoundaryComponent.Width;
                }
                return 1;
            }
        }

        public int Height
        {
            get
            {
                if (BoundaryComponent != null)
                {
                    return BoundaryComponent.Height;
                }
                return 1;
            }
        }

        public void Enable()
        {
            IsEnabled = true;
        }

        public void Disable()
        {
            IsEnabled = false;
        }

        public bool IsEnabled { get; private set; }

        public Rectangle BoundingRectangle
        {
            get
            {
                return new Rectangle((int)TopLeft.X, (int)TopLeft.Y, Width, Height);
            }
        }

        private BoundaryComponent _boundaryComponent;
        BoundaryComponent BoundaryComponent
        {
            get
            {
                if (_boundaryComponent == null)
                {
                    var item = _components.FirstOrDefault(c => c.GetType() == typeof(BoundaryComponent)) as BoundaryComponent;
                    _boundaryComponent = item;
                }
                return _boundaryComponent;
            }
        }

        private StateComponent _stateComponent;
        StateComponent StateComponent
        {
            get
            {
                if (_stateComponent == null)
                {
                    var item = _components.FirstOrDefault(c => c.GetType() == typeof(StateComponent)) as StateComponent;
                    _stateComponent = item;
                }
                return _stateComponent;
            }
        }

        public IEnumerable<IMovementComponent> MovementComponents
        {
            get
            {
                return _components.OfType<IMovementComponent>();
            }
        }

        public bool HasCollision
        {
            get
            {
                return BoundaryComponent != null;
            }
        }

        public bool HasState
        {
            get
            {
                return StateComponent != null;
            }
        }


        public float Rotation { get; set; }

        public virtual void Update(GameTime gameTime)
        {
            var enumerable = _updateableComponents.GetEnumerator();
            while (IsEnabled && enumerable.MoveNext())
            {
                var component = enumerable.Current;
                if (component.IsEnabled)
                {
                    component.Update(gameTime);
                }                
            }
            TopLeft += Velocity;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            _drawableComponents.Where(c => c.IsEnabled).ToList().ForEach(c => c.Draw(spriteBatch));
        }

        public virtual void Update(NetworkMessage networkMessage)
        {
            _networkingComponents.Where(c => c.IsEnabled).ToList().ForEach(c => c.Update(networkMessage));
        }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(GameType))
            {
                return GameType;
            }
            return base.ToString();
        }
    }
}
