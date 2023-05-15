namespace MiniCafe.Extras
{
    public class Teaspoon : CustomItem
    {
        public override string UniqueNameID => "teaspoon";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Teaspoon");
        public override Appliance DedicatedProvider => GetCastedGDO<Appliance, TeaspoonDispenser>();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("spoon", "Metal");
        }
    }
}
