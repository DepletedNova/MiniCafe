namespace MiniCafe.Mains
{
    public class BigEspresso : CustomItem
    {
        public override string UniqueNameID => "big_espresso";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Big Espresso");
        public override Item DisposesTo => GetCastedGDO<Item, BigMug>();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override string ColourBlindTag => "BEs";

        public override void OnRegister(Item gdo)
        {
            BigMug.ApplyMugMaterials(Prefab.GetChild("mug"));
            Prefab.ApplyMaterialToChildCafe("fill", "Coffee - Black");
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");
        }
    }
}
