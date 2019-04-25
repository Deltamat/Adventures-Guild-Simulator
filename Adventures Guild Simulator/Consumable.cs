using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventures_Guild_Simulator
{
    class Consumable : Item
    {
        int uses;
                
        public int Uses { get => uses; set => uses = value; }

        /// <summary>
        /// Constructor for generating consumables from the database (because it has the "id")
        /// </summary>
        public Consumable(Vector2 position, int id, string name, string spriteName, string type, string rarity, int goldCost, int skillRating, int uses) : base(position, id, name, spriteName, type, rarity, goldCost, skillRating)
        {
            Uses = uses;
        }
        /// <summary>
        /// Constructor for generating temporary consumables (because it doesn't need the "id")
        /// </summary>       
        public Consumable(Vector2 position, string spriteName, string rarity, int skillRating, string type, int goldCost, string name, int uses) : base(position, spriteName, rarity, skillRating, type, goldCost, name)
        {
            Uses = uses;
        }

        public static void GenerateConsumable(Vector2 itemPosition)
        {
            int tempRarityGenerator = GameWorld.Instance.GenerateRandom(0, 100);
            int tempSkillRating;
            string tempRarity;

            if (tempRarityGenerator == 99)
            {
                tempRarity = "Legendary";
                tempSkillRating = GameWorld.Instance.GenerateRandom(0, 20) + 80;
            }

            else if (tempRarityGenerator > 90)
            {
                tempRarity = "Epic";
                tempSkillRating = GameWorld.Instance.GenerateRandom(0, 20) + 60;
            }

            else if (tempRarityGenerator > 75)
            {
                tempRarity = "Rare";
                tempSkillRating = GameWorld.Instance.GenerateRandom(0, 20) + 40;
            }

            else if (tempRarityGenerator > 50)
            {
                tempRarity = "Uncommon";
                tempSkillRating = GameWorld.Instance.GenerateRandom(0, 20) + 20;
            }

            else
            {
                tempRarity = "Common";
                tempSkillRating = GameWorld.Instance.GenerateRandom(0, 20) + 1;
            }

            string tempItemType = "Potion";
            int tempItemTypeGenerate = GameWorld.Instance.GenerateRandom(0, 4);

            if (tempItemTypeGenerate == 0)
            {
                tempItemType = "Potion";
            }

            else if (tempItemTypeGenerate == 1)
            {
                tempItemType = "Potion";
            }

            else if (tempItemTypeGenerate == 2)
            {
                tempItemType = "Potion";
            }

            else if (tempItemTypeGenerate == 3)
            {
                tempItemType = "Potion";
            }

            double tempGoldCostGenerate = (Convert.ToDouble(GameWorld.Instance.GenerateRandom(1, 50)) / 100);
            int tempGoldCost = Convert.ToInt32(Math.Round(tempSkillRating * (tempGoldCostGenerate + 0.75)));


            GameWorld.itemList.Add(new Consumable(itemPosition, tempItemType, tempRarity, tempSkillRating, tempItemType, tempGoldCost, tempItemType, 1));
        }
    }
}
