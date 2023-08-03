using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using MiniCafe.Appliances;
using System.Collections.Generic;
using UnityEngine;
using static KitchenLib.Utils.GDOUtils;
using static KitchenLib.Utils.MaterialUtils;
using static MiniCafe.Helper;

namespace MiniCafe.Coffee.Large
{
    internal class LargeCup : CustomItem
    {
        public override string UniqueNameID => "large_cup";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Large Cup");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override Appliance DedicatedProvider => GetCastedGDO<Appliance, LargeCoffeeMachine>();

        public override string ColourBlindTag => "L";

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Process = GetGDO<Process>(ProcessReferences.FillCoffee),
                Duration = 4,
                Result = GetCastedGDO<Item, LargeCoffee>()
            }
        };

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChild("cup", "Light Coffee Cup");
        }
    }
}
