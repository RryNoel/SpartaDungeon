using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    internal class Character
    {
        public string Name { get; set; }
        public string Job { get; set; }
        public int Level { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Health { get; set; }
        public int Gold { get; set; }

        public int BaseAttack {  get; set; }
        public int BaseDefense { get; set; }
        public List<Item> Inventory { get; set; }

        public Character(string name, string job) 
        {
            Name = name;
            Job = job;
            Level = 1;
            Attack = 10;
            Defense = 5;
            Health = 10;
            Gold = 1500;
            BaseAttack = 10;
            BaseDefense = 5;
            Inventory = new List<Item>();
        }
    }
}
