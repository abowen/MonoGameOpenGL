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

namespace MonoGame.Common.Entities
{
    public class GameObject
    {
        private readonly List<IComponent> InputComponents = new List<IComponent>();
        private readonly List<IComponent> PhysicsComponents = new List<IComponent>();
        private readonly List<IComponent> GraphicsComponents = new List<IComponent>();
        public readonly GameLayer GameLayer;

        public string GameType { get; set; }

        public void AddInputComponent(IComponent component)
        {
            InputComponents.Add(component);
            component.Owner = this;
        }

        public void AddPhysicsComponent(IComponent component)
        {
            PhysicsComponents.Add(component);
            component.Owner = this;
        }

        public void AddGraphicsComponent(IComponent component)
        {
            GraphicsComponents.Add(component);
            component.Owner = this;
        }

    

        public event EventHandler<ObjectEventArgs> ObjectEvent;

        public void Event(ObjectEvent action)
        {
            if (ObjectEvent != null)
            {
                ObjectEvent(this, new ObjectEventArgs { Action = action });
            }
        }

        public GameObject(GameLayer gameLayer, Vector2 topLeftLocation)
        {
            GameLayer = gameLayer;
            TopLeft = topLeftLocation;
        }

        public void RemoveGameObject()
        {
            GameLayer.GameObjects.Remove(this);
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
                    var item = PhysicsComponents.FirstOrDefault(c => c.GetType() == typeof(BoundaryComponent)) as BoundaryComponent;
                    _boundaryComponent = item;
                }
                return _boundaryComponent;
            }
        }

        public bool HasCollision
        {
            get
            {
                return BoundaryComponent != null;
            }
        }




        public virtual void Update(GameTime gameTime)
        {
            InputComponents.ForEach(c => c.Update(gameTime));
            PhysicsComponents.ForEach(c => c.Update(gameTime));
            GraphicsComponents.ForEach(c => c.Update(gameTime));
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            GraphicsComponents.ForEach(c => c.Draw(spriteBatch));
            if (GameConstants.ShowObjectBoundary)
            {
                PhysicsComponents.ForEach(c => c.Draw(spriteBatch));
            }
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
