using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Graphics.Space
{
    // TODO: Could replace this with a T4 Template so as new content is added, it's easily generated
    // Convention could be A..D representats variation, while 01..04 represents animation
    public class SpaceGraphics
    {

        public static Texture2D FlameAsset { get; private set; }
        public static Texture2D MissileAsset { get; private set; }
        public static Texture2D PlayerGunLeftAsset { get; private set; }
        public static Texture2D PlayerGunRightAsset { get; private set; }
        public static Texture2D LargeBulletAsset { get; private set; }

        public static void LoadSpaceContent(ContentManager content)
        {
            content.RootDirectory = @".\Graphics";
            FlameAsset = content.Load<Texture2D>("Flame");
            PlayerGunLeftAsset = content.Load<Texture2D>("PlayerGunLeft");
            PlayerGunRightAsset = content.Load<Texture2D>("PlayerGunRight");
            MissileAsset = content.Load<Texture2D>("Missile");
            LargeBulletAsset = content.Load<Texture2D>("LargeBullet");

            // Messy way of doing things.
            OldMethodLoadSpaceContent(content);
        }

        public static Texture2D Asset { get; private set; }

        #region Old Method

        public static Dictionary<string, Texture2D[]> Assets { get; private set; }

        private static readonly string Asteroid = "Asteroid";
        private static readonly string MiniAsteroid = "MiniAsteroid";
        private static readonly string PlayerShip = "PlayerShip";
        private static readonly string EnemyShip = "EnemyShip";
        private static readonly string Bullet = "Bullet";        
        private static readonly string LargeExplosion = "LargeExplosion";
        private static readonly string MiniBullet = "MiniBullet";
        private static readonly string MiniEnemyShip = "MiniEnemyShip";
        private static readonly string Health = "Health";
        private static readonly string Planet = "Planet";
        private static readonly string Star = "Star";
        private static readonly string Boundary = "Boundary";
        private static readonly string HealthBar = "HealthBar";
        private static readonly string AmmoBar = "AmmoBar";
        private static readonly string OnePixelBar = "OnePixelBar";
        private static readonly string Fire = "Fire";
        private static readonly string BossA = "BossA";


        public static Texture2D[] AsteroidAsset { get { return Assets[Asteroid]; } }
        public static Texture2D[] MiniAsteroidAsset { get { return Assets[MiniAsteroid]; } }
        public static Texture2D[] PlayerShipAsset { get { return Assets[PlayerShip]; } }
        public static Texture2D[] EnemyShipAsset { get { return Assets[EnemyShip]; } }
        public static Texture2D[] BulletAsset { get { return Assets[Bullet]; } }        
        public static Texture2D[] LargeExpolosionAsset { get { return Assets[LargeExplosion]; } }
        public static Texture2D[] MiniBulletAsset { get { return Assets[MiniBullet]; } }
        public static Texture2D[] MiniEnemyShipAsset { get { return Assets[MiniEnemyShip]; } }
        public static Texture2D[] HealthAsset { get { return Assets[Health]; } }
        public static Texture2D[] PlanetAsset { get { return Assets[Planet]; } }
        public static Texture2D[] StarAsset { get { return Assets[Star]; } }
        public static Texture2D[] BoundaryAsset { get { return Assets[Boundary]; } }
        public static Texture2D[] HealthBarAsset { get { return Assets[HealthBar]; } }
        public static Texture2D[] AmmoBarAsset { get { return Assets[AmmoBar]; } }
        public static Texture2D[] OnePixelBarAsset { get { return Assets[OnePixelBar]; } }
        public static Texture2D[] FireAsset { get { return Assets[Fire]; } }
        public static Texture2D[] BossAAsset { get { return Assets[BossA]; } }

        private static void OldMethodLoadSpaceContent(ContentManager content)
        {            
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

            var largeExplosion = new[]
            {
                content.Load<Texture2D>("LargeExplosion01")
            };
            Assets.Add(LargeExplosion, largeExplosion);

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
            var boundary = new[]
            {
                content.Load<Texture2D>("Boundary")
            };
            Assets.Add(Boundary, boundary);

            var healthBar = new[]
            {
                content.Load<Texture2D>("HealthBar"),
                content.Load<Texture2D>("HealthBar02")
            };
            Assets.Add(HealthBar, healthBar);

            var ammoBar = new[]
            {
                content.Load<Texture2D>("AmmoBar")
            };
            Assets.Add(AmmoBar, ammoBar);

            var onePixelBar = new[]
            {
                content.Load<Texture2D>("OnePixelBar")
            };
            Assets.Add(OnePixelBar, onePixelBar);

            var fire = new[]
            {
                content.Load<Texture2D>("Fire01"),
                content.Load<Texture2D>("Fire02")
            };
            Assets.Add(Fire, fire);

            var bossA = new[]
            {
                content.Load<Texture2D>("BossA")
            };
            Assets.Add(BossA, bossA);
        }

        #endregion
    }
}
