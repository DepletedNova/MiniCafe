namespace MiniCafe.Items
{
    internal class KettleBoiled : CustomItem
    {
        public override string UniqueNameID => "kettle_boiled";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Kettle Tea Raw");
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
        public override Item DisposesTo => GetCastedGDO<Item, Kettle>();

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Result = GetCastedGDO<Item, Kettle>(),
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Duration = 8,
                IsBad = true,
            }
        };
    }
}
