using System.Collections.Generic;

namespace MonoGame.Graphics.Common
{
    public class CharHelper
    {
        /// <summary>
        /// Lower case of a->z
        /// </summary>
        public static IEnumerable<char> Base26LowerChars = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

        /// <summary>
        /// Upper case of A->Z
        /// </summary>
        public static IEnumerable<char> Base26UpperChars = "abcdefghijklmnopqrstuvwxyz".ToUpper().ToCharArray();

        /// <summary>
        /// 0 -> 9
        /// </summary>
        public static IEnumerable<char> Base10Numbers = "0123456789".ToCharArray();
    }
}
