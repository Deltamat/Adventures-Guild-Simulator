using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventures_Guild_Simulator
{
    public class Quest : GameObject
    {
        Random rng = new Random();

        int difficultyRating;
        int reward;
        float durationTime;
        float progressTime;
        float expireTime;
        float timeToExpire;

        string enemy;
        bool ongoing;

        public Quest()
        {
            //Random difficulty, duration time and expire time
            DifficultyRating = rng.Next(1, 101);
            DurationTime = rng.Next(60, 121);
            ExpireTime = rng.Next(30, 61);

            //Chooses enemies and gold reward depending on difficulty rating
            if (DifficultyRating <= 10)
            {
                Enemy = "rat";
                Reward = rng.Next(10, 16);
            }
            else if (DifficultyRating > 10 && DifficultyRating <= 20)
            {
                Enemy = "bat";
                Reward = rng.Next(16, 22);
            }
            else if (DifficultyRating > 20 && DifficultyRating <= 30)
            {
                Enemy = "wolf";
                Reward = rng.Next(22, 28);
            }
            else if (DifficultyRating > 30 && DifficultyRating <= 40)
            {
                Enemy = "bear";
                Reward = rng.Next(28, 34);
            }
            else if (DifficultyRating > 40 && DifficultyRating <= 50)
            {
                Enemy = "orc";
                Reward = rng.Next(34, 40);
            }
            else if (DifficultyRating > 50 && DifficultyRating <= 60)
            {
                Enemy = "skeleton";
                Reward = rng.Next(40, 46);
            }
            else if (DifficultyRating > 60 && DifficultyRating <= 70)
            {
                Enemy = "livingarmour";
                Reward = rng.Next(46, 52);
            }
            else if (DifficultyRating > 70 && DifficultyRating <= 80)
            {
                Enemy = "warlock";
                Reward = rng.Next(52, 58);
            }
            else if (DifficultyRating > 80 && DifficultyRating <= 90)
            {
                Enemy = "giantspider";
                Reward = rng.Next(58, 64);
            }
            else if (DifficultyRating > 90)
            {
                Enemy = "dragon";
                Reward = rng.Next(64, 70);
            }
        }

        public Quest(Vector2 position, string spriteName) : base (position, spriteName)
        {
            
        }

        public int DifficultyRating { get => difficultyRating; set => difficultyRating = value; }
        public int Reward { get => reward; set => reward = value; }
        public float DurationTime { get => durationTime; set => durationTime = value; }
        public float ProgressTime { get => progressTime; set => progressTime = value; }
        public float ExpireTime { get => expireTime; set => expireTime = value; }
        public float TimeToExpire { get => timeToExpire; set => timeToExpire = value; }
        public string Enemy { get => enemy; set => enemy = value; }

        public override void Update(GameTime gameTime)
        {
            if (ongoing == false) //Whether any one is assinged to the quest
            {
                //Counts down how much time before the quest expires
                TimeToExpire += (float)GameWorld.Instance.globalDeltaTime;
                if (TimeToExpire > ExpireTime)
                {
                    GameWorld.Instance.questsToBeRemoved.Add(this);
                }
            }
            else
            {
                //Counts down how much time before the quest is completed
                ProgressTime += (float)GameWorld.Instance.globalDeltaTime;
                if (ProgressTime > DurationTime)
                {
                    GameWorld.Instance.questsToBeRemoved.Add(this);
                    //complete quest, give rewards
                }
            }
        }
    }
}
