namespace MiniCafe.Sides
{
    public class Stroop : CustomItem
    {
        public override string UniqueNameID => "stroop";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Stroop");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override string ColourBlindTag => "St";

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Bowl", "Metal");
            Prefab.ApplyMaterialToChildCafe("Fill", "Stroop");
        }
    }
}
