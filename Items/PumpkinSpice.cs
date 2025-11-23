using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using MiniCafe.Items;
using System.Collections.Generic;
using UnityEngine;

namespace MiniCafe.Extras
{
    public class PumpkinSpice : CustomItemGroup
    {
        public override string UniqueNameID => "pumpkin_spice_ingredient";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Pumpkin Spice");
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override int SplitCount => 4;
        public override bool PreventExplicitSplit => true;
        public override bool AllowSplitMerging => true;
        public override Item SplitSubItem => GetCastedGDO<Item, PumpkinSpiceIngredient>();

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetGDO<Item>(ItemReferences.PumpkinPieces),
                    GetGDO<Item>(ItemReferences.Sugar)
                },
                Min = 2,
                Max = 2
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChild("Bowl", "Metal - Brass");
            Prefab.ApplyMaterialToChildren("Spice", "Pumpkin");

            ReflectionUtils.GetField<ObjectsSplittableView>("Objects").SetValue(Prefab.TryAddComponent<ObjectsSplittableView>(), new List<GameObject>()
            {
                Prefab.GetChild("Spice 0"),
                Prefab.GetChild("Spice 1"),
                Prefab.GetChild("Spice 2"),
                Prefab.GetChild("Spice 3"),
            });
        }
    }
}
