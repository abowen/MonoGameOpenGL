﻿using Microsoft.Xna.Framework;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components
{
    public class MovementComponent : ISimpleComponent, ISimpleUpdateable, IMovementComponent
    {
        public MovementComponent(float baseSpeed, FaceDirection startFaceDirection, Vector2 movementInputDirection)
        {
            BaseSpeed = baseSpeed;
            FaceDirection = startFaceDirection;
            InputDirection = movementInputDirection;
        }

        /// <summary>
        /// Speed of the object ignoring the location of the camera        
        /// </summary>
        public float BaseSpeed { get; protected set; }

        /// <summary>
        /// Speed of the object relative to the camera
        /// </summary>
        public float RelativeSpeed
        {
            get
            {
                var gameDepth = Owner.GameLayer.GameLayerDepth;
                if (gameDepth != GameLayerDepth.Display)
                {
                    var result =  BaseSpeed / (int) gameDepth;
                    if (result == 0)
                    {
                        // DEBUG
                    }
                    return result;
                }
                return 0;
            }
        }

        /// <summary>
        /// Movement direction of entity
        /// </summary>
        public Vector2 InputDirection { get; set; }

        /// <summary>
        /// InputDirection the Game Object is facing
        /// </summary>
        /// <remarks>
        /// It's important to keep FaceDirection separate 
        /// from InputDirection to allow for strafing
        /// </remarks>
        public FaceDirection FaceDirection { get; protected set; }

        public Vector2 Velocity
        {
            get
            {
                return RelativeSpeed * InputDirection;
            }
        }

        public GameObject Owner { get; private set; }

        public void SetOwner(GameObject owner)
        {
            Owner = owner;
        }


        public void Update(GameTime gameTime)
        {
            Owner.TopLeft += Velocity;            
        }           
    }
}
