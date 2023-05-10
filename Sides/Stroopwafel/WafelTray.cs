namespace MiniCafe.Sides
{
    internal class WafelTray : CustomItem
    {
        public override string UniqueNameID => "wafel_tray";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Cooked Wafel Tray");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override int SplitCount => 5;
        public override Item SplitSubItem => GetCastedGDO<Item, Wafel>();
        public override List<Item> SplitDepletedItems => new() { GetCastedGDO<Item, Wafel>() };

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Tray", "Metal");

            var list = new List<GameObject>();
            for (int i = 0; i < Prefab.GetChildCount(); i++)
            {
                var child = Prefab.GetChild(i);
                if (!child.name.Contains("Wafel"))
                    continue;
                child.ApplyMaterialCafe("Cooked Pastry", "Bread - Cooked");
                if (list.Count < gdo.SplitCount)
                    list.Add(child);
                
            }

            ReflectionUtils.GetField<ObjectsSplittableView>("Objects").SetValue(Prefab.AddComponent<ObjectsSplittableView>(), list);
        }
    }
}
