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
    public class UnmixedSconeDough : CustomItemGroup, IWontRegister
    {
        public override string UniqueNameID => "unmixed_scone";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Unmixed Scone Dough");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Duration = 3.5f,
                Result = GetCastedGDO<Item, SconePlatter>()
            }
        };

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Max = 2,
                Min = 2,
                Items = new()
                {
                    GetCastedGDO<Item, EggDough>(),
                    GetCastedGDO<Item, WhippingCreamIngredient>()
                }
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChild("Bowl", "Metal Dark");
            Prefab.ApplyMaterialToChild("Dough", "Egg Dough");
            Prefab.ApplyMaterialToChild("Whipping", "Coffee Cup");
        }
    }
}
