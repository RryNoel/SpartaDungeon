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
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        public int Gold { get; set; }

        public int BaseAttack {  get; set; }
        public int BaseDefense { get; set; }
        public List<Item> Inventory { get; set; }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold) 
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
            BaseAttack = 10;
            BaseDefense = 5;
            Inventory = new List<Item>();
        }
    }
}
