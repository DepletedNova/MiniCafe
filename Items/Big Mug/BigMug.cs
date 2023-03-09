namespace MiniCafe.Items
{
    public class BigMug : CustomItem
    {
        public static int ItemID { get; private set; }

        public override string UniqueNameID => "big_mug";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Big Mug");
        public override bool IsIndisposable => true;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override Appliance DedicatedProvider => GetCastedGDO<Appliance, MugCabinet>();
        public override string ColourBlindTag => "B";
        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 4.5f,
                Process = GetGDO<Process>(ProcessReferences.FillCoffee),
                Result = GetCastedGDO<Item, BigEspresso>()
            }
        };
        public override void Convert(GameData gameData, out GameDataObject gdo)
        {
            base.Convert(gameData, out gdo);
            ItemID = gdo.ID;
        }

        public override void OnRegister(GameDataObject gdo)
        {
            gdo.name = "Big Mug";

            // Materials
            ApplyMugMaterials(Prefab.GetChild("mug"));
        }

        public static void ApplyMugMaterials(GameObject mug) => mug.ApplyMaterial("Light Coffee Cup", "Coffee Cup");
    }
}
