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
        public Consumable(int id, int skillRating, string type, string name) : base(id, skillRating, type, name)
        {
        }
    }
}
