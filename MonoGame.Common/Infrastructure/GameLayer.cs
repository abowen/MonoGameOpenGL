﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Infrastructure
{
    /// <summary>
    /// GameLayer represents a single environment
    /// e.g. Shop, world map, fighting arena
    /// </summary>
    public class GameLayer : ISimpleDrawable, ISimpleUpdateable
    {
        public readonly GameLayerDepth GameLayerDepth;
        public readonly List<Sprite> GameEntities = new List<Sprite>();
        public readonly List<IManager> Managers = new List<IManager>();
        public readonly List<GameObject> GameObjects = new List<GameObject>();

        public GameLayer(GameLayerDepth gameLayerDepth)
        {
            GameLayerDepth = gameLayerDepth;
        }

        public void Update(GameTime gameTime)
        {
            GameObjects.ForEach(s => s.Update(gameTime));
            GameEntities.ForEach(s => s.Update(gameTime));
            GameEntities.RemoveAll(s => s.IsRemoved);
            Managers.ForEach(s => s.Update(gameTime));

            // Collision Manager
            var sourceComponents = GameObjects.Where(co => co.HasCollision).ToList();
            var destinationComponents = sourceComponents.ToList();

            foreach (var source in sourceComponents)
            {
                destinationComponents.Remove(source);
                foreach (var destination in destinationComponents)
                {
                    if (source.BoundingRectangle.Intersects(destination.BoundingRectangle))
                    {
                        source.Event(ObjectEvent.Collision);
                        destination.Event(ObjectEvent.Collision);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gametime)
        {
            GameObjects.ForEach(s => s.Draw(spriteBatch, gametime));
            GameEntities.ForEach(s => s.Draw(spriteBatch));
        }
    }

}
