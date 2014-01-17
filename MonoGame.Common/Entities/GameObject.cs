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
        private readonly List<ISimpleDrawable> _drawableComponents = new List<ISimpleDrawable>();
        private readonly List<ISimpleNetworking> _networkingComponents = new List<ISimpleNetworking>();
        private readonly float _originalRotation;
        private readonly Vector2 _originalTopLeftLocation;
        private readonly List<ISimpleUpdateable> _updateableComponents = new List<ISimpleUpdateable>();

        public GameLayer GameLayer;
        private BoundaryComponent _boundaryComponent;
        private bool _hadCollision;
        private bool _preHadCollision;
        private StateComponent _stateComponent;

        public GameObject(string typeName, Vector2 topLeftLocation, float rotation = 0)
        {
            GameType = typeName;
            TopLeft = topLeftLocation;
            _originalTopLeftLocation = topLeftLocation;
            _originalRotation = rotation;
            Rotation = rotation;
            Enable();
        }

        public string GameType { get; private set; }

        public string State { get; private set; }

        public Vector2 Velocity { get; set; }

        public Vector2 TopLeft { get; set; }

        public Vector2 TopLeftScaled
        {
            get { return TopLeft * GameConstants.Scale; }
        }

        /// <summary>
        ///     Global Centre co-ordinates of the object
        /// </summary>
        public Vector2 Centre
        {
            get
            {
                float x = (TopLeft.X + Width / 2);
                float y = (TopLeft.Y + Height / 2);
                return new Vector2(x, y);
            }
        }

        /// <summary>
        ///     Local centre co-ordinators of the object
        /// </summary>
        public Vector2 CentreLocal
        {
            get
            {
                int x = (Width / 2);
                int y = (Height / 2);
                return new Vector2(x, y);
            }
        }

        public float WidthScaled
        {
            get { return Width * GameConstants.Scale; }
        }

        public float HeighScaled
        {
            get { return Height * GameConstants.Scale; }
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

        public Rectangle BoundingRectangle
        {
            get { return new Rectangle((int)TopLeft.X, (int)TopLeft.Y, Width, Height); }
        }

        private BoundaryComponent BoundaryComponent
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

        private StateComponent StateComponent
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
            get { return _components.OfType<IMovementComponent>(); }
        }

        public bool HasCollisionComponent
        {
            get { return BoundaryComponent != null; }
        }

        public string[] IgnoreCollisionTypes
        {
            get
            {
                if (BoundaryComponent != null)
                {
                    return BoundaryComponent.IgnoreTypes;
                }
                return new string[0];
            }
        }

        public bool HasState
        {
            get { return StateComponent != null; }
        }


        public float Rotation { get; set; }
        public bool IsEnabled { get; private set; }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            _drawableComponents.Where(c => c.IsEnabled).ToList().ForEach(c => c.Draw(spriteBatch));
        }

        public virtual void Update(NetworkMessage networkMessage)
        {
            _networkingComponents.Where(c => c.IsEnabled).ToList().ForEach(c => c.Update(networkMessage));
        }

        public virtual void Update(GameTime gameTime)
        {
            if (!_preHadCollision && _hadCollision)
            {
                Event(Enums.ObjectEvent.CollisionEnter);
                _preHadCollision = true;
            }
            else if (_preHadCollision && !_hadCollision)
            {
                Event(Enums.ObjectEvent.CollisionExit);
                _preHadCollision = false;
            }
            _hadCollision = false;

            List<ISimpleUpdateable>.Enumerator enumerable = _updateableComponents.GetEnumerator();
            while (IsEnabled && enumerable.MoveNext())
            {
                ISimpleUpdateable component = enumerable.Current;
                if (component.IsEnabled)
                {
                    component.Update(gameTime);
                }
            }
            TopLeft += Velocity;
        }

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

        public event EventHandler<ObjectEventArgs> ObjectEvent;

        public void Event(ObjectEvent action)
        {
            if (action == Enums.ObjectEvent.RemoveEntity)
            {
                RemoveGameObject();
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

        public void RemoveGameObject()
        {
            GameLayer.RemoveGameObject(this);
        }

        public void Enable()
        {
            IsEnabled = true;
        }

        public void Disable()
        {
            IsEnabled = false;
        }

        public void RaiseCollisionEvent(GameObject otherGameObject)
        {
            if (IgnoreCollisionTypes.Contains(otherGameObject.GameType))
            {
                return;
            }
            _hadCollision = true;
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