namespace MiniCafe.Mains
{
    public class PlatedBigCoffee : CustomItemGroup<PlatedBigCoffee.View>
    {
        public override string UniqueNameID => "plated_big_coffee";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Plated Big");
        public override Item DisposesTo => GetCastedGDO<Item, BigMug>();
        public override Item DirtiesTo => GetCastedGDO<Item, BigMugDirty>();
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
                    GetCastedGDO<Item, BigEspresso>(),
                    GetCastedGDO<Item, BigAmericano>(),
                    GetCastedGDO<Item, BigCappuccino>(),
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
            BigMug.ApplyMugMaterials(mug);
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
                    Item = GetCastedGDO<Item, BigEspresso>(),
                    GameObject = gameObject.GetChildFromPath("Mug/Espresso")
                },
                new()
                {
                    Item = GetCastedGDO<Item, BigAmericano>(),
                    GameObject = gameObject.GetChildFromPath("Mug/Americano")
                },
                new()
                {
                    Item = GetCastedGDO<Item, BigCappuccino>(),
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
                    Item = GetCastedGDO<Item, BigEspresso>(),
                    Text = "BEs"
                },
                new()
                {
                    Item = GetCastedGDO<Item, BigAmericano>(),
                    Text = "BAm"
                },
                new()
                {
                    Item = GetCastedGDO<Item, BigCappuccino>(),
                    Text = "BCa"
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
