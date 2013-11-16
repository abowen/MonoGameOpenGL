﻿using System;
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
        private readonly List<ISimpleComponent> _components = new List<ISimpleComponent>();
        private readonly List<ISimpleUpdateable> _updateableComponents = new List<ISimpleUpdateable>();
        private readonly List<ISimpleDrawable> _drawableComponents = new List<ISimpleDrawable>();
        
        public readonly GameLayer GameLayer;
        public string GameType { get; private set; }

        public void AddComponent(ISimpleComponent component)
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

        public GameObject(string typeName, GameLayer gameLayer, Vector2 topLeftLocation)
        {
            GameType = typeName;
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
                    var item = _components.FirstOrDefault(c => c.GetType() == typeof(BoundaryComponent)) as BoundaryComponent;
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
            _updateableComponents.ForEach(c => c.Update(gameTime));
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _drawableComponents.ForEach(c => c.Draw(spriteBatch, gameTime));
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
