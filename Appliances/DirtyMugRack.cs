using ApplianceLib.Api;

namespace MiniCafe.Appliances
{
    public class DirtyMugRack : CustomAppliance
    {
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Dirty Mug Rack");
        public override string UniqueNameID => "mug_rack";
        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Dirty Mug Rack", "Put them here and wait for someone else to do it", new() 
                { new() { Title = "Dirty Storage", Description = "Stores up to 4 dirty mugs" } }, new()))
        };
        public override bool IsPurchasable => true;
        public override List<Process> RequiresProcessForShop => new()
        {
            GetCastedGDO<Process, CuplessFillCupProcess>()
        };
        public override PriceTier PriceTier => PriceTier.Cheap;
        public override RarityTier RarityTier => RarityTier.Common;
        public override ShoppingTags ShoppingTags => ShoppingTags.Plumbing;
        public override List<IApplianceProperty> Properties => new()
        {
            new CFlexibleContainer()
            {
                Maximum = 4,
            },
            new CRestrictedReceiver()
            {
                ApplianceKey = Main.DirtyMugKey
            }
        };

        public override void OnRegister(Appliance gdo)
        {
            var parent = Prefab.GetChild("Block/Counter2");
            var paintedWood = GetMaterialArray("Wood 4 - Painted");
            var defaultWood = GetMaterialArray("Wood - Default");
            parent.ApplyMaterialToChildCafe("Counter", paintedWood);
            parent.ApplyMaterialToChildCafe("Counter Doors", paintedWood);
            parent.ApplyMaterialToChildCafe("Counter Surface", defaultWood);
            parent.ApplyMaterialToChildCafe("Counter Top", defaultWood);
            parent.ApplyMaterialToChildCafe("Handles", "Knob");

            var rack = Prefab.GetChild("Rack");
            rack.ApplyMaterialToChildCafe("Rack", "Plastic");

            List<Transform> items = new();

            for (int i = 0; i < rack.GetChildCount(); i++)
            {
                var mug = rack.transform.GetChild(i);
                if (!mug.name.ToLower().Contains("mug"))
                    continue;
                items.Add(mug);
            }

            var flexibleView = Prefab.AddComponent<FlexibleContainerView>();
            flexibleView.Transforms = items;
        }
    }
}
