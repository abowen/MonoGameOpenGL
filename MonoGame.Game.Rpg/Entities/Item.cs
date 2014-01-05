using System;
using MonoGame.Game.Rpg.Enums;

namespace MonoGame.Game.Rpg.Entities
{
    public class Item
    {
        public Guid Identifier { get; private set; }
        public ItemType ItemType { get; private set; }
        public int ItemNumber { get; private set; }
        public ItemAttribute[] Attributes;

        public Item(ItemType item, int itemNumber, params ItemAttribute[] attributes)
        {
            Identifier = Guid.NewGuid();
            ItemType = item;
            ItemNumber = itemNumber;
            Attributes = attributes;
        }
    }
}
