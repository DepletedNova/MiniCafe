namespace MiniCafe.Appliances
{
    public class MugRack : CustomAppliance
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
            new CFlexibleStorage()
            {
                Maximum = 4,
                Item1 = GetCustomGameDataObject<BigMugDirty>().ID,
                Item2 = GetCustomGameDataObject<SmallMugDirty>().ID,
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

            List<GameObject> items1 = new();
            List<GameObject> items2 = new();

            var big = rack.GetChild("Big");
            for (int i = 0; i < big.GetChildCount(); i++)
            {
                var mug = big.GetChild(i);
                mug.ApplyMaterialToChildCafe("dirt", "Plate - Dirty Food");
                BigMug.ApplyMugMaterials(mug);
                items1.Add(mug);
            }

            var small = rack.GetChild("Small");
            for (int i = 0; i < small.GetChildCount(); i++)
            {
                var mug = small.GetChild(i);
                mug.ApplyMaterialToChildCafe("dirt", "Plate - Dirty Food");
                SmallMug.ApplyMugMaterials(mug);
                items2.Add(mug);
            }

            var flexibleView = Prefab.AddComponent<FlexibleContainerView>();
            flexibleView.ItemList1 = items1;
            flexibleView.ItemList2 = items2;
        }
    }
}
