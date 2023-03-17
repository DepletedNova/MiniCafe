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
global using IngredientLib.Ingredient.Items;
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
global using static MiniCafe.Helper;

namespace MiniCafe
{
    public class Main : BaseMod
    {
        public const string GUID = "nova.minicafe";
        public const string VERSION = "1.4.2";

        public Main() : base(GUID, "Mini Cafe", "Depleted Supernova#1957", VERSION, ">=1.0.0", Assembly.GetExecutingAssembly()) { }

        internal static AssetBundle Bundle;

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

                if (typeof(CustomGameDataObject).IsAssignableFrom(type))
                {
                    MethodInfo generic = AddGDOMethod.MakeGenericMethod(type);
                    generic.Invoke(this, null);
                    counter++;
                }
            }
            Log($"Registered {counter} GameDataObjects.");
        }

    }
}
