namespace MiniCafe.Appliances
{
    public class SteepingStand : CustomAppliance
    {
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Steeping Stand");
        public override string UniqueNameID => "steeping_stand";
        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Steeping Stand", "Dedicated steeping appliance!", new(), new()))
        };
        public override List<Process> RequiresProcessForShop => new()
        {
            GetCastedGDO<Process, SteepProcess>()
        };
        public override bool IsPurchasable => true;
        public override PriceTier PriceTier => PriceTier.Cheap;
        public override RarityTier RarityTier => RarityTier.Common;
        public override ShoppingTags ShoppingTags => ShoppingTags.Cooking;

        public override List<IApplianceProperty> Properties => new()
        {
            new CItemHolder()
        };

        public override List<Appliance.ApplianceProcesses> Processes => new()
        {
            new()
            {
                Process = GetCastedGDO<Process, SteepProcess>(),
                IsAutomatic = true,
                Speed = 1.5f,
                Validity = ProcessValidity.Generic
            }
        };

        public override void OnRegister(Appliance gdo)
        {
            // View
            Prefab.TryAddComponent<HoldPointContainer>().HoldPoint = Prefab.transform.Find("Hold");

            // Materials
            GameObject parent = Prefab.GetChild("Block/Counter2");
            var paintedWood = GetMaterialArray("Wood 4 - Painted");
            var defaultWood = GetMaterialArray("Wood - Default");
            parent.ApplyMaterialToChildCafe("Counter", paintedWood);
            parent.ApplyMaterialToChildCafe("Counter Doors", paintedWood);
            parent.ApplyMaterialToChildCafe("Counter Surface", defaultWood);
            parent.ApplyMaterialToChildCafe("Counter Top", defaultWood);
            parent.ApplyMaterialToChildCafe("Handles", "Knob");

            Prefab.ApplyMaterialToChildCafe("Plate", "Metal");
        }
    }
}
