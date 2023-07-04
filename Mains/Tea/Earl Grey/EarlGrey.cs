namespace MiniCafe.Mains.Tea
{
    public class EarlGrey : CustomItem
    {
        public override string UniqueNameID => "earl_grey";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Earl Grey");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override Appliance DedicatedProvider => GetCastedGDO<Appliance, EarlGreyProvider>();

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Result = GetCastedGDO<Item, EarlGreySteeped>(),
                Process = GetGDO<Process>(SteepProcess),
                RequiresWrapper = true
            }
        };

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("teabag", "Cloth - Fancy", "Earl Grey Tea");
        }
    }
}
