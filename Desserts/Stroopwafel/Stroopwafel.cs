using IngredientLib.Ingredient.Items;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using MiniCafe.Desserts;
using System.Collections.Generic;
using UnityEngine;
using static KitchenLib.Utils.MaterialUtils;

namespace MiniCafe.Sides
{
    public class Stroopwafel : CustomItemGroup
    {
        public override string UniqueNameID => "stroopwafel";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Stroopwafel");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemValue ItemValue => ItemValue.Small;

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, SyrupIngredient>(),
                    GetCastedGDO<Item, Wafel>()
                },
                IsMandatory = true,
                Max = 2,
                Min = 2
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChild("wafel", "Bread - Cooked");
            Prefab.ApplyMaterialToChild("stroop", "Cooked Batter");
            Prefab.ApplyMaterialToChild("plate", "Plate", "Plate - Ring");
        }
    }
}
