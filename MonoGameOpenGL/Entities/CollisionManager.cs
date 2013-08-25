using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace MonoGameOpenGL.Entities
{
    public class CollisionManager
    {
        public CollisionManager(GameState gameState)
        {
            _gameState = gameState;
        }

        private readonly GameState _gameState;
        public readonly List<CollisionType> CollisionTypes = new List<CollisionType>();

        public void Update(GameTime gameTime)
        {
            foreach(var collisionType in CollisionTypes)
            {
                var thisCollisionType = collisionType;
                var sourceCollisions = _gameState.GameEntities.Where(s => s.GetType() == thisCollisionType.TypeA).ToList();
                var destinationCollision = _gameState.GameEntities.Where(s => s.GetType() == thisCollisionType.TypeB).ToList();
                foreach (var source in sourceCollisions)
                {
                    foreach (var destination in destinationCollision)
                    {
                        if (source.BoundingBox.Intersects(destination.BoundingBox))
                        {
                            collisionType.Action(source, destination);
                        }
                    }
                }
            }
        }
    }
}
