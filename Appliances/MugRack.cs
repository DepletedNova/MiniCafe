using MiniCafe.Items;

namespace MiniCafe.Appliances
{
    public class MugRack : CustomAppliance
    {
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Mug Rack");
        public override string UniqueNameID => "mug_rack";
        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Mug Rack", "Put them here and wait for someone else to do it", new() 
                { new() { Title = "Dirty Storage", Description = "Stores up to 4 dirty mugs" } }, new()))
        };
        public override bool IsPurchasable => true;
        public override List<Process> RequiresProcessForShop => new()
        {
            GetCastedGDO<Process, SteamProcess>()
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

        public override void OnRegister(GameDataObject gdo)
        {
            gdo.name = "Dirty Mug Rack";

            var parent = Prefab.GetChildFromPath("Block/Counter2");
            var paintedWood = GetMaterialArray("Wood 4 - Painted");
            var defaultWood = GetMaterialArray("Wood - Default");
            parent.ApplyMaterialToChild("Counter", paintedWood);
            parent.ApplyMaterialToChild("Counter Doors", paintedWood);
            parent.ApplyMaterialToChild("Counter Surface", defaultWood);
            parent.ApplyMaterialToChild("Counter Top", defaultWood);
            parent.ApplyMaterialToChild("Handles", "Knob");

            var rack = Prefab.GetChild("Rack");
            rack.ApplyMaterialToChild("Rack", "Plastic");

            List<GameObject> items1 = new();
            List<GameObject> items2 = new();

            var big = rack.GetChild("Big");
            for (int i = 0; i < big.GetChildCount(); i++)
            {
                var mug = big.GetChild(i);
                mug.ApplyMaterialToChild("dirt", "Plate - Dirty Food");
                BigMug.ApplyMugMaterials(mug);
                items1.Add(mug);
            }

            var small = rack.GetChild("Small");
            for (int i = 0; i < small.GetChildCount(); i++)
            {
                var mug = small.GetChild(i);
                mug.ApplyMaterialToChild("dirt", "Plate - Dirty Food");
                SmallMug.ApplyMugMaterials(mug);
                items2.Add(mug);
            }

            var flexibleView = Prefab.AddComponent<FlexibleContainerView>();
            flexibleView.ItemList1 = items1;
            flexibleView.ItemList2 = items2;
        }
    }
}
