namespace MiniCafe.Extras
{
    internal class GlazeIngredient : CustomItem
    {
        public override string UniqueNameID => "glaze";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Glaze");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
    }
}
