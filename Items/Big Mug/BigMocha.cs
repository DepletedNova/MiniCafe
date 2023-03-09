namespace MiniCafe.Items
{
    public class BigMocha : CustomItemGroup<BigMocha.ItemGroupViewAccessed>
    {
        public override string UniqueNameID => "big_mocha";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Big Mocha");
        public override Item DisposesTo => GetCastedGDO<Item, BigMug>();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemValue ItemValue => ItemValue.Large;
        public override Item DirtiesTo => GetCastedGDO<Item, BigMugDirty>();
        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, WhippedCream>()
                },
                Min = 0,
                Max = 1,
                RequiresUnlock = true,
            },
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, BigCappuccino>(),
                    GetCastedGDO<Item, ChocolateSauce>()
                },
                Min = 2,
                Max = 2,
                IsMandatory = true,
            },
        };

        private bool registered = false;
        public override void OnRegister(GameDataObject gdo)
        {
            gdo.name = "Big Mocha";

            var view = Prefab.GetComponent<ItemGroupViewAccessed>();
            view.Setup();

            BigMug.ApplyMugMaterials(Prefab.GetChild("mug"));
            Prefab.ApplyMaterialToChild("fill", "Coffee Blend", "Chocolate");
            var whipped = Prefab.GetChild("cream");
            whipped.ApplyMaterial("Coffee Cup");
            whipped.ApplyMaterialToChild("chocolate", "Chocolate");
            
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");

            if (!registered)
            {
                ColorblindUtils.setColourBlindLabelObjectOnItemGroupView(view, ColorblindUtils.cloneColourBlindObjectAndAddToItem(gdo as Item));
                registered = true;
            }
        }

        public class ItemGroupViewAccessed : ItemGroupView
        {
            public void Setup()
            {
                ComponentGroups = new()
                {
                    new()
                    {
                        GameObject = gameObject.GetChild("cream"),
                        Item = GetCastedGDO<Item, WhippedCream>()
                    }
                };

                ComponentLabels = new()
                {

                    new()
                    {
                        Text = "BMo",
                        Item = GetCastedGDO<Item, BigCappuccino>()
                    },
                    new()
                    {
                        Text = "W",
                        Item = GetCastedGDO<Item, WhippedCream>()
                    }
                };

            }
        }

    }
}
