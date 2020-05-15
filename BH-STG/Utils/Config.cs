namespace BH_STG.Utils
{
    public class Config
    {

        public enum EnemyType
        {
            EnemyType0 = 0,
            EnemyType1 = 1,
            MidBoss = 2,
            FinalBoss = 3
        }
        public enum BulletStyle
        {
            Bullet0 = 0,
            Bullet1 = 1,
            Bullet2 = 2,
            Bullet3 = 3
        }

        public enum TrackStyle
        {
            NormalTrack = 0,
            NormalWithBias = 1,
            CircleTrack = 2,
            TailTrack = 3,
            NormalStayTrack = 4,
            RightToLeftTrack = 5,
            Queue = 6,

        }
    }
}