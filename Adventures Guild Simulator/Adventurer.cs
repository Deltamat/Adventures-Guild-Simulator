using Microsoft.Xna.Framework;
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
        }

        public override void Update(GameTime gameTime)
        {
            Skill = level;
            //Skill = (int)((level + Weapon.SkillRating + Chest.SkillRating + Helmet.SkillRating + Boot.SkillRating) * 0.2 + Consumable.SkillRating); //Might need to revise to use TempSkillBuff
            if (Consumable != null)
            {
                Skill += Consumable.SkillRating;
            }
        }
    }
}
