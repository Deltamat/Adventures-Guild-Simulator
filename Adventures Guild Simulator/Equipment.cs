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
            string weaponType = "Axe";
            int weaponNumber = 0;
            int armorNumber = 0;

            //Weapon rarity
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

            //Weapon type
            if (tempItemTypeGenerate == 0)
            {
                tempItemType = "Weapon";

                int tempNumber = GameWorld.Instance.GenerateRandom(0, 11);

                switch (tempNumber)
                {
                    case 0:
                        weaponType = "Axe";
                        weaponNumber = GameWorld.Instance.GenerateRandom(0, 24);
                        break;

                    case 1:
                        weaponType = "Bow";
                        weaponNumber = GameWorld.Instance.GenerateRandom(0, 11);
                        break;

                    case 2:
                        weaponType = "Club";
                        weaponNumber = GameWorld.Instance.GenerateRandom(0, 15);
                        break;

                    case 3:
                        weaponType = "Crossbow";
                        weaponNumber = GameWorld.Instance.GenerateRandom(0, 4);
                        break;

                    case 4:
                        weaponType = "Dagger";
                        weaponNumber = GameWorld.Instance.GenerateRandom(0, 18);
                        break;

                    case 5:
                        weaponType = "Fist";
                        weaponNumber = GameWorld.Instance.GenerateRandom(0, 8);
                        break;

                    case 6:
                        weaponType = "Gun";
                        weaponNumber = GameWorld.Instance.GenerateRandom(0, 8);
                        break;

                    case 7:
                        weaponType = "Hammer";
                        weaponNumber = GameWorld.Instance.GenerateRandom(0, 13);
                        break;

                    case 8:
                        weaponType = "Polearm";
                        weaponNumber = GameWorld.Instance.GenerateRandom(0, 11);
                        break;

                    case 9:
                        weaponType = "Staff";
                        weaponNumber = GameWorld.Instance.GenerateRandom(0, 13);
                        break;

                    case 10:
                        weaponType = "Sword";
                        weaponNumber = GameWorld.Instance.GenerateRandom(0, 24);
                        break;
                }
            }
            else if (tempItemTypeGenerate == 1)
            {
                tempItemType = "Chest";
                armorNumber = GameWorld.Instance.GenerateRandom(0, 23);

            }
            else if (tempItemTypeGenerate == 2)
            {
                tempItemType = "Helmet";
                armorNumber = GameWorld.Instance.GenerateRandom(0, 25);
            }
            else if (tempItemTypeGenerate == 3)
            {
                tempItemType = "Boot";
                armorNumber = GameWorld.Instance.GenerateRandom(0, 17);
            }

            //Random name generating


            double tempGoldCostGenerate = (Convert.ToDouble(GameWorld.Instance.GenerateRandom(1, 50)) / 100);
            int tempGoldCost = Convert.ToInt32(Math.Round(tempSkillRating * (tempGoldCostGenerate + 0.75)));
            if (tempGoldCost < 1)
            {
                tempGoldCost = 1;
            }

            if (tempItemType == "Weapon")
            {
                string tempName = $"{Controller.Instance.GetName(38, 100)} {weaponType} of {Controller.Instance.GetName(1, 39)}";
                Controller.Instance.CreateEquipment(tempName, $"Items/Weapons/{weaponType}/{weaponNumber}", tempItemType, tempRarity, tempGoldCost, tempSkillRating, false);
            }

            else 
            {
                string tempName = $"{Controller.Instance.GetName(38, 100)} {tempItemType} of {Controller.Instance.GetName(1, 39)}";
                Controller.Instance.CreateEquipment(tempName, $"Items/Armor/{tempItemType}/{armorNumber}", tempItemType, tempRarity, tempGoldCost, tempSkillRating, false);
            }
        }

        public static Equipment ReturnEquipment(Vector2 itemPosition)
        {
            int tempRarityGenerator = GameWorld.Instance.GenerateRandom(0, 100);
            int tempSkillRating;
            string tempRarity;
            string weaponType = "Axe";
            int weaponNumber = 0;
            int armourNumber = 0;

            //Weapon rarity
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

            //Weapon type
            if (tempItemTypeGenerate == 0)
            {
                tempItemType = "Weapon";

                int tempNumber = GameWorld.Instance.GenerateRandom(0, 11);

                switch (tempNumber)
                {
                    case 0:
                        weaponType = "Axe";
                        weaponNumber = GameWorld.Instance.GenerateRandom(0, 24);
                    break;

                    case 1:
                        weaponType = "Bow";
                        weaponNumber = GameWorld.Instance.GenerateRandom(0, 11);
                        break;

                    case 2:
                        weaponType = "Club";
                        weaponNumber = GameWorld.Instance.GenerateRandom(0, 15);
                        break;

                    case 3:
                        weaponType = "Crossbow";
                        weaponNumber = GameWorld.Instance.GenerateRandom(0, 4);
                        break;

                    case 4:
                        weaponType = "Dagger";
                        weaponNumber = GameWorld.Instance.GenerateRandom(0, 18);
                        break;

                    case 5:
                        weaponType = "Fist";
                        weaponNumber = GameWorld.Instance.GenerateRandom(0, 8);
                        break;

                    case 6:
                        weaponType = "Gun";
                        weaponNumber = GameWorld.Instance.GenerateRandom(0, 8);
                        break;

                    case 7:
                        weaponType = "Hammer";
                        weaponNumber = GameWorld.Instance.GenerateRandom(0, 13);
                        break;

                    case 8:
                        weaponType = "Polearm";
                        weaponNumber = GameWorld.Instance.GenerateRandom(0, 11);
                        break;

                    case 9:
                        weaponType = "Staff";
                        weaponNumber = GameWorld.Instance.GenerateRandom(0, 13);
                        break;

                    case 10:
                        weaponType = "Sword";
                        weaponNumber = GameWorld.Instance.GenerateRandom(0, 24);
                        break;
                }
            }
            else if (tempItemTypeGenerate == 1)
            {
                tempItemType = "Chest";
                armourNumber = GameWorld.Instance.GenerateRandom(0, 23);

            }
            else if (tempItemTypeGenerate == 2)
            {
                tempItemType = "Helmet";
                armourNumber = GameWorld.Instance.GenerateRandom(0, 25);
            }
            else if (tempItemTypeGenerate == 3)
            {
                tempItemType = "Boot";
                armourNumber = GameWorld.Instance.GenerateRandom(0, 17);
            }

            //Random name generating


            double tempGoldCostGenerate = (Convert.ToDouble(GameWorld.Instance.GenerateRandom(1, 50)) / 100);
            int tempGoldCost = Convert.ToInt32(Math.Round(tempSkillRating * (tempGoldCostGenerate + 0.75)));
            if (tempGoldCost < 1)
            {
                tempGoldCost = 1;
            }

            if (tempItemType == "Weapon")
            {
                string tempName = $"{Controller.Instance.GetName(38, 100)} {weaponType} of {Controller.Instance.GetName(1, 39)}";
                return new Equipment(itemPosition, tempName, $"Items/Weapons/{weaponType}/{weaponNumber}", tempItemType, tempRarity, tempGoldCost, tempSkillRating, false);
            }

            else
            {
                string tempName = $"{Controller.Instance.GetName(38, 100)} {tempItemType} of {Controller.Instance.GetName(1, 39)}";
                return new Equipment(itemPosition, tempName, $"Items/Armor/{tempItemType}/{armourNumber}", tempItemType, tempRarity, tempGoldCost, tempSkillRating, false);
            }
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
