using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace MiniCafe.Desserts
{
    internal class CannoliShellPot : CustomItem
    {
        public override string UniqueNameID => "Cannoli Shell Pot";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override Item DisposesTo => GetGDO<Item>(ItemReferences.Pot);

        public override List<Item> SplitDepletedItems => new() { GetGDO<Item>(1719428613) };
        public override int SplitCount => 4;
        public override Item SplitSubItem => GetCastedGDO<Item, CannoliShell>();

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Result = GetCastedGDO<Item, BurnedCannoliPot>(),
                Duration = 4f,
                Process = GetGDO<Process>(ProcessReferences.Cook),
                IsBad = true,
            }
        };

        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Fried Cannoli Shells");
        public override void SetupPrefab(GameObject prefab)
        {
            prefab.ApplyMaterialToChild("Pot/Base", "Metal");
            prefab.ApplyMaterialToChild("Pot/Handle", "Metal Dark");
            prefab.ApplyMaterialToChild("Oil", "Frying Oil");

            prefab.GetChild("Shells").ApplyMaterialToChildren("roll", "Cooked Pastry");

            var objectsField = ReflectionUtils.GetField<ObjectsSplittableView>("Objects");
            var view = prefab.TryAddComponent<ObjectsSplittableView>();

            List<GameObject> objects = new();
            for (int i = 0; i < prefab.GetChild("Shells").GetChildCount(); i++)
                objects.Add(prefab.GetChild("Shells").GetChild(i));
            objectsField.SetValue(view, objects);
        }
    }
}
