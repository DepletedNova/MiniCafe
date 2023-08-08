using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using UnityEngine;
using static KitchenLib.Utils.GDOUtils;
using static MiniCafe.Helper;

namespace MiniCafe.Bakery
{
    public class CherryFlavour : CustomItem
    {
        public override string UniqueNameID => "Cherry Flavour";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Cherry - Flavour Icon");

        public override void OnRegister(Item gdo)
        {
            gdo.SatisfiedBy = new()
            {
                GetGDO<Item>(333230026), // cookie
                GetGDO<ItemGroup>(-1312823003), // donut
                GetGDO<ItemGroup>(1366309564), // cupcake
                GetGDO<Item>(-1532306603) // cake
            };

            gdo.NeedsIngredients = new()
            {
                GetGDO<Item>(ItemReferences.Cherry)
            };
        }

        public override void SetupPrefab(GameObject prefab)
        {
            var cake = prefab.GetChild("Cake Icon");
            for (int i = 0; i < cake.GetChildCount(); i++)
                cake.GetChild(i).ApplyMaterial("Batter - Cooked", "Batter - Cooked");

            prefab.ApplyMaterialToChildren("Cherries", "Cherry", "Wood - Dark");
        }
    }
}
