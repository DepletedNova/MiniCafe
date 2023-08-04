using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using UnityEngine;
using static KitchenLib.Utils.GDOUtils;
using static MiniCafe.Helper;

namespace MiniCafe.Bakery.Pumpkin
{
    public class PumpkinFlavour : CustomItem
    {
        public override string UniqueNameID => "Pumpkin Flavour";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Pumpkin - Flavour Icon");

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
                GetGDO<Item>(ItemReferences.PumpkinPieces)
            };
        }

        public override void SetupPrefab(GameObject prefab)
        {
            var cake = prefab.GetChild("Cake Icon");
            for (int i = 0; i < cake.GetChildCount(); i++)
                cake.GetChild(i).ApplyMaterial("Batter - Cooked", "Batter - Cooked");

            prefab.ApplyMaterialToChild("Pumpkin/Body", "Pumpkin", "Pumpkin");
            prefab.ApplyMaterialToChild("Pumpkin/Top", "Pumpkin", "Pumpkin - Stem");
        }
    }
}
