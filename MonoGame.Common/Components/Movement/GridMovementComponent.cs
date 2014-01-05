using Microsoft.Xna.Framework;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Events;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components.Movement
{
    public class GridMovementComponent : SimpleComponent
    {
        private readonly int _gridSize;

        public GridMovementComponent(int gridSize)
        {
            _gridSize = gridSize;            
        }

        public override void SetOwner(GameObject owner)
        {
            base.SetOwner(owner);
            Owner.ObjectEvent += OwnerOnObjectEvent;
        }

        private void OwnerOnObjectEvent(object sender, ObjectEventArgs objectEventArgs)
        {
            if (objectEventArgs.Action == ObjectEvent.MoveUp)
            {
                Owner.TopLeft += new Vector2(0,-_gridSize);
            }
            if (objectEventArgs.Action == ObjectEvent.MoveDown)
            {
                Owner.TopLeft += new Vector2(0, _gridSize);
            }
            if (objectEventArgs.Action == ObjectEvent.MoveLeft)
            {
                Owner.TopLeft += new Vector2(-_gridSize, 0);
            }
            if (objectEventArgs.Action == ObjectEvent.MoveRight)
            {
                Owner.TopLeft += new Vector2(_gridSize, 0);
            }
        }        
    }
}
