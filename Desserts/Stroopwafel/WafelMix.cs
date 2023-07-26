﻿using UnityEngine;

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
        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetGDO<Item>(ItemReferences.Flour),
                    GetGDO<Item>(ItemReferences.EggCracked),
                    GetGDO<Item>(329108931),
                },
                IsMandatory = true,
                Max = 3,
                Min = 3
            },
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, Cinnamon>(),
                },
                Max = 1,
                Min = 1,
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
