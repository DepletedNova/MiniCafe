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
global using static IngredientLib.Util.Helper;
global using static IngredientLib.Util.VisualEffectHelper;

global using System.Linq;
global using System.Reflection;
global using System.Collections.Generic;

global using UnityEngine;

global using Unity.Entities;
global using Unity.Collections;

global using MessagePack;

global using MiniCafe.Appliances;
global using MiniCafe.Misc;
global using MiniCafe.Items;
global using MiniCafe.Dishes;
global using static MiniCafe.MaterialHelper;
global using static MiniCafe.Utilities;

using IngredientLib.Ingredient.Items;

namespace MiniCafe
{
    public class Main : BaseMod
    {
        public const string GUID = "nova.minicafe";
        public const string VERSION = "0.0.1";

        public Main() : base(GUID, "Mini Cafe", "Depleted Supernova#1957", VERSION, ">=1.0.0", Assembly.GetExecutingAssembly()) { }

        internal static AssetBundle Bundle;

        internal void AddGameData()
        {
            // Processes
            AddGameDataObject<SteamProcess>();

            // Appliances
            AddGameDataObject<MugCabinet>();
            AddGameDataObject<BaristaMachine>();

            // Items
            AddGameDataObject<BigMug>();
            AddGameDataObject<BigMugDirty>();
            AddGameDataObject<BigEspresso>();
            AddGameDataObject<BigCappuccino>();

            AddGameDataObject<SmallMug>();
            AddGameDataObject<SmallMugDirty>();
            AddGameDataObject<SmallEspresso>();
            AddGameDataObject<SmallCappuccino>();

            AddGameDataObject<SteamedMilk>();

            // Dishes
            AddGameDataObject<EspressoDish>();
            AddGameDataObject<CappuccinoDish>();
        }

        internal void AddMaterials()
        {
            AddMaterial(CreateTransparent("Glass", 0xF6FEFF, 0.6f));

            // Coffee
            AddMaterial(CreateFlat("Coffee Blend", 0xAF8967));
            AddMaterial(CreateFlat("Coffee Foam", 0xE0C2A8));
            AddMaterial(CreateFlat("Light Coffee Cup", 0xDAC7AB));
        }

        private void UpdateCoffee()
        {
            // Coffee Machine
            var coffeeMachine = GetExistingGDO(ApplianceReferences.CoffeeMachine) as Appliance;
            coffeeMachine.Upgrades.Add(GetCastedGDO<Appliance, BaristaMachine>());
            coffeeMachine.Processes = new()
            {
                new()
                {
                    IsAutomatic = true,
                    Process = GetExistingGDO(ProcessReferences.FillCoffee) as Process,
                    Speed = 0.75f,
                    Validity = ProcessValidity.Generic
                },
                new()
                {
                    IsAutomatic = true,
                    Process = GetCastedGDO<Process, SteamProcess>(),
                    Speed = 0.75f,
                    Validity = ProcessValidity.Generic
                }
            };
            coffeeMachine.RequiresProcessForShop = new()
            {
                GetExistingGDO(ProcessReferences.FillCoffee) as Process,
                GetCastedGDO<Process, SteamProcess>()
            };
            coffeeMachine.ShoppingTags = ShoppingTags.Cooking | ShoppingTags.Basic;
            coffeeMachine.SellOnlyAsDuplicate = false;

            // Dish
            var blackCoffee = GetExistingGDO(DishReferences.CoffeeDessert) as Dish;
            blackCoffee.BlockedBy.Add(GetCastedGDO<Unlock, EspressoDish>());
            blackCoffee.RequiredProcesses = new()
            {
                GetExistingGDO(ProcessReferences.FillCoffee) as Process,
                GetCastedGDO<Process, SteamProcess>()
            };
        }

        private void UpdateMilk()
        {
            GetCastedGDO<Item, MilkIngredient>().DerivedProcesses.Add(new()
            {
                Duration = 2.5f,
                Process = GetCastedGDO<Process, SteamProcess>(),
                Result = GetCastedGDO<Item, SteamedMilk>()
            });;
        }

        public override void PostActivate(Mod mod)
        {
            Bundle = mod.GetPacks<AssetBundleModPack>().SelectMany(e => e.AssetBundles).ToList()[0];

            AddGameData();

            AddMaterials();

            Events.BuildGameDataEvent += (s, args) =>
            {
                UpdateCoffee();
                UpdateMilk();

                args.gamedata.ProcessesView.Initialise(args.gamedata);
            };
        }
    }
}
