global using IngredientLib.Ingredient.Items;
global using Kitchen;
global using KitchenData;
global using KitchenLib;
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
global using MiniCafe.Mains;
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
global using static MiniCafe.Helper;
using ApplianceLib.Api;
using KitchenLib.Registry;
using MiniCafe.Appliances.Spills;
using MiniCafe.Coffee;
using MiniCafe.Desserts;

namespace MiniCafe
{
    public class Main : BaseMod
    {
        public const string GUID = "nova.minicafe";
        public const string VERSION = "2.1.4";

        public Main() : base(GUID, "Mini Cafe", "Depleted Supernova#1957", VERSION, ">=1.0.0", Assembly.GetExecutingAssembly()) { }

        internal static AssetBundle Bundle;

        internal static bool PaperPlatesInstalled => ModRegistery.Registered.Any(modPair => modPair.Value.ModID == "paperPlates");

        internal static readonly RestaurantStatus OVERFILLING_STATUS = (RestaurantStatus)VariousUtils.GetID("OverfillingStatus");
        internal static readonly RestaurantStatus LARGE_MUG_STATUS = (RestaurantStatus)VariousUtils.GetID("LargeMugStatus");

        internal void AddMaterials()
        {
            AddMaterial(CreateFlat("Light Coffee Cup", 0xF2CD9B));

            // Coffee
            AddMaterial(CreateFlat("Coffee Blend", 0xAF8967));
            AddMaterial(CreateFlat("Coffee Foam", 0xE3D7C2));

            AddMaterial(CreateFlat("Americano", 0x895238));

            // Tea
            AddMaterial(CreateFlat("Sage", 0x80B25C));
            AddMaterial(CreateFlat("Sage Dried 1", 0xB1AB82));
            AddMaterial(CreateFlat("Sage Dried 2", 0x8D8043));
            AddMaterial(CreateFlat("Sage Tea", 0x5B7329));

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

        internal static string DirtyMugKey = "MiniCafe-DirtyMugs";
        private void UpdateDirtyMugTransfer()
        {
            RestrictedItemTransfers.AllowItem(DirtyMugKey, GetCastedGDO<Item, BigMugDirty>());
            RestrictedItemTransfers.AllowItem(DirtyMugKey, GetCastedGDO<Item, SmallMugDirty>());
        }

        internal static string GenericMugKey = "MiniCafe-MugItems";

        internal static string EmptyMugKey = "MiniCafe-EmptyMugs";
        internal static string FilledMugKey = "MiniCafe-FilledMugs";
        private void UpdateGenericMugTransfers(GameData gameData)
        {
            RestrictedItemTransfers.AllowProcessableItems(EmptyMugKey, ProcessReferences.FillCoffee);

            foreach (var item in gameData.Get<Item>())
            {
                foreach (var process in item.DerivedProcesses)
                {
                    if (process.Process.ID == ProcessReferences.FillCoffee)
                    {
                        RestrictedItemTransfers.AllowItem(FilledMugKey, process.Result);
                        break;
                    }
                }
            }
        }

        private void UpdateCoffee()
        {
            // Coffee Machine
            var coffeeMachine = GetGDO<Appliance>(ApplianceReferences.CoffeeMachine);
            coffeeMachine.Upgrades.Add(GetCastedGDO<Appliance, BaristaMachine>());
            coffeeMachine.Properties = new()
            {
                new CItemHolder(),
                GetCItemProvider(ItemReferences.CoffeeCup, 0, 0, false, false, true, false, false, false, false),
                new COverfills
                {
                    ID = GetCustomGameDataObject<CoffeeSpill1>().ID
                }
            };

            // Update Tea
            var teaCard = GetGDO<Dish>(TeaDish);
            teaCard.BlockedBy = new() { GetCastedGDO<Dish, EarlGreyDish>() };

            // Transfer & add AllowedDishes
            var coffeeBase = GetGDO<Dish>(CoffeeBaseDish);
            var coffeeMode = GetGDO<UnlockCard>(CoffeeshopMode);

            if (!coffeeBase.AllowedFoods.IsNullOrEmpty())
            {
                coffeeBase.BlocksAllOtherFood = false;
                coffeeMode.BlocksAllOtherFood = true;

                foreach (Unlock food in coffeeBase.AllowedFoods)
                    AddAllowed(coffeeMode, food);
            }

            // Base Coffee Card
            AddAllowed(coffeeMode, GetCastedGDO<Unlock, CroissantCoffeeDish>());
            AddAllowed(coffeeMode, GetCastedGDO<Unlock, SconeCoffeeDish>());
            AddAllowed(coffeeMode, GetCastedGDO<Unlock, CannoliCoffeeDish>());

            AddAllowed(coffeeMode, GetCastedGDO<Unlock, AmericanoDish>());

            // Add Colourblind to normal coffees
            AddColourBlind(ItemReferences.CoffeeCup, "S");
            AddColourBlind(ItemReferences.CoffeeCupCoffee, "SBl");
            AddColourBlind(LatteItem, "SLa");
            AddColourBlind(IcedCoffeeItem, "SIc");

        }

        private void AddAllowed(UnlockCard AddTo, Unlock Add)
        {
            if (!AddTo.AllowedFoods.Contains(Add))
                AddTo.AllowedFoods.Add(Add);
        }

        private void AddColourBlind(int itemID, string tag)
        {
            Item Item = GetGDO<Item>(itemID);
            Item CB_Parent = GetGDO<Item>(ItemReferences.PieMeatCooked);
            if (CB_Parent != null && Item != null)
            {
                GameObject colourBlind = Object.Instantiate(CB_Parent.Prefab.GetChild("Colour Blind"));
                colourBlind.name = "Colour Blind";
                colourBlind.transform.SetParent(Item.Prefab.transform, false);
                colourBlind.GetChild("Title").GetComponent<TMP_Text>().text = tag;
            }
        }

        private void ApplyVisualOverrides()
        {
            OverrideCoffeeVisuals(ExtraCoffee);
            OverrideCoffeeVisuals(SlowBrew);
            OverrideCoffeeVisuals(MilkExtra);
            OverrideCoffeeVisuals(SugarExtra);
        }

        private void OverrideCoffeeVisuals(int id)
        {
            UnlockOverrides.AddIconOverride(id, "<sprite name=\"fill_coffee\">");
            UnlockOverrides.AddColourOverride(id, ColorFromHex(0x6D5140));
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

        private void UpdatePizzaCrust() => GetGDO<ItemGroup>(ItemReferences.PizzaCrust).DerivedProcesses.Add(new()
        {
            Process = GetGDO<Process>(ProcessReferences.Knead),
            Duration = 1.3f,
            Result = GetCastedGDO<Item, UncookedCannoliTray>()
        });

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
                UpdateLemon();
                UpdateCoffee();
                UpdatePizzaCrust(); 

                UpdateDirtyMugTransfer();
                UpdateGenericMugTransfers(args.gamedata);

                ApplyVisualOverrides();

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
