namespace MiniCafe.Items
{
    public class SteamedMilk : CustomItem
    {
        public override string UniqueNameID => "steamed_milk";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Steamed Milk");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override Appliance DedicatedProvider => GetGDO<Appliance>(References.GetProvider("Milk"));

        public override void OnRegister(Item gdo)
        {
            var bowl = Prefab.GetChild("bowl");
            bowl.ApplyMaterialToChildCafe("Cylinder.001", "Metal Dark");
            bowl.ApplyMaterialToChildCafe("Cylinder", "Coffee Cup");
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");
        }
    }
}
