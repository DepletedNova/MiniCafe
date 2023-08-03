using IngredientLib.Ingredient.Items;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;
using static KitchenLib.Utils.GDOUtils;
using static KitchenLib.Utils.MaterialUtils;
using static MiniCafe.Helper;

namespace MiniCafe.Extras
{
    public class UnrolledCroissant : CustomItemGroup
    {
        public override string UniqueNameID => "unrolled_croissant";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Unwrapped Croissant");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Process = GetGDO<Process>(ProcessReferences.Knead),
                Duration = 0.75f,
                Result = GetCastedGDO<Item, UncookedCroissant>()
            }
        };

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                IsMandatory = true,
                Max = 2,
                Min = 2,
                Items = new()
                {
                    GetGDO<Item>(ItemReferences.Dough),
                    GetCastedGDO<Item, Butter>(),
                }
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChild("Dough", "Raw Pastry");
            Prefab.ApplyMaterialToChild("Butter", "Butter");
        }
    }
}
