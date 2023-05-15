namespace MiniCafe.Extras
{
    internal class ChocolateGlazeIngredient : CustomItem
    {
        public override string UniqueNameID => "chocolate_glaze";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Chocolate Glaze");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
    }
}
