namespace MiniCafe.Items
{
    internal class Kettle : CustomItem
    {
        public static int KettleID { get; private set; }
        public override void AttachDependentProperties(GameData gameData, GameDataObject gdo)
        {
            base.AttachDependentProperties(gameData, gdo);
            KettleID = gdo.ID;
        }

        public override string UniqueNameID => "kettle";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Kettle");
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
        public override Appliance DedicatedProvider => GetCastedGDO<Appliance, KettleStand>();
        public override bool IsIndisposable => true;

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChild("pot", "Plastic", "Metal Dark", "Metal", "Hob Black");
            Prefab.ApplyMaterialToChild("lid", "Plastic", "Metal");
            Prefab.ApplyMaterialToChild("water", "Water");
        }
    }
}
