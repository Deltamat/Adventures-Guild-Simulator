using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventures_Guild_Simulator
{
    public class Adventurer : GameObject
    {
        int id;
        string name;
        int level;
        int skill;
        int tempSkillBuff;
        Equipment weapon;
        Equipment chest;
        Equipment helmet;
        Equipment boot;
        Consumable consumable;
        GameObject helmetFrame;
        GameObject bootFrame;
        GameObject weaponFrame;
        GameObject chestFrame;
        GameObject consumableFrame;
        bool onQuest = false;        
       
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int Level { get => level; set => level = value; }
        public int Skill { get => skill; set => skill = value; }
        public int TempSkillBuff { get => tempSkillBuff; set => tempSkillBuff = value; }
        public Equipment Weapon { get => weapon; set => weapon = value; }
        public Equipment Chest { get => chest; set => chest = value; }
        public Equipment Helmet { get => helmet; set => helmet = value; }
        public Equipment Boot { get => boot; set => boot = value; }
        public Consumable Consumable { get => consumable; set => consumable = value; }
        public bool OnQuest { get => onQuest; set => onQuest = value; }
        public GameObject HelmetFrame { get => helmetFrame; set => helmetFrame = value; }
        public GameObject BootFrame { get => bootFrame; set => bootFrame = value; }
        public GameObject WeaponFrame { get => weaponFrame; set => weaponFrame = value; }
        public GameObject ChestFrame { get => chestFrame; set => chestFrame = value; }
        public GameObject ConsumableFrame { get => consumableFrame; set => consumableFrame = value; }

        public Adventurer(Vector2 position, string spriteName, int id, string name, int level, Equipment weapon, Equipment chest, Equipment helmet, Equipment boot, Consumable consumable) : base(position, spriteName)
        {
            Id = id;
            Name = name;
            Level = level;
            Weapon = weapon;
            Chest = chest;
            Helmet = helmet;
            Boot = boot;
            Consumable = consumable;

            if (weapon == null)
            {
               Weapon = new Equipment(Position, "Training Sword", "Weapon", "Weapon", "Common", 0, 1, true);
            }

            if (helmet == null)
            {
               Helmet = new Equipment(Position, "Training Helmet", "Helmet", "Helmet", "Common", 0, 10, true);
            }

            if (chest == null)
            {
               Chest = new Equipment(Position, "Training Chest", "Chest", "Chest", "Common", 0, 1, true);
            }

            if (boot == null)
            {
               Boot = new Equipment(Position, "Training Boot", "Boot", "Boot", "Common", 0, 1, true);
            }

            Weapon.Position = position + new Vector2(150, 0);
            Helmet.Position = position + new Vector2(300, 0);
            Chest.Position = position + new Vector2(450, 0);
            Boot.Position = position + new Vector2(600, 0);

            ChestFrame = new GameObject(Chest.Position + new Vector2(-60,-60), "Frame", Chest.Rarity);
            BootFrame = new GameObject(Boot.Position + new Vector2(-60, -60), "Frame", Boot.Rarity);
            WeaponFrame = new GameObject(Weapon.Position + new Vector2(-60, -60), "Frame", Weapon.Rarity);
            HelmetFrame = new GameObject(Helmet.Position + new Vector2(-60, -60), "Frame", Helmet.Rarity);

        }

        public override void Update(GameTime gameTime)
        {
            Skill = level;
            Skill = (int)((level + Weapon.SkillRating + Chest.SkillRating + Helmet.SkillRating + Boot.SkillRating) * 0.2 /*+ Consumable.SkillRating*/); //Might need to revise to use TempSkillBuff
            if (Consumable != null)
            {
                Skill += Consumable.SkillRating;
            }
           

            //Chest.Update(gameTime);
            //Boot.Update(gameTime);
            //Weapon.Update(gameTime);
            //Helmet.Update(gameTime);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Chest.selected == true)
            {
                Chest.Draw(spriteBatch);
            }

            if (Helmet.selected == true)
            {
                Helmet.Draw(spriteBatch);
            }

            if (Boot.selected == true)
            {
                Boot.Draw(spriteBatch);
            }

            if (Weapon.selected == true)
            {
                Weapon.Draw(spriteBatch);
            }
            
            
           
            base.Draw(spriteBatch);
            
        }
    }
}
