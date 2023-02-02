﻿namespace MiniCafe.Items
{
    public class BigAmericano : CustomItemGroup
    {
        public override string UniqueNameID => "big_americano";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Big Americano");
        public override Item DisposesTo => GetCastedGDO<Item, BigMug>();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemValue ItemValue => ItemValue.Small;
        public override Item DirtiesTo => GetCastedGDO<Item, BigMugDirty>();
        public override string ColourBlindTag => "BAm";
        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, BigEspresso>(),
                    GetGDO<Item>(ItemReferences.Water),
                },
                Min = 2,
                Max = 2,
                IsMandatory = true,
                OrderingOnly = false,
                RequiresUnlock = false,
            },
        };

        public override void OnRegister(GameDataObject gdo)
        {
            gdo.name = "Big Americano";

            BigMug.ApplyMugMaterials(Prefab.GetChild("mug"));
            Prefab.ApplyMaterialToChild("fill", "Americano", "Coffee - Black");
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");
        }
    }
}
