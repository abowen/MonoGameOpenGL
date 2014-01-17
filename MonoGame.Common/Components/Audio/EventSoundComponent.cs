using Microsoft.Xna.Framework.Audio;
using MonoGame.Common.Entities;
using MonoGame.Common.Enums;
using MonoGame.Common.Events;
using MonoGame.Common.Interfaces;

namespace MonoGame.Common.Components.Audio
{
    public class EventSoundComponent : SimpleComponent
    {
        private readonly SoundEffect _soundEffect;
        private readonly ObjectEvent _subscribeEvent;

        public EventSoundComponent(SoundEffect soundEffect, ObjectEvent subscribeEvent)
        {
            _soundEffect = soundEffect;
            _subscribeEvent = subscribeEvent;
        }

        public override void SetOwner(GameObject owner)
        {
            base.SetOwner(owner);
            Owner.ObjectEvent += OwnerOnObjectEvent;
        }

        private void OwnerOnObjectEvent(object sender, ObjectEventArgs objectEventArgs)
        {
            if (objectEventArgs.Action == _subscribeEvent)
            {
                _soundEffect.Play();
            }
        }
    }
}
