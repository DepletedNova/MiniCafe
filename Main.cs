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
global using MiniCafe.Items;
global using MiniCafe.Processes;
global using MiniCafe.Desserts;
global using MiniCafe.Mains;
global using MiniCafe.Extras;
global using MiniCafe.Components;
global using MiniCafe.Views;
global using static MiniCafe.MaterialHelper;

namespace MiniCafe
{
    public class Main : BaseMod
    {
        public const string GUID = "nova.minicafe";
        public const string VERSION = "1.3.0";

        public Main() : base(GUID, "Mini Cafe", "Depleted Supernova#1957", VERSION, ">=1.0.0", Assembly.GetExecutingAssembly()) { }

        internal static AssetBundle Bundle;

        internal void AddGameData()
        {
            // Processes
            AddGameDataObject<SteamProcess>();
            AddGameDataObject<CuplessFillCupProcess>();

            // Generic Appliances
            AddGameDataObject<MugCabinet>();
            AddGameDataObject<MugRack>();
            AddGameDataObject<SmallMugHolder>();
            AddGameDataObject<BigMugHolder>();

            AddGameDataObject<CuplessCoffeeMachine>();

            AddGameDataObject<BaristaMachine>();
            AddGameDataObject<SteamerMachine>();

            // Big Mug
            AddGameDataObject<BigMug>();
            AddGameDataObject<BigMugDirty>();
            AddGameDataObject<BigEspresso>();
            AddGameDataObject<BigCappuccino>();
            AddGameDataObject<BigAmericano>();
            AddGameDataObject<BigIced>();

            AddGameDataObject<BigMocha>();

            AddGameDataObject<PlatedBigCoffee>();

            // Small Mug
            AddGameDataObject<SmallMug>();
            AddGameDataObject<SmallMugDirty>();
            AddGameDataObject<SmallEspresso>();
            AddGameDataObject<SmallCappuccino>();
            AddGameDataObject<SmallAmericano>();
            AddGameDataObject<SmallIced>();

            AddGameDataObject<SmallMocha>();

            AddGameDataObject<PlatedSmallCoffee>();

            // Extras
            AddGameDataObject<UnrolledCroissant>();
            AddGameDataObject<UncookedCroissant>();
            AddGameDataObject<Croissant>();

            AddGameDataObject<UnmixedSconeDough>();
            AddGameDataObject<UncookedScones>();
            AddGameDataObject<SconePlatter>();
            AddGameDataObject<Scone>();

            AddGameDataObject<TeaspoonDispenser>();
            AddGameDataObject<Teaspoon>();

            AddGameDataObject<WhippedCreamProvider>();
            AddGameDataObject<SteamedMilk>();
            AddGameDataObject<CannedWhippedCream>();

            // Main Dishes
            AddGameDataObject<EspressoDish>();
            AddGameDataObject<CappuccinoDish>();
            AddGameDataObject<AmericanoDish>();
            AddGameDataObject<IcedDish>();

            // Extra Dishes
            AddGameDataObject<CroissantDish>();
            AddGameDataObject<SconeDish>();

            // Dessert Dishes
            AddGameDataObject<MochaDish>();
        }

        internal void AddMaterials()
        {
            AddMaterial(CreateTransparent("Glass", 0xF6FEFF, 0.6f));

            AddMaterial(CreateFlat("Light Coffee Cup", 0xDAC7AB));

            // Coffee
            AddMaterial(CreateFlat("Coffee Blend", 0xAF8967));
            AddMaterial(CreateFlat("Coffee Foam", 0xE0C2A8));

            AddMaterial(CreateFlat("Americano", 0x895238));

            // Sides
            AddMaterial(CreateFlat("Croissant", 0xDA9134));
        }

        private void UpdateRewards()
        {

        }

        private void UpdateCoffeeMachine()
        {
            // Coffee Machine
            var coffeeMachine = GetExistingGDO(ApplianceReferences.CoffeeMachine) as Appliance;
            coffeeMachine.Upgrades.Add(GetCastedGDO<Appliance, BaristaMachine>());
            coffeeMachine.Upgrades.Add(GetCastedGDO<Appliance, SteamerMachine>());
            coffeeMachine.Processes.Add(new()
            {
                IsAutomatic = true,
                Process = GetCastedGDO<Process, SteamProcess>(),
                Speed = 1f,
                Validity = ProcessValidity.Generic
            });
        }

        private void UpdateMilk()
        {
            GetCastedGDO<Item, MilkIngredient>().DerivedProcesses.Add(new()
            {
                Duration = 2.6f,
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
    }

    public abstract class AccessedItemGroupView : ItemGroupView
    {
        protected abstract List<ComponentGroup> groups { get; }
        protected virtual List<ColourBlindLabel> labels => new();

        public GameObject LabelGameObject;

        public void Setup(GameDataObject gdo)
        {
            ComponentGroups = groups;

            if (labels.Count > 0)
            {
                ComponentLabels = labels;
                LabelGameObject = ColorblindUtils.cloneColourBlindObjectAndAddToItem(gdo as Item);
                ColorblindUtils.setColourBlindLabelObjectOnItemGroupView(this, LabelGameObject);
            }
        }
    }
}
