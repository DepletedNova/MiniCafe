namespace MiniCafe.Items
{
    public class Croissant : CustomItem
    {
        public override string UniqueNameID => "croissant";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Croissant");
        public override GameObject SidePrefab => Main.Bundle.LoadAsset<GameObject>("Croissant Side");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override string ColourBlindTag => "Cr";

        public override void OnRegister(GameDataObject gdo)
        {
            Prefab.ApplyMaterialToChild("roll", "Croissant");
            SidePrefab.ApplyMaterialToChild("roll", "Croissant");
        }
    }
}
