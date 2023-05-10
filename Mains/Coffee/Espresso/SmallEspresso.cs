namespace MiniCafe.Mains.Coffee
{
    public class SmallEspresso : CustomItem
    {
        public override string UniqueNameID => "small_espresso";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Small Espresso");
        public override Item DisposesTo => GetCastedGDO<Item, SmallMug>();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override string ColourBlindTag => "SEs";

        public override void OnRegister(Item gdo)
        {
            SmallMug.ApplyMugMaterials(Prefab.GetChild("mug"));
            Prefab.ApplyMaterialToChildCafe("fill", "Coffee - Black");
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");
        }
    }
}
