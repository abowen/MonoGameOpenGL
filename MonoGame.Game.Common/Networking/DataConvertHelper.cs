using System.Diagnostics.Contracts;

namespace MonoGame.Game.Common.Networking
{
    public class DataConvertHelper
    {
        public static byte ConvertBoolToByte(bool value)
        {
            return value ? (byte)255 : (byte)0;
        }

        // TODO: Complete this method
        public static byte ConvertFloatToByte(float value, float minimumRange = -1, float maximumRange = 1)
        {
            Contract.Assert(value >= minimumRange && value <= maximumRange);
            var output = (byte)((value + 1) * 255);
            return output;
        }
    }
}
