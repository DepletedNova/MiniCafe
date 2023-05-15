namespace MiniCafe.Extras
{
    public class Scone : CustomItem
    {
        public override string UniqueNameID => "scone";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Scone");
        public override GameObject SidePrefab => Main.Bundle.LoadAsset<GameObject>("Scone");
        public override bool IsMergeableSide => true;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Scone", "Bread - Inside Cooked", "Chocolate");
        }
    }
}
