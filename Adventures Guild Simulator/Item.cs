using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventures_Guild_Simulator
{
    class Item : GameObject
    {
        int id;
        int skillRating;
        string type;
        string name;

        public int Id { get => id; set => id = value; }
        public int SkillRating { get => skillRating; set => skillRating = value; }
        public string Type { get => type; set => type = value; }
        public string Name { get => name; set => name = value; }

        public Item(int id, int skillRating, string type, string name)
        {
            Id = id;
            SkillRating = skillRating;
            Type = type;
            Name = name;
        }
    }
}
