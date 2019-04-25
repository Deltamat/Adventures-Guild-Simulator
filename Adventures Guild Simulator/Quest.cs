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
        Adventurer assignedAdventurer;

        public Quest()
        {
            //Random difficulty, duration time and expire time
            DifficultyRating = GameWorld.Instance.GenerateRandom(1, 101);
            DurationTime = GameWorld.Instance.GenerateRandom(60, 121);
            ExpireTime = GameWorld.Instance.GenerateRandom(30, 61);

            //Chooses enemies and gold reward depending on difficulty rating
            if (DifficultyRating <= 10)
            {
                Enemy = "rat";
                Reward = GameWorld.Instance.GenerateRandom(10, 16);
            }
            else if (DifficultyRating > 10 && DifficultyRating <= 20)
            {
                Enemy = "bat";
                Reward = GameWorld.Instance.GenerateRandom(16, 22);
            }
            else if (DifficultyRating > 20 && DifficultyRating <= 30)
            {
                Enemy = "wolf";
                Reward = GameWorld.Instance.GenerateRandom(22, 28);
            }
            else if (DifficultyRating > 30 && DifficultyRating <= 40)
            {
                Enemy = "bear";
                Reward = GameWorld.Instance.GenerateRandom(28, 34);
            }
            else if (DifficultyRating > 40 && DifficultyRating <= 50)
            {
                Enemy = "orc";
                Reward = GameWorld.Instance.GenerateRandom(34, 40);
            }
            else if (DifficultyRating > 50 && DifficultyRating <= 60)
            {
                Enemy = "skeleton";
                Reward = GameWorld.Instance.GenerateRandom(40, 46);
            }
            else if (DifficultyRating > 60 && DifficultyRating <= 70)
            {
                Enemy = "livingarmour";
                Reward = GameWorld.Instance.GenerateRandom(46, 52);
            }
            else if (DifficultyRating > 70 && DifficultyRating <= 80)
            {
                Enemy = "warlock";
                Reward = GameWorld.Instance.GenerateRandom(52, 58);
            }
            else if (DifficultyRating > 80 && DifficultyRating <= 90)
            {
                Enemy = "giantspider";
                Reward = GameWorld.Instance.GenerateRandom(58, 64);
            }
            else if (DifficultyRating > 90)
            {
                Enemy = "dragon";
                Reward = GameWorld.Instance.GenerateRandom(64, 70);
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
                    float failureChance;
                    //Every positive skill point difference between a quest's difficulty rating, and the adventurer's skill rating equals a 5% failure rate
                    //I.E. difficultyRating = 10, assignedAdventurer total skill = 5. 5 point difference equals 25% failure chance
                    failureChance = (DifficultyRating - (assignedAdventurer.Skill + assignedAdventurer.TempSkillBuff)) * 5; 
                    if (GameWorld.Instance.GenerateRandom(0, 101) > failureChance) //Checks whether the quest was succesfully completed
                    {
                        GameWorld.Instance.gold += Reward; //Adds the gold reward to the player's stats
                        //random item
                    }
                    else if (GameWorld.Instance.GenerateRandom(0, 101) * (failureChance * 0.1) > 50) //Quest failed, rolls chance for the adventurer to die
                    {
                        GameWorld.Instance.adventurersDic.Remove(assignedAdventurer.Id); //changed it to a dictionary
                    }
                }
            }
        }
    }
}
