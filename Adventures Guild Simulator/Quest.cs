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

        public Quest(Vector2 position, string spriteName) : base (position, spriteName)
        {
            difficultyRating = rng.Next(1, 101);
            durationTime = rng.Next(60, 121);
            expireTime = rng.Next(30, 61);

            if (difficultyRating <= 10)
            {
                enemy = "rat";
                reward = rng.Next(1, 6);
            }
            else if (difficultyRating > 10 && difficultyRating <= 20)
            {
                enemy = "bat";
                reward = rng.Next(6, 12);
            }
            else if (difficultyRating > 20 && difficultyRating <= 30)
            {
                enemy = "wolf";
                reward = rng.Next(12, 18);
            }
            else if (difficultyRating > 30 && difficultyRating <= 40)
            {
                enemy = "bear";
                reward = rng.Next(18, 24);
            }
            else if (difficultyRating > 40 && difficultyRating <= 50)
            {
                enemy = "orc";
                reward = rng.Next(24, 30);
            }
            else if (difficultyRating > 50 && difficultyRating <= 60)
            {
                enemy = "skeleton";
                reward = rng.Next(30, 36);
            }
            else if (difficultyRating > 60 && difficultyRating <= 70)
            {
                enemy = "livingarmour";
                reward = rng.Next(36, 42);
            }
            else if (difficultyRating > 70 && difficultyRating <= 80)
            {
                enemy = "warlock";
                reward = rng.Next(42, 48);
            }
            else if (difficultyRating > 80 && difficultyRating <= 90)
            {
                enemy = "giantspider";
                reward = rng.Next(48, 54);
            }
            else if (difficultyRating > 90)
            {
                enemy = "dragon";
                reward = rng.Next(54, 60);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (ongoing == false)
            {
                timeToExpire += (float)GameWorld.Instance.globalDeltaTime;
                if (timeToExpire > expireTime)
                {
                    GameWorld.Instance.quests.Remove(this);
                }
            }
            else
            {
                progressTime += (float)GameWorld.Instance.globalDeltaTime;
                if (progressTime > durationTime)
                {
                    GameWorld.Instance.quests.Remove(this);
                    //complete quest, give rewards
                }
            }
        }
    }
}
