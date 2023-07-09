namespace MiniCafe.Desserts
{
    public class CannoliTray : CustomItemGroup
    {
        public override string UniqueNameID => "cannoli_tray";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Cannoli Tray");
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override int SplitCount => 5;
        public override Item SplitSubItem => GetCastedGDO<Item, Cannoli>();
        public override List<Item> SplitDepletedItems => new() { GetCastedGDO<Item, Cannoli>() };

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, CookedCannoliTray>(),
                    GetCastedGDO<Item, CannoliFilling>()
                },
                Min = 2,
                Max = 2
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Tray", "Metal");

            List<GameObject> items = new();
            for (int i = 0; i < Prefab.GetChildCount(); i++)
            {
                var child = Prefab.GetChild(i);
                if (!child.name.Contains("Cannoli"))
                    continue;

                child.ApplyMaterialCafe("Cooked Pastry");
                child.ApplyMaterialToChildCafe("Filling", "Coffee Cup");
                child.ApplyMaterialToChildCafe("Chocolate", "Chocolate");

                if (items.Count < gdo.SplitCount)
                    items.Add(child);
            }

            items.Reverse();
            ReflectionUtils.GetField<ObjectsSplittableView>("Objects").SetValue(Prefab.AddComponent<ObjectsSplittableView>(), items);
        }
    }
}
