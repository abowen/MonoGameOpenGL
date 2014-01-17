using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace MonoGame.Sounds.Space
{
    public class SpaceSounds
    {
        // WMA IS NOT SUPPORTED BY OPEN GL
        // DO NOT USE SOUNDEFFECT FOR MUSIC
        // MAYBE PLAY MP3 IN BACKGROUND USING FOOBAR
        //public static Song Music_SickDrop;

        //public static void LoadSpaceMusic(ContentManager content)
        //{
        //    content.RootDirectory = @".\Content\Music";
        //    var sickdrop = content.Load<Song>("sickdrop");
        //    Music_SickDrop = sickdrop;
        //}

        public static SoundEffect Sound_ShortFire01;
        public static SoundEffect Sound_LongFire01;
        public static SoundEffect Sound_Explosion01;

        public static void LoadSpaceSounds(ContentManager content)
        {
            content.RootDirectory = @".\Content\Sound";
            Sound_Explosion01 = content.Load<SoundEffect>("Explosion01");
            Sound_ShortFire01 = content.Load<SoundEffect>("LongFire01");
            Sound_LongFire01 = content.Load<SoundEffect>("ShortFire01");            
        }
    }
}
