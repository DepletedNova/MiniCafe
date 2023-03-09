global using KitchenLib;
global using KitchenLib.Customs;
global using KitchenLib.Utils;
global using KitchenLib.References;
global using KitchenLib.Event;
global using KitchenLib.Colorblind;
global using static KitchenLib.Utils.GDOUtils;
global using static KitchenLib.Utils.KitchenPropertiesUtils;

global using KitchenMods;
global using KitchenData;
global using Kitchen;

global using IngredientLib;
global using IngredientLib.Util;
global using IngredientLib.Ingredient.Items;
global using static IngredientLib.Util.Helper;
global using static IngredientLib.Util.VisualEffectHelper;

global using System.Linq;
global using System.Reflection;
global using System.Collections.Generic;

global using UnityEngine;

global using Unity.Entities;
global using Unity.Collections;

global using MessagePack;

global using TMPro;

global using MiniCafe.Appliances;
global using MiniCafe.Misc;
global using MiniCafe.Items;
global using MiniCafe.Dishes;
global using static MiniCafe.MaterialHelper;

namespace MiniCafe
{
    public class Main : BaseMod
    {
        public const string GUID = "nova.minicafe";
        public const string VERSION = "1.0.0";

        public Main() : base(GUID, "Mini Cafe", "Depleted Supernova#1957", VERSION, ">=1.0.0", Assembly.GetExecutingAssembly()) { }

        internal static AssetBundle Bundle;

        internal void AddGameData()
        {
            // Processes
            AddGameDataObject<SteamProcess>();

            // Appliances
            AddGameDataObject<MugCabinet>();
            AddGameDataObject<WhippedCreamProvider>();
            AddGameDataObject<BaristaMachine>();

            // Items
            AddGameDataObject<BigMug>();
            AddGameDataObject<BigMugDirty>();
            AddGameDataObject<BigEspresso>();
            AddGameDataObject<BigCappuccino>();
            AddGameDataObject<BigAmericano>();
            AddGameDataObject<BigMocha>();

            AddGameDataObject<SmallMug>();
            AddGameDataObject<SmallMugDirty>();
            AddGameDataObject<SmallEspresso>();
            AddGameDataObject<SmallCappuccino>();
            AddGameDataObject<SmallAmericano>();
            AddGameDataObject<SmallMocha>();

            AddGameDataObject<SteamedMilk>();
            AddGameDataObject<CannedWhippedCream>();

            // Dishes
            AddGameDataObject<EspressoDish>();
            cappuccino = AddGameDataObject<CappuccinoDish>().GameDataObject as Unlock;
            americano = AddGameDataObject<AmericanoDish>().GameDataObject as Unlock;
            mocha = AddGameDataObject<MochaDish>().GameDataObject as Unlock;

        }

        internal void AddMaterials()
        {
            AddMaterial(CreateTransparent("Glass", 0xF6FEFF, 0.6f));

            AddMaterial(CreateFlat("Light Coffee Cup", 0xDAC7AB));

            // Coffee
            AddMaterial(CreateFlat("Coffee Blend", 0xAF8967));
            AddMaterial(CreateFlat("Coffee Foam", 0xE0C2A8));

            AddMaterial(CreateFlat("Americano", 0x895238));
        }

        private void UpdateRewards()
        {
            GetCastedGDO<Item, SmallEspresso>().Reward = 4;
            GetCastedGDO<Item, SmallAmericano>().Reward = 4;
        }

        private void UpdateCoffeeMachine()
        {
            // Coffee Machine
            var coffeeMachine = GetExistingGDO(ApplianceReferences.CoffeeMachine) as Appliance;
            //coffeeMachine.Upgrades.Add(GetCastedGDO<Appliance, BaristaMachine>());
            coffeeMachine.Processes = new()
            {
                new()
                {
                    IsAutomatic = true,
                    Process = GetExistingGDO(ProcessReferences.FillCoffee) as Process,
                    Speed = 1f,
                    Validity = ProcessValidity.Generic
                },
                new()
                {
                    IsAutomatic = true,
                    Process = GetCastedGDO<Process, SteamProcess>(),
                    Speed = 1f,
                    Validity = ProcessValidity.Generic
                }
            };
            coffeeMachine.RequiresProcessForShop = new()
            {
                GetExistingGDO(ProcessReferences.FillCoffee) as Process,
                GetCastedGDO<Process, SteamProcess>()
            };
            coffeeMachine.ShoppingTags = ShoppingTags.Cooking | ShoppingTags.Basic;
        }

        private void UpdateMilk()
        {
            GetCastedGDO<Item, MilkIngredient>().DerivedProcesses.Add(new()
            {
                Duration = 1.3f,
                Process = GetCastedGDO<Process, SteamProcess>(),
                Result = GetCastedGDO<Item, SteamedMilk>()
            });;
        }

        private void AddIcons()
        {
            Bundle.LoadAllAssets<Texture2D>();
            Bundle.LoadAllAssets<Sprite>();

            var icons = Bundle.LoadAsset<TMP_SpriteAsset>("Icon Asset");
            TMP_Settings.defaultSpriteAsset.fallbackSpriteAssets.Add(icons);
            icons.material = Object.Instantiate(TMP_Settings.defaultSpriteAsset.material);
            icons.material.mainTexture = Bundle.LoadAsset<Texture2D>("Icon Texture");
        }

        protected override void OnPostActivate(Mod mod)
        {
            Bundle = mod.GetPacks<AssetBundleModPack>().SelectMany(e => e.AssetBundles).ToList()[0];

            AddGameData();

            AddMaterials();

            AddIcons();

            Events.BuildGameDataEvent += (s, args) =>
            {
                UpdateRewards();
                UpdateCoffeeMachine();
                UpdateMilk();

                args.gamedata.ProcessesView.Initialise(args.gamedata);
            };
        }

        static Unlock cappuccino;
        static Unlock americano;
        static Unlock mocha;
        protected override void OnUpdate()
        {
            if (mocha == null)
                return;
            cappuccino.BlockedBy.Clear();
            americano.BlockedBy.Clear();
            mocha.BlockedBy.Clear();
        }
    }
}
