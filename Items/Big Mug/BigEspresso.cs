namespace MiniCafe.Items
{
    public class BigEspresso : CustomItem
    {
        public override string UniqueNameID => "big_espresso";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Big Espresso");
        public override Item DisposesTo => GetCastedGDO<Item, BigMug>();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemValue ItemValue => ItemValue.Small;
        public override Item DirtiesTo => GetCastedGDO<Item, BigMugDirty>();
        public override string ColourBlindTag => "BEs";

        public override void OnRegister(GameDataObject gdo)
        {
            gdo.name = "Big Espresso";

            BigMug.ApplyMugMaterials(Prefab.GetChild("mug"));
            Prefab.ApplyMaterialToChild("fill", "Coffee - Black");
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");
        }
    }
}
