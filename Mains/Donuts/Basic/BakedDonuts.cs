﻿using MiniCafe.Sides;

namespace MiniCafe.Mains
{
    internal class BakedDonuts : CustomItem
    {
        public override string UniqueNameID => "baked_donuts";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Baked Donuts");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override int SplitCount => 2;
        public override Item SplitSubItem => GetCastedGDO<Item, PlainDonut>();
        public override List<Item> SplitDepletedItems => new() { GetCastedGDO<Item, PlainDonut>() };

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Tray", "Metal");

            gdo.AddObjectsSplittableView(Prefab, "donut", "Bread - Inside Cooked");
        }
    }
}