using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using UnityEngine;
using static KitchenLib.Utils.MaterialUtils;

namespace MiniCafe.Cosmetics
{
    internal class VisorHat : CustomPlayerCosmetic
    {
        public override string UniqueNameID => "visor";
        public override CosmeticType CosmeticType => CosmeticType.Hat;
        public override GameObject Visual => Main.Bundle.LoadAsset<GameObject>("Visor");
        public override bool BlockHats => true;

        public override void OnRegister(PlayerCosmetic gdo)
        {
            var visor = Visual.GetChild("visor");

            Visual.TryAddComponent<PlayerOutfitComponent>().Renderers.Add(visor.GetComponent<SkinnedMeshRenderer>());
            visor.ApplyMaterial<SkinnedMeshRenderer>(GetMaterialArray("Clothing Black"));
        }
    }
}
