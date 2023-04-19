namespace MiniCafe.Appliances
{
    public class EarlGreyProvider : CustomAppliance
    {
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Earl Grey Provider");
        public override string UniqueNameID => "earl_grey_provider";
        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Earl Grey Blend", "Provides earl grey", new(), new()))
        };
        public override bool IsPurchasable => true;
        public override PriceTier PriceTier => PriceTier.Cheap;
        public override RarityTier RarityTier => RarityTier.Common;
        public override ShoppingTags ShoppingTags => ShoppingTags.Cooking;
        public override bool SellOnlyAsDuplicate => true;

        public override List<IApplianceProperty> Properties => new()
        {
            GetUnlimitedCItemProvider(GetCustomGameDataObject<EarlGrey>().ID),
        };

        public override void OnRegister(Appliance gdo)
        {
            // Materials
            GameObject parent = Prefab.GetChild("Block/Counter2");
            var paintedWood = GetMaterialArray("Wood 4 - Painted");
            var defaultWood = GetMaterialArray("Wood - Default");
            parent.ApplyMaterialToChildCafe("Counter", paintedWood);
            parent.ApplyMaterialToChildCafe("Counter Doors", paintedWood);
            parent.ApplyMaterialToChildCafe("Counter Surface", defaultWood);
            parent.ApplyMaterialToChildCafe("Counter Top", defaultWood);
            parent.ApplyMaterialToChildCafe("Handles", "Knob");

            var container = Prefab.GetChild("Container");
            container.ApplyMaterial("Wood 1");
            container.ApplyMaterialToChildCafe("Herbs", "Earl Grey");
            container.ApplyMaterialToChildren("extra", "Earl Grey Extra");

            Prefab.ApplyMaterialToChildCafe("Napkin", "Paper", "Earl Grey Tea");
        }
    }
}
