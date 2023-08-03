using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using MiniCafe.Appliances;
using UnityEngine;
using static KitchenLib.Utils.GDOUtils;
using static KitchenLib.Utils.MaterialUtils;

namespace MiniCafe.Items
{
    public class BigMug : CustomItem
    {
        public static int ItemID { get; private set; }

        public override string UniqueNameID => "big_mug";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Big Mug");
        public override bool IsIndisposable => true;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override Appliance DedicatedProvider => GetCastedGDO<Appliance, MugCabinet>();
        public override string ColourBlindTag => "B";

        public override void Convert(GameData gameData, out GameDataObject gdo)
        {
            base.Convert(gameData, out gdo);
            ItemID = gdo.ID;
        }

        public override void OnRegister(Item gdo)
        {
            ApplyMugMaterials(Prefab.GetChild("mug"));

            if (Main.PaperPlatesInstalled)
                gdo.IsIndisposable = false;
        }

        public static void ApplyMugMaterials(GameObject mug) => mug.ApplyMaterial("Coffee Cup", "Sack - Blue");
    }
}
