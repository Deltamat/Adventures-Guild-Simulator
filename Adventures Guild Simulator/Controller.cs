﻿using System;
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

        public List<Adventurer> LoadAdventurers()
        {
            return adventurer.LoadAdventurers();
        }

        public void RemoveAdventurer(int id)
        {
            adventurer.DeleteAdventurerByID(id);
        }
        #endregion

        #region Consumable

        #endregion

        #region Equipment

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
