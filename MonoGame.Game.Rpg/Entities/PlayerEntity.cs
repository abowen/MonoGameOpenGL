using System.Collections.Generic;

namespace MonoGame.Game.Rpg.Entities
{
    /// <summary>
    /// Used for save games
    /// </summary>
    public class PlayerEntity
    {
        public PlayerEntity(string name, int startHealth = 5, int startMoves = 3)
        {
            Name = name;
            TotalHealth = startHealth;
            Health = startHealth;
            TotalMoves = startMoves;
            Moves = startMoves;
        }

        public readonly string Name;
        public int TotalHealth { get; set; }
        public int Health { get; set; }
        public int TotalMoves { get; set; }
        public int Moves { get; set; }
        public readonly List<Item> Inventory = new List<Item>();
    }
}
