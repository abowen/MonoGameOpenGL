﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Entities;
using MonoGame.Game.Common.Enums;
using MonoGameOpenGL.Interfaces;

namespace MonoGame.Game.Common.Infrastructure
{
    /// <summary>
    /// GameLayer represents a single environment
    /// e.g. Shop, world map, fighting arena
    /// </summary>
    public class GameLayer
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
            var collisionComponents = GameObjects.Where(co => co.HasCollision).ToArray();
            foreach (var source in collisionComponents)
            {
                foreach (var destination in collisionComponents)
                {
                    if (source != destination)
                    {
                        if (source.BoundingRectangle.Intersects(destination.BoundingRectangle))
                        {
                            source.Event(ObjectEvent.Collision);
                            destination.Event(ObjectEvent.Collision);
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            GameObjects.ForEach(s => s.Draw(spriteBatch));
            GameEntities.ForEach(s => s.Draw(spriteBatch));
        }
    }

}
