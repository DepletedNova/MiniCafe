namespace MiniCafe.Mains
{
    public class PlatedSmallCoffee : CustomItemGroup<PlatedSmallCoffee.View>
    {
        public override string UniqueNameID => "plated_small_coffee";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Plated Small");
        public override Item DisposesTo => GetCastedGDO<Item, SmallMug>();
        public override Item DirtiesTo => GetCastedGDO<Item, SmallMugDirty>();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemValue ItemValue => ItemValue.MediumLarge;
        public override List<ItemGroup.ItemSet> Sets => new()
        {
            // Main
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, SmallEspresso>(),
                    GetCastedGDO<Item, SmallAmericano>(),
                    GetCastedGDO<Item, SmallCappuccino>(),
                },
                Min = 1,
                Max = 1,
                IsMandatory = true,
                RequiresUnlock = true
            },
            // Extras
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, Teaspoon>(),
                    GetCastedGDO<Item, Croissant>(),
                },
                Min = 1,
                Max = 1,
                IsMandatory = true,
                RequiresUnlock = true
            }
        };

        public override void OnRegister(GameDataObject gdo)
        {
            Prefab.GetComponent<View>().Setup(gdo);

            var mug = Prefab.GetChild("Mug");
            SmallMug.ApplyMugMaterials(mug);
            mug.ApplyMaterialToChild("Espresso", "Coffee - Black");
            mug.ApplyMaterialToChild("Americano", "Americano", "Coffee - Black");
            mug.ApplyMaterialToChild("Cappuccino", "Coffee Blend", "Coffee Foam");

            var sides = Prefab.GetChild("Sides");
            sides.ApplyMaterialToChild("Spoon", "Metal");
            sides.ApplyMaterialToChild("Croissant", "Croissant");

            Prefab.ApplyMaterialToChild("Plate", "Plate", "Plate - Ring");
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");
        }

        public class View : AccessedItemGroupView
        {
            protected override List<ComponentGroup> groups => new()
            {
                // Mains
                new()
                {
                    Item = GetCastedGDO<Item, SmallEspresso>(),
                    GameObject = gameObject.GetChildFromPath("Mug/Espresso")
                },
                new()
                {
                    Item = GetCastedGDO<Item, SmallAmericano>(),
                    GameObject = gameObject.GetChildFromPath("Mug/Americano")
                },
                new()
                {
                    Item = GetCastedGDO<Item, SmallCappuccino>(),
                    GameObject = gameObject.GetChildFromPath("Mug/Cappuccino")
                },
                // Extras
                new()
                {
                    Item = GetCastedGDO<Item, Teaspoon>(),
                    GameObject = gameObject.GetChildFromPath("Sides/Spoon")
                },
                new()
                {
                    Item = GetCastedGDO<Item, Croissant>(),
                    GameObject = gameObject.GetChildFromPath("Sides/Croissant")
                },
            };

            protected override List<ColourBlindLabel> labels => new()
            {
                // Mains
                new()
                {
                    Item = GetCastedGDO<Item, SmallEspresso>(),
                    Text = "SEs"
                },
                new()
                {
                    Item = GetCastedGDO<Item, SmallAmericano>(),
                    Text = "SAm"
                },
                new()
                {
                    Item = GetCastedGDO<Item, SmallCappuccino>(),
                    Text = "SCa"
                },
                // Extras
                new()
                {
                    Item = GetCastedGDO<Item, Teaspoon>(),
                    Text = "Sp"
                },
                new()
                {
                    Item = GetCastedGDO<Item, Croissant>(),
                    Text = "Cr"
                },
            };
        }
    }
}
