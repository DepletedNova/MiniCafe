using KitchenData;
using KitchenLib.Customs;
using System.Collections.Generic;

namespace MiniCafe.Sides
{
    // Unused
    public class StroopMix : CustomItemGroup
    {
        public override string UniqueNameID => "stroop_mix";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override List<Item.ItemProcess> Processes => new()
        {
        };
        public override List<ItemGroup.ItemSet> Sets => new()
        {
        };

        public override void OnRegister(ItemGroup gdo)
        {
        }
    }
}
