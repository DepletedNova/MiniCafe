using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace MiniCafe.Desserts
{
    internal class BurnedCannoliPot : CustomItem
    {
        public override string UniqueNameID => "Burned Cannoli Pot";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override Item DisposesTo => GetGDO<Item>(ItemReferences.Pot);

        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Cannoli Shells");
    }
}
