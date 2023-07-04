namespace MiniCafe.Appliances
{
    public class TeaspoonDispenser : CustomAppliance
    {
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Teaspoon Dispenser");
        public override string UniqueNameID => "teaspoon_dispenser";
        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Teaspoons", "Provides teaspoons", new()
            {
                new()
                {
                    Title = "Dispenser",
                    Description = "Automatically adds teaspoons to anything in front of it"
                }
            }, new()))
        };
        public override bool IsPurchasable => true;
        public override PriceTier PriceTier => PriceTier.Cheap;
        public override RarityTier RarityTier => RarityTier.Common;
        public override ShoppingTags ShoppingTags => ShoppingTags.Cooking;
        public override bool SellOnlyAsDuplicate => true;

        public override List<IApplianceProperty> Properties => new()
        {
            GetCItemProvider(GetCustomGameDataObject<Teaspoon>().ID, 0, 0, false, false, false, false, false, true, false),
            new CAutomatedInteractor()
            {
                Type = InteractionType.Grab,
                DoNotReceive = true,
                TransferOnly = false,
                IsHeld = false,
                RequiredFlags = TransferFlags.RequireMerge
            },
            new CItemHolder(),
            new CItemHolderFilter()
            {
                NoDirectInsertion = true,
                AllowAny = false,
                Category = ItemCategory.Generic,
            },
            new CInvertedPlacement()
        };

        public override void OnRegister(Appliance gdo)
        {
            Prefab.AddComponent<HoldPointContainer>().HoldPoint = Prefab.transform.Find("HoldPoint");

            var counter = Prefab.GetChild("Counter");
            counter.ApplyMaterialCafe("Wood - Default", "Wood 4 - Painted", "Wood 4 - Painted");
            counter.ApplyMaterialToChildCafe("Handle", "Knob");
            counter.ApplyMaterialToChildCafe("Countertop", "Wood - Default");

            Prefab.ApplyMaterialToChildCafe("Pusher", "Plastic - Blue");

            Prefab.ApplyMaterialToChildCafe("Dispenser", "Plastic - Red", "Plastic - Black", "Hob Black");
        }
    }
}
