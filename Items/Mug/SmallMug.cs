namespace MiniCafe.Items
{
    public class SmallMug : CustomItem
    {
        public static int ItemID { get; private set; }

        public override string UniqueNameID => "small_mug";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Small Mug");
        public override bool IsIndisposable => true;
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override Appliance DedicatedProvider => GetCastedGDO<Appliance, MugCabinet>();
        public override string ColourBlindTag => "S";
        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 3.25f,
                Process = GetGDO<Process>(ProcessReferences.FillCoffee),
                Result = GetCastedGDO<Item, SmallEspresso>()
            }
        };

        public override void Convert(GameData gameData, out GameDataObject gdo)
        {
            base.Convert(gameData, out gdo);
            ItemID = gdo.ID;
        }

        public override void OnRegister(Item gdo)
        {
            ApplyMugMaterials(Prefab.GetChild("mug"));

            if (Main.PaperPlatesInstalled)
                gdo.IsIndisposable = false;
        }

        public static void ApplyMugMaterials(GameObject mug)
        {
            mug.ApplyMaterialCafe("Coffee Cup", "Wood 1", "Light Coffee Cup");
        }
    }
}
