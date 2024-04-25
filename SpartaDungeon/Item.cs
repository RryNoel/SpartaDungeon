using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    internal class Item
    {
        public string Name { get; set; }

        public string Effect { get; set; }
        public int EffectValue { get; set; }
        public string Explan { get; set; }
        public int Price { get; set; }
        public bool Equipped { get; set; }

        public Item(string name, string effect, int effectValue, string explan, int price)
        {
            Name = name;
            Effect = effect;
            EffectValue = effectValue;
            Explan = explan;
            Price = price;
            Equipped = false;
        }


        public void Equip()
        {
            Equipped = true;
        }

        public void Unequip()
        {
            Equipped = false;
        }
    }
}
