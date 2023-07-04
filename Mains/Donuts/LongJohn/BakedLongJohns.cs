﻿using MiniCafe.Sides;

namespace MiniCafe.Mains
{
    internal class BakedLongJohns : CustomItem
    {
        public override string UniqueNameID => "baked_long_johns";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Baked Long Johns");
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override int SplitCount => 2;
        public override Item SplitSubItem => GetCastedGDO<Item, PlainLongJohn>();
        public override List<Item> SplitDepletedItems => new() { GetCastedGDO<Item, PlainLongJohn>() };
        public override float SplitSpeed => 1.5f;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Result = GetCastedGDO<Item, BurntLongJohns>(),
                Duration = 10,
                IsBad = true,
                Process = GetGDO<Process>(ProcessReferences.Cook)
            }
        };

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Tray", "Metal");

            gdo.AddObjectsSplittableView(Prefab, "donut", "Bread - Inside Cooked");
        }
    }
}
