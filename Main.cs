global using IngredientLib;
global using IngredientLib.Ingredient.Items;
global using Kitchen;
global using KitchenData;
global using KitchenLib;
global using KitchenLib.Colorblind;
global using KitchenLib.Customs;
global using KitchenLib.Event;
global using KitchenLib.References;
global using KitchenLib.Utils;
global using KitchenMods;
global using MessagePack;
global using MiniCafe.Appliances;
global using MiniCafe.Components;
global using MiniCafe.Extras;
global using MiniCafe.Items;
global using MiniCafe.Mains.Coffee;
global using MiniCafe.Mains.Tea;
global using MiniCafe.Processes;
global using MiniCafe.Views;
global using System.Collections.Generic;
global using System.Linq;
global using System.Reflection;
global using TMPro;
global using Unity.Collections;
global using Unity.Entities;
global using UnityEngine;
global using static KitchenLib.Utils.GDOUtils;
global using static KitchenLib.Utils.KitchenPropertiesUtils;
global using static MiniCafe.Extras.ExtraHelper;
global using static MiniCafe.Helper;
using ApplianceLib.Api;
using KitchenLib.Registry;

namespace MiniCafe
{
    public class Main : BaseMod
    {
        public const string GUID = "nova.minicafe";
        public const string VERSION = "1.6.0";

        public Main() : base(GUID, "Mini Cafe", "Depleted Supernova#1957", VERSION, ">=1.0.0", Assembly.GetExecutingAssembly()) { }

        internal static AssetBundle Bundle;

        internal static bool PaperPlatesInstalled => ModRegistery.Registered.Any(modPair => modPair.Value.ModID == "paperPlates");

        internal void AddMaterials()
        {
            AddMaterial(CreateTransparent("Glass", 0xF6FEFF, 0.6f));

            AddMaterial(CreateFlat("Light Coffee Cup", 0xDAC7AB));

            // Coffee
            AddMaterial(CreateFlat("Coffee Blend", 0xAF8967));
            AddMaterial(CreateFlat("Coffee Foam", 0xE0C2A8));

            AddMaterial(CreateFlat("Americano", 0x895238));

            // Tea
            AddMaterial(CreateFlat("Sage", 0x80B25C));
            AddMaterial(CreateFlat("Sage Dried 1", 0xB1AB82));
            AddMaterial(CreateFlat("Sage Dried 2", 0x8D8043));
            AddMaterial(CreateFlat("Sage Tea", 0xE6EDA6));

            AddMaterial(CreateFlat("Earl Grey", 0x2E2818));
            AddMaterial(CreateFlat("Earl Grey Extra", 0xAC7021));
            AddMaterial(CreateFlat("Earl Grey Tea", 0x59221C));

            AddMaterial(CreateFlat("Hibiscus", 0x452425));
            AddMaterial(CreateFlat("Hibiscus Extra", 0x892A31));
            AddMaterial(CreateFlat("Hibiscus Teapot", 0x991327));
            AddMaterial(CreateFlat("Hibiscus Tea", 0xB5153A));

            // Sides
            AddMaterial(CreateFlat("Croissant", 0xDA9134));
            AddMaterial(CreateFlat("Stroop", 0xC97919));

            // Desserts
            AddMaterial(CreateFlat("Lava Cake Light", 0xA05000));
            AddMaterial(CreateFlat("Lava Cake Dark", 0x633100));
        }

        internal static string DirtyMugKey = "DirtyMugs";
        private void UpdateMugTransfer()
        {
            RestrictedItemTransfers.AllowItem(DirtyMugKey, GetCastedGDO<Item, BigMugDirty>());
            RestrictedItemTransfers.AllowItem(DirtyMugKey, GetCastedGDO<Item, SmallMugDirty>());
        }

        private void UpdateCoffeeMachine()
        {
            // Coffee Machine
            var coffeeMachine = GetExistingGDO(ApplianceReferences.CoffeeMachine) as Appliance;
            coffeeMachine.Upgrades.Add(GetCastedGDO<Appliance, BaristaMachine>());
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

        private void UpdateLemon()
        {
            var lemon = GetCastedGDO<Item, ChoppedLemon>();
            lemon.SplitCount = 1;
            lemon.AllowSplitMerging = true;
            lemon.PreventExplicitSplit = true;
            lemon.SplitDepletedItems = new() { GetCastedGDO<Item, LemonSlice>() };
            lemon.SplitSubItem = GetCastedGDO<Item, LemonSlice>();
        }

        private void UpdateAppliances(GameData gameData)
        {
            foreach (var appliance in gameData.Get<Appliance>())
            {
                #region Steeping
                var hasProvider = appliance.GetProperty<CItemProvider>(out var cProvider);
                var isFreezer = appliance.ID == ApplianceReferences.Freezer;
                if ((appliance.GetProperty<CItemHolder>(out var _) && hasProvider && cProvider.AutoPlaceOnHolder && cProvider.Maximum == 1) || 
                    appliance.Name.ToLower().Contains("counter") || isFreezer)
                {
                    appliance.Processes.Add(new()
                    {
                        Process = GetCastedGDO<Process, SteepProcess>(),
                        IsAutomatic = true,
                        Speed = isFreezer ? 1 : 2f,
                        Validity = ProcessValidity.Generic
                    });
                }
                #endregion
            }
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
                UpdateCoffeeMachine();
                UpdateMilk();
                UpdateLemon();

                UpdateAppliances(args.gamedata);
                UpdateMugTransfer();

                args.gamedata.ProcessesView.Initialise(args.gamedata);
            };
        }

        internal void AddGameData()
        {
            MethodInfo AddGDOMethod = typeof(BaseMod).GetMethod(nameof(BaseMod.AddGameDataObject));
            int counter = 0;
            Log("Registering GameDataObjects.");
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.IsAbstract || typeof(IWontRegister).IsAssignableFrom(type))
                    continue;

                if (!typeof(CustomGameDataObject).IsAssignableFrom(type))
                    continue;

                MethodInfo generic = AddGDOMethod.MakeGenericMethod(type);
                generic.Invoke(this, null);
                counter++;
            }
            Log($"Registered {counter} GameDataObjects.");
        }

    }
}
