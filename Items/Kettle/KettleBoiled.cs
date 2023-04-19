namespace MiniCafe.Items
{
    internal class KettleBoiled : CustomItem
    {
        public static int KettleID { get; private set; }
        public override void AttachDependentProperties(GameData gameData, GameDataObject gdo)
        {
            base.AttachDependentProperties(gameData, gdo);
            KettleID = gdo.ID;
        }

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
