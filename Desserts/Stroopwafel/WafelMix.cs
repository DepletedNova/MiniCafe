using IngredientLib.Ingredient.Items;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace MiniCafe.Sides
{
    internal class WafelMix : CustomItemGroup
    {
        public override string UniqueNameID => "wafel_mix";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Wafel Mix");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetGDO<Item>(ItemReferences.Dough),
                    GetCastedGDO<Item, Cinnamon>(),
                },
                Max = 2,
                Min = 2
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChild("Bowl", "Metal");
            Prefab.ApplyMaterialToChild("Ball", "Cinnamon");

        }
    }
}
