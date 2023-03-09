namespace MiniCafe.Items
{
    public class SmallMocha : CustomItemGroup<SmallMocha.ItemGroupViewAccessed>
    {
        public override string UniqueNameID => "small_mocha";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Small Mocha");
        public override Item DisposesTo => GetCastedGDO<Item, SmallMug>();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemValue ItemValue => ItemValue.Large;
        public override Item DirtiesTo => GetCastedGDO<Item, SmallMugDirty>();
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
                    GetCastedGDO<Item, SmallCappuccino>(),
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
            gdo.name = "Small Mocha";

            var view = Prefab.GetComponent<ItemGroupViewAccessed>();
            view.Setup();

            SmallMug.ApplyMugMaterials(Prefab.GetChild("mug"));
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
                        Text = "SMo",
                        Item = GetCastedGDO<Item, SmallCappuccino>()
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
