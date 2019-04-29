using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventures_Guild_Simulator
{
    public class Equipment : Item
    {
        /// <summary>
        /// Constructor for generating equipment from the database (because it has the "id")
        /// </summary>
        public Equipment(Vector2 position, int id, string name, string spriteName, string type, string rarity, int goldCost, int skillRating, bool isEquipped) : base(position, id, name, spriteName, type, rarity, goldCost, skillRating, isEquipped)
        {
            
        }
        
        /// <summary>
        /// Constructor for generating temporary equipment (because it doesn't need the "id")
        /// </summary>       
        public Equipment(Vector2 position, string name, string spriteName, string type, string rarity, int goldCost, int skillRating, bool isEquipped) : base(position, name, spriteName, type, rarity, goldCost, skillRating, isEquipped)
        {
            
        }

        public static void GenerateEquipment(Vector2 itemPosition)
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

            string tempItemType = "Weapon";
            int tempItemTypeGenerate = GameWorld.Instance.GenerateRandom(0, 4);

            if (tempItemTypeGenerate == 0)
            {
                tempItemType = "Weapon";
            }

            else if (tempItemTypeGenerate == 1)
            {
                tempItemType = "Chest";
            }

            else if (tempItemTypeGenerate == 2)
            {
                tempItemType = "helmet";
            }

            else if (tempItemTypeGenerate == 3)
            {
                tempItemType = "Boot";
            }

            string tempName = $"{ModelNaming.SelectPrefix(GameWorld.Instance.GenerateRandom(38, 100))} {tempItemType} of {ModelNaming.SelectPrefix(GameWorld.Instance.GenerateRandom(1, 39))}";

            double tempGoldCostGenerate = (Convert.ToDouble(GameWorld.Instance.GenerateRandom(1, 50)) / 100);
            int tempGoldCost = Convert.ToInt32(Math.Round(tempSkillRating * (tempGoldCostGenerate + 0.75)));


            Controller.Instance.CreateEquipment(tempName, tempItemType, tempItemType, tempRarity, tempGoldCost, tempSkillRating, false);
        }
    }
}
