using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Game.Common.Entities;
using MonoGame.Game.Common.Enums;
using MonoGame.Game.Common.Infrastructure;
using MonoGame.Game.Common.Interfaces;

namespace MonoGame.Game.Common.Components
{
    public class MovementComponent : IMonoGameComponent
    {
        public MovementComponent(int baseSpeed)
        {
            BaseSpeed = baseSpeed;
        }

        /// <summary>
        /// Speed of the object ignoring the location of the camera        
        /// </summary>
        public int BaseSpeed { get; protected set; }

        /// <summary>
        /// Speed of the object relative to the camera
        /// </summary>
        public int RelativeSpeed
        {
            get
            {
                var gameDepth = Owner.GameLayer.GameLayerDepth;
                if (gameDepth != GameLayerDepth.Display)
                {
                    return BaseSpeed / (int) gameDepth;
                }
                return 0;
            }
        }

        /// <summary>
        /// Movement direction of entity
        /// </summary>
        public Vector2 Direction { get; set; }

        /// <summary>
        /// Direction the Game Object is facing
        /// </summary>
        /// <remarks>
        /// It's important to keep FaceDirection separate 
        /// from Direction to allow for strafing
        /// </remarks>
        public FaceDirection FaceDirection { get; protected set; }

        public Vector2 Velocity
        {
            get
            {
                return RelativeSpeed * Direction;
            }
        }

        public GameObject Owner { get; set; }


        public void Update(GameTime gameTime)
        {
            
            Owner.Centre += Velocity;            
        }

        public void Draw(SpriteBatch gameTime)
        {
            
        }                
    }
}
