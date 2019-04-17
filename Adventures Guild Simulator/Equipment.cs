using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventures_Guild_Simulator
{
    class Equipment : Item
    {
        bool isEquipped;

        public bool IsEquipped { get => isEquipped; set => isEquipped = value; }

        public Equipment(Vector2 position, string spriteName, int id, int skillRating, string type, string name, bool isEquipped) : base(position, spriteName, id, skillRating, type, name)
        {            
            IsEquipped = isEquipped;            
        }
    }
}
