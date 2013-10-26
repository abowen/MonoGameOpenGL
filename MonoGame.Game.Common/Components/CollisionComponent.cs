//using System;
//using MonoGame.Game.Common.Entities;
//using MonoGame.Game.Common.Events;
//using MonoGame.Game.Common.Interfaces;

//namespace MonoGame.Game.Common.Components
//{
//    public class CollisionComponent : IMonoGameComponent
//    {
//        public GameObject Owner { get; set; }

//        public CollisionComponent(GameObject owner, Action collisionAction)
//        {
//            Owner = owner;
//            Owner.ActionEvent += OwnerOnActionEvent;
//        }

//        public Action CollisionAction { get; set; }

//        private void OwnerOnActionEvent(object sender, ActionEventArgs actionEventArgs)
//        {
//            if (CollisionAction != null && actionEventArgs.Action == "Collision")
//            {
//                CollisionAction();
//            }
//        }

//        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
//        {
//            //     throw new NotImplementedException();
//        }

//        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch gameTime)
//        {
//            //     throw new NotImplementedException();
//        }
//    }
//}
