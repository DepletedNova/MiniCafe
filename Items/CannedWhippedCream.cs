namespace MiniCafe.Items
{
    public class CannedWhippedCream : CustomItem
    {
        public override string UniqueNameID => "canned_whipped_cream";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Whipped Cream");
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override bool IsIndisposable => true;
        public override Appliance DedicatedProvider => GetCastedGDO<Appliance, WhippedCreamProvider>();

        public override Item SplitSubItem => GetCastedGDO<Item, WhippedCream>();
        public override bool PreventExplicitSplit => true;
        public override bool AllowSplitMerging => true;
        public override int SplitCount => 999;

        public override void OnRegister(GameDataObject gdo)
        {
            Prefab.ApplyMaterialToChild("Can", "Metal", "Plastic - White", "Plastic - Red");
        }
    }
}
