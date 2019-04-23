using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventures_Guild_Simulator
{
    public class Item : GameObject
    {
        int id;
        int skillRating;
        int goldCost;
        string type;
        string rarity;
        int goldCost;
        string name;
        

        public int Id { get => id; set => id = value; }
        public int SkillRating { get => skillRating; set => skillRating = value; }
        public string Type { get => type; set => type = value; }
        public string Name { get => name; set => name = value; }
        public int GoldCost { get => goldCost; set => goldCost = value; }
        public string Rarity { get => rarity; set => rarity = value; }

        public Item(Vector2 position, string spriteName, int id, string rarity, int skillRating, string type, int goldCost, string name) : base(position, spriteName)
        {            
            Rarity = rarity;
            SkillRating = skillRating;
            Type = type;
            Name = name;
            GoldCost = goldCost;

        }

        public static void GenerateItem(Vector2 itemPosition)
        {
            int tempRarityGenerator = GameWorld.GenerateRandom(0, 100);
            int tempSkillRating;
            string tempRarity;

            if (tempRarityGenerator == 99)
            {
                tempRarity = "Legendary";
                tempSkillRating = GameWorld.GenerateRandom(0, 20) + 80;
            }

            else if (tempRarityGenerator > 90)
            {
                tempRarity = "Epic";
                tempSkillRating = GameWorld.GenerateRandom(0, 20) + 60;
            }

            else if (tempRarityGenerator > 75)
            {
                tempRarity = "Rare";
                tempSkillRating = GameWorld.GenerateRandom(0, 20) + 40;
            }

            else if (tempRarityGenerator > 50)
            {
                tempRarity = "Uncommon";
                tempSkillRating = GameWorld.GenerateRandom(0, 20) + 20;
            }

            else
            {
                tempRarity = "Common";
                tempSkillRating = GameWorld.GenerateRandom(0, 20) + 1;
            }

            string tempItemType = "Weapon";
            int tempItemTypeGenerate = GameWorld.GenerateRandom(0, 4);

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
                tempItemType = "Head";
            }

            else if (tempItemTypeGenerate == 3)
            {
                tempItemType = "Boot";
            }

            double tempGoldCostGenerate = (Convert.ToDouble(GameWorld.GenerateRandom(1, 50)) / 100);
            int tempGoldCost = Convert.ToInt32(Math.Round(tempSkillRating * (tempGoldCostGenerate + 0.75)));


            GameWorld.itemList.Add(new Item(itemPosition, tempItemType, tempRarity, tempSkillRating, tempItemType,tempGoldCost, tempItemType));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, Position, Color.White);
            
            if (Rarity == "Common")
            {
                spriteBatch.DrawString(GameWorld.font, $"{Type}", Position + new Vector2(100, 0), Color.White);
            }

            else if (Rarity == "Uncommon")
            {
                spriteBatch.DrawString(GameWorld.font, $"{Type}", Position + new Vector2(100, 0), Color.Green);
            }

            else if (Rarity == "Rare")
            {
                spriteBatch.DrawString(GameWorld.font, $"{Type}", Position + new Vector2(100, 0), Color.Blue);
            }

            else if (Rarity == "Epic")
            {
                spriteBatch.DrawString(GameWorld.font, $"{Type}", Position + new Vector2(100, 0), Color.Purple);
            }

            else if (Rarity == "Legendary")
            {
                spriteBatch.DrawString(GameWorld.font, $"{Type}", Position + new Vector2(100, 0), Color.Orange);
            }

            spriteBatch.DrawString(GameWorld.font, $"Cost: {GoldCost}", Position + new Vector2(200, 0), Color.Gold);
            spriteBatch.DrawString(GameWorld.font, $"GearScore: {skillRating}", Position + new Vector2(100, 100), Color.White);

        }
    }
}
