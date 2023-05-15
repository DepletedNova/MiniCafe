namespace MiniCafe.Extras
{
    internal class ChocolateGlaze : CustomItem
    {
        public override string UniqueNameID => "cooked_chocolate_glaze";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Chocolate Glaze");
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override int SplitCount => 15;
        public override Item SplitSubItem => GetCastedGDO<Item, ChocolateGlazeIngredient>();
        public override List<Item> SplitDepletedItems => new() { GetGDO<Item>(ItemReferences.Pot) };
        public override Item DisposesTo => GetGDO<Item>(ItemReferences.Pot);
        public override bool AllowSplitMerging => true;
        public override bool PreventExplicitSplit => true;
        public override string ColourBlindTag => "Ch";

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Pot", "Metal");
            Prefab.ApplyMaterialToChildCafe("Handles", "Metal Dark");
            Prefab.ApplyMaterialToChildCafe("Glaze", "Chocolate");

            Prefab.AddPositionSplittableView(new() { Prefab.GetChild("Glaze") }, Vector3.up * 0.05f, Vector3.up * 0.275f);
        }
    }
}
