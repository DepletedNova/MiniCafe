namespace MiniCafe.Items
{
    public class SteamedMilk : CustomItem
    {
        public override string UniqueNameID => "steamed_milk";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Steamed Milk");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override Appliance DedicatedProvider => GetGDO<Appliance>(References.GetProvider("Milk"));

        public override void OnRegister(GameDataObject gdo)
        {
            gdo.name = "Steamed Milk";

            var bowl = Prefab.GetChild("bowl");
            bowl.ApplyMaterialToChild("Cylinder.001", "Metal Dark");
            bowl.ApplyMaterialToChild("Cylinder", "Coffee Cup");
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");
        }
    }
}
