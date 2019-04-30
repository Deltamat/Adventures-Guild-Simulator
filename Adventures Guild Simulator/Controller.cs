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

        public void SetAdventurerLevel(int id, int level)
        {
            adventurer.SetLevel(id, level);
        }

        public void UpdateAdventurerWeapon(int weaponId)
        {
            adventurer.UpdateWeapon(weaponId);
        }
        public void UpdateAdventurerHelmet(int helmetId)
        {
            adventurer.UpdateHelmet(helmetId);
        }
        public void UpdateAdventurerChest(int chestId)
        {
            adventurer.UpdateChest(chestId);
        }
        public void UpdateAdventurerBoot(int bootId)
        {
            adventurer.UpdateBoot(bootId);
        }
        public void UpdateAdventurerConsumeable(int consumableId)
        {
            adventurer.UpdateConsumable(consumableId);
        }
        #endregion

        #region Consumable
        public Dictionary<int, Consumable> LoadConsumable()
        {
            return consumable.LoadConsumable();
        }
        public Consumable CreateConsumable(string name, string spriteName, string type, string rarity, int goldCost, int skillRating, bool isEquipped, int uses)
        {
            return consumable.CreateConsumable(name, spriteName, type, rarity, goldCost, skillRating, isEquipped, uses);
        }
        public void SellConsumable(int id)
        {
            consumable.SellConsumable(id);
        }
        public void UpdateConsumable(int id, int newUses)
        {
            consumable.UpdateUses(id, newUses);
        }
        #endregion

        #region Equipment
        public Dictionary<int, Equipment> LoadEquipment()
        {
            return equipment.LoadEquipment();
        }
        public Equipment CreateEquipment(string name, string spriteName, string type, string rarity, int goldCost, int skillRating, bool isEquipped)
        {
            return equipment.CreateEquipment(name, spriteName, type, rarity, goldCost, skillRating, isEquipped);
        }
        public void SellEquipement(int id)
        {
            equipment.SellEquipment(id);
        }
        public void EquipEquipment(int id)
        {
            equipment.EquipEquipment(id);
        }
        public void UnequipEquipment(int id)
        {
            equipment.UnequipEquipment(id);
        }


        #endregion

        #region Stat
        public void UpdateStats()
        {
            stat.UpdateStats();
        }

        public int LoadGold()
        {
            return stat.LoadGold();
        }

        public int LoadDeaths()
        {
            return stat.LoadDeaths();
        }        

        public int LoadCompletedQuests()
        {
            return stat.LoadCompletedQuests();
        }
        
        #endregion

        public void Reset()
        {
            adventurer.Reset();
            stat.UpdateStats();
            equipment.Reset();
            consumable.Reset();

        }

        public void Naming()
        {
            if (!naming.NamesCreated())
            {
                naming.CreateNames();
            }
        }

        public string GetName(int i, int j)
        {
            return naming.SelectPrefix(GameWorld.Instance.GenerateRandom(i, j));
        }
    }
}
