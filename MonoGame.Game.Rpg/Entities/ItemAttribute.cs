using MonoGame.Game.Rpg.Enums;

namespace MonoGame.Game.Rpg.Entities
{
    public class ItemAttribute
    {
        public ItemAttribute(ItemAttributeType attributeType, int value)
        {
            AttributeType = attributeType;
            Value = value;
        }

        public ItemAttributeType AttributeType { get; private set; }
        public int Value { get; private set; }
    }
}
