namespace MiniCafe.Items
{
    internal class LemonSlicePlated : CustomItem
    {
        public override string UniqueNameID => "lemon_slice_plated";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Lemon Slice Plate");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override bool PreventExplicitSplit => true;
        public override bool AllowSplitMerging => true;
        public override List<Item> SplitDepletedItems => new() { GetCastedGDO<Item, LemonSlice>() };
        public override Item SplitSubItem => GetCastedGDO<Item, LemonSlice>();
        public override int SplitCount => 6;
        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Plate", "Plate", "Plate - Ring");
            Prefab.ApplyMaterialToChildren("Lemon", "Lemon", "Lemon Inner", "White Fruit");

            List<GameObject> lemons = new();
            for (int i = 0; i < Prefab.GetChildCount(); i++)
                if (Prefab.GetChild(i).name.Contains("Lemon"))
                    lemons.Add(Prefab.GetChild(i));

            ReflectionUtils.GetField<ObjectsSplittableView>("Objects").SetValue(Prefab.AddComponent<ObjectsSplittableView>(), lemons);
        }
    }
}
