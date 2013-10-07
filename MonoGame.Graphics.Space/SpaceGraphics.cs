using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Graphics.Space
{
    // TODO: Could replace this with a T4 Template so as new content is added, it's easily generated
    // Convention could be A..D representats variation, while 01..04 represents animation
    public class SpaceGraphics
    {
        public static Dictionary<string, Texture2D[]> Assets { get; private set; }

        private static readonly string Asteroid = "Asteroid";
        private static readonly string MiniAsteroid = "MiniAsteroid";
        private static readonly string PlayerShip = "PlayerShip";
        private static readonly string EnemyShip = "EnemyShip";
        private static readonly string Bullet = "Bullet";
        private static readonly string MiniBullet = "MiniBullet";
        private static readonly string MiniEnemyShip = "MiniEnemyShip";
        private static readonly string Health = "Health";
        private static readonly string Planet = "Planet";
        private static readonly string Star = "Star";

        public static Texture2D[] AsteroidAsset { get { return Assets[Asteroid]; } }
        public static Texture2D[] MiniAsteroidAsset { get { return Assets[MiniAsteroid]; } }
        public static Texture2D[] PlayerShipAsset { get { return Assets[PlayerShip]; } }
        public static Texture2D[] EnemyShipAsset { get { return Assets[EnemyShip]; } }
        public static Texture2D[] BulletAsset { get { return Assets[Bullet]; } }
        public static Texture2D[] MiniBulletAsset { get { return Assets[MiniBullet]; } }
        public static Texture2D[] MiniEnemyShipAsset { get { return Assets[MiniEnemyShip]; } }
        public static Texture2D[] HealthAsset { get { return Assets[Health]; } }
        public static Texture2D[] PlanetAsset { get { return Assets[Planet]; } }
        public static Texture2D[] StarAsset { get { return Assets[Star]; } }

        public static void LoadSpaceContent(ContentManager content)
        {
            content.RootDirectory = "";
            Assets = new Dictionary<string, Texture2D[]>();

            var planets = new[]
            {
                content.Load<Texture2D>("Planet01"),
                content.Load<Texture2D>("Planet02"),
                content.Load<Texture2D>("Planet03"),                
                content.Load<Texture2D>("Planet04")                
            };            
            Assets.Add(Planet, planets);

            var asteroids = new[]
            {
                content.Load<Texture2D>("Asteroid01"),
                content.Load<Texture2D>("Asteroid02"),
                content.Load<Texture2D>("Asteroid03"),
                content.Load<Texture2D>("Asteroid04"),
            };
            Assets.Add(Asteroid, asteroids);

            var miniAsteroids = new[]
            {
                content.Load<Texture2D>("MiniAsteroid01"),
                content.Load<Texture2D>("MiniAsteroid02"),
                content.Load<Texture2D>("MiniAsteroid03"),
                content.Load<Texture2D>("MiniAsteroid04"),
            };
            Assets.Add(MiniAsteroid, miniAsteroids);

            var stars = new[]
            {
                content.Load<Texture2D>("Star01"),
                content.Load<Texture2D>("Star02")                
            };
            Assets.Add(Star, stars);

            var playerShip = new[]
            {
                content.Load<Texture2D>("PlayerShip")
            };
            Assets.Add(PlayerShip, playerShip);

            var enemyShip = new[]
            {
                content.Load<Texture2D>("EnemyShip")
            };
            Assets.Add(EnemyShip, enemyShip);

            var bullet = new[]
            {
                content.Load<Texture2D>("Bullet")
            };
            Assets.Add(Bullet, bullet);

            var miniBullet = new[]
            {
                content.Load<Texture2D>("MiniBullet")
            };
            Assets.Add(MiniBullet, miniBullet);

            var miniEnemyShip = new[]
            {
                content.Load<Texture2D>("MiniEnemyShip")
            };
            Assets.Add(MiniEnemyShip, miniEnemyShip);

            var health = new[]
            {
                content.Load<Texture2D>("Health")
            };
            Assets.Add(Health, health);
        }
    }
}
