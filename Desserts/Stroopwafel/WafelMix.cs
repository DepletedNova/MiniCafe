using UnityEngine;

namespace MiniCafe.Sides
{
    internal class WafelMix : CustomItemGroup
    {
        public override string UniqueNameID => "wafel_mix";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Wafel Mix");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Duration = 4.5f,
                Result = GetCastedGDO<Item, WafelTray>()
            }
        };
        public override List<ItemGroupView.ColourBlindLabel> Labels => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, ThinBatter>(),
                Text = "Ba"
            },
            new()
            {
                Item = GetCastedGDO<Item, Cinnamon>(),
                Text = "Ci"
            },
        };
        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, ThinBatter>(),
                    GetCastedGDO<Item, Cinnamon>(),
                },
                Max = 2,
                Min = 2,
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Bowl", "Metal");
            Prefab.ApplyMaterialToChildCafe("Batter", "Flour", "Raw Pastry");
            Prefab.ApplyMaterialToChildCafe("Cinnamon", "Cinnamon");

        }
    }
}
