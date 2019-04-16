using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventures_Guild_Simulator
{
    class Quest : GameObject
    {
        Random rng = new Random();

        int difficultyRating;
        int reward;
        float durationTime;
        float progressTime;
        float expireTime;

        string enemy;

        public Quest()
        {
            difficultyRating = rng.Next(1, 101);
            reward = rng.Next(10, 21);
            durationTime = rng.Next(60, 121);
            expireTime = rng.Next(30, 61);

            if (difficultyRating <= 10)
            {
                enemy = "rat";
            }
            else if (difficultyRating > 10 && difficultyRating <= 20)
            {
                enemy = "boar";
            }
            else if (difficultyRating > 20 && difficultyRating <= 30)
            {
                enemy = "wolf";
            }
            else if (difficultyRating > 30 && difficultyRating <= 40)
            {
                enemy = "bandit";
            }
            else if (difficultyRating > 40 && difficultyRating <= 50)
            {
                enemy = "direwolf";
            }
            else if (difficultyRating > 50 && difficultyRating <= 60)
            {
                enemy = "skeleton";
            }
            else if (difficultyRating > 60 && difficultyRating <= 70)
            {
                enemy = "warlock";
            }
            else if (difficultyRating > 70 && difficultyRating <= 80)
            {
                enemy = "manticore";
            }
            else if (difficultyRating > 80 && difficultyRating <= 90)
            {
                enemy = "hydra";
            }
            else if (difficultyRating > 90 && difficultyRating <= 100)
            {
                enemy = "dragon";
            }
        }
    }
}
