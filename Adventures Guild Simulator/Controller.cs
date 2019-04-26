using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventures_Guild_Simulator
{
    class Controller
    {
        ModelAdventurer adventurer;
        ModelConsumable consumable;
        ModelEquipment equipment;
        ModelInventory inventory;
        ModelNaming naming;
        ModelStat stat;

        static Controller instance;
        static public Controller Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Controller();
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }
        private Controller()
        {
            adventurer = new ModelAdventurer();
            consumable = new ModelConsumable();
            equipment = new ModelEquipment();
            inventory = new ModelInventory();
            naming = new ModelNaming();
            stat = new ModelStat();
        }

        #region Adventurer
        public Adventurer CreateAdventurer(string name)
        {
            return adventurer.CreateAdventurer(name);
        }

        public Dictionary<int, Adventurer> LoadAdventurers()
        {
            return adventurer.LoadAdventurers();
        }

        public void RemoveAdventurer(int id)
        {
            adventurer.DeleteAdventurerByID(id);
        }
        #endregion

        #region Consumable
        public Dictionary<int, Consumable> LoadConsumable()
        {
            return consumable.LoadConsumable();
        }
        public Consumable CreateConsumable(string name, string spriteName, string type, string rarity, int goldCost, int skillRating, int uses)
        {
            return consumable.CreateConsumable(name, spriteName, type, rarity, goldCost, skillRating, uses);
        }
        #endregion

        #region Equipment
        public Dictionary<int, Equipment> LoadEquipment()
        {
            return equipment.LoadEquipment();
        }
        public Equipment CreateEquipment(string name, string spriteName, string type, string rarity, int goldCost, int skillRating)
        {
            return equipment.CreateEquipment(name, spriteName, type, rarity, goldCost, skillRating);
        }
        #endregion

        #region Inventory

        #endregion

        #region Stat
        public void SetGold(int gold)
        {
            stat.SetGold(gold);
        }

        public int LoadGold()
        {
            return stat.LoadGold();
        }
        #endregion

        

    }
}
