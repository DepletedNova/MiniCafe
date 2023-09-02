global using static KitchenLib.Utils.MaterialUtils;
global using static Kitchen.ItemGroupView;
global using static KitchenLib.Utils.GDOUtils;
global using static MiniCafe.Helper;
global using static KitchenLib.Utils.KitchenPropertiesUtils;
using ApplianceLib.Api;
using IngredientLib.Ingredient.Items;
using Kitchen;
using KitchenData;
using KitchenLib;
using KitchenLib.Customs;
using KitchenLib.Event;
using KitchenLib.References;
using KitchenLib.Registry;
using KitchenLib.Utils;
using KitchenMods;
using MiniCafe.Appliances;
using MiniCafe.Appliances.Spills;
using MiniCafe.Coffee;
using MiniCafe.Components;
using MiniCafe.Desserts;
using MiniCafe.Items;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using Unity.Entities;
using UnityEngine;

namespace MiniCafe
{
    public class Main : BaseMod
    {
        public const string GUID = "nova.minicafe";
        public const string VERSION = "2.5.4";

        public Main() : base(GUID, "Mini Cafe", "Zoey Davis", VERSION, ">=1.0.0", Assembly.GetExecutingAssembly()) { }

        public static AssetBundle Bundle;

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

            // Bakery
            AddMaterial(CreateFlat("Cherry Cake", 0xC46373));
        }

        #region Transfers
        public static string DirtyMugKey = "MiniCafe-DirtyMugs";
        private void UpdateDirtyMugTransfer()
        {
            RestrictedItemTransfers.AllowItem(DirtyMugKey, GetCastedGDO<Item, BigMugDirty>());
            RestrictedItemTransfers.AllowItem(DirtyMugKey, GetCastedGDO<Item, SmallMugDirty>());
        }

        public static string GenericMugKey = "MiniCafe-MugItems";

        public static string EmptyMugKey = "MiniCafe-EmptyMugs";
        public static string FilledMugKey = "MiniCafe-FilledMugs";
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
        #endregion

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

        private void UpdateBakery()
        {
            // Had no properties for some reason (lemon tree)
            GetGDO<Appliance>(1470180731).Properties = new() { GetUnlimitedCItemProvider(2094624730) };
        }

        #region Flavouring
        // Pumpkin
        private GameObject pumpkinCake;
        private GameObject pumpkinCupcake;
        private GameObject pumpkinDonut;
        private GameObject pumpkinBakedCookie;
        private GameObject pumpkinCookie;

        // Cherry
        private GameObject cherryCake;
        private GameObject cherryCupcake;
        private GameObject cherryDonut;
        private GameObject cherryCookie;

        private void AddFlavours()
        {
            // Pumpkin
            pumpkinCake = Bundle.LoadAsset<GameObject>("Pumpkin Cake Slice");
            pumpkinCake.ApplyMaterialToChild("Cake", "Cake", "Pumpkin");
            pumpkinCake.ApplyMaterialToChild("Face", "Plastic - Black");

            pumpkinCupcake = Bundle.LoadAsset<GameObject>("Pumpkin Cupcake");
            pumpkinCupcake.ApplyMaterialToChild("Icing", "Pumpkin");
            pumpkinCupcake.ApplyMaterialToChild("Pumpkin/Body", "Plastic - Dark Green", "Plastic - Dark Green");
            pumpkinCupcake.ApplyMaterialToChild("Pumpkin/Top", "Plastic - Dark Green", "Plastic - Dark Green");

            pumpkinDonut = Bundle.LoadAsset<GameObject>("Pumpkin Donut");
            pumpkinDonut.ApplyMaterialToChild("Icing", "Pumpkin");
            pumpkinDonut.ApplyMaterialToChild("Stem", "Plastic - Dark Green");

            pumpkinBakedCookie = Bundle.LoadAsset<GameObject>("Pumpkin Baked Cookie");
            pumpkinBakedCookie.ApplyMaterialToChild("Cookie", "Pumpkin");
            pumpkinBakedCookie.ApplyMaterialToChild("Face", "Plastic - Black");

            pumpkinCookie = Bundle.LoadAsset<GameObject>("Pumpkin Cookie");
            pumpkinCookie.ApplyMaterialToChild("Cookie", "Pumpkin - Flesh");
            pumpkinCookie.ApplyMaterialToChild("Face", "Plastic - Black");

            AddFlavour(GetGDO<Item>(ItemReferences.PumpkinPieces), new(0, -0.06f, 0), new(), "Pu", pumpkinCake, pumpkinCupcake, pumpkinDonut, pumpkinBakedCookie, pumpkinCookie);

            // Cherry
            cherryCake = Bundle.LoadAsset<GameObject>("Cherry Cake Slice");
            cherryCake.ApplyMaterialToChild("Cake", "Cake", "Cherry Cake");
            cherryCake.ApplyMaterialToChild("Cherry", "Cherry", "Wood - Dark");

            cherryCupcake = Bundle.LoadAsset<GameObject>("Cherry Cupcake");
            cherryCupcake.ApplyMaterialToChild("Icing", "Cherry Cake");
            cherryCupcake.ApplyMaterialToChild("Cherry", "Cherry", "Wood - Dark");

            cherryDonut = Bundle.LoadAsset<GameObject>("Cherry Donut");
            cherryDonut.ApplyMaterialToChild("Icing", "Cherry Cake");
            cherryDonut.ApplyMaterialToChild("Cherry", "Cherry", "Wood - Dark");

            cherryCookie = Bundle.LoadAsset<GameObject>("Cherry Cookie");
            cherryCookie.ApplyMaterialToChild("Cherry", "Cherry", "Wood - Dark");

            AddFlavour(GetGDO<Item>(ItemReferences.Cherry), new(0, -0.06f, 0), new(), "Che", cherryCake, cherryCupcake, cherryDonut, cherryCookie, cherryCookie);
        }

        private void AddFlavour(Item item, Vector3 PosOffset, Quaternion RotOffset, string ColourblindTag,
            GameObject cakePrefab, GameObject cupcakePrefab, GameObject donutPrefab, GameObject bakedCookiePrefab, GameObject cookiePrefab)
        {
            var addCookie = cookiePrefab != null && bakedCookiePrefab != null;
            var addDonut = donutPrefab != null;
            var addCupcake = cupcakePrefab != null;
            var addCake = cakePrefab != null;

            var mixingBowl = GetGDO<ItemGroup>(-705806008);
            var mixingView = mixingBowl.Prefab.GetComponent<ItemGroupView>();

            var cookieTray = GetGDO<ItemGroup>(-491299234);
            var bakedCookies = GetGDO<ItemGroup>(-502245988);
            var cookie = GetGDO<Item>(333230026);

            var donut = GetGDO<ItemGroup>(-1312823003);

            var cupcake = GetGDO<ItemGroup>(1366309564);

            var cakeTin = GetGDO<ItemGroup>(-1354941517);
            var cakeSlice = GetGDO<Item>(-1532306603);

            if (mixingBowl.DerivedSets[0].Items.Any(p => p == item))
                return;

            // Update GDO
            #region Mixing Bowl
            var mixingPrior = new List<Item>(mixingBowl.DerivedSets[0].Items) { item };
            mixingBowl.DerivedSets[0] = new()
            {
                Items = mixingPrior,
                Max = 1,
                Min = 1,
                IsMandatory = true
            };
            #endregion
            #region Cookies
            if (addCookie)
            {
                var cookiePrior = new List<Item>(cookieTray.DerivedSets[1].Items) { item };
                cookieTray.DerivedSets[1] = new()
                {
                    Items = cookiePrior,
                    Max = 1,
                    Min = 1,
                    IsMandatory = true
                };
            }
            #endregion
            #region Donut
            if (addDonut)
            {
                var donutPrior = new List<Item>(donut.DerivedSets[1].Items) { item };
                donut.DerivedSets[1] = new()
                {
                    Items = donutPrior,
                    Max = 1,
                    Min = 1,
                    IsMandatory = true
                };
            }
            #endregion
            #region Cupcake
            if (addCupcake)
            {
                var cupcakePrior = new List<Item>(cupcake.DerivedSets[1].Items) { item };
                cupcake.DerivedSets[1] = new()
                {
                    Items = cupcakePrior,
                    Max = 1,
                    Min = 1,
                    IsMandatory = true
                };
            }
            #endregion
            #region Cake
            if (addCake)
            {
                var cakePrior = new List<Item>(cakeTin.DerivedSets[1].Items) { item };
                cakeTin.DerivedSets[1] = new()
                {
                    Items = cakePrior,
                    Max = 1,
                    Min = 1,
                    IsMandatory = true
                };
            }
            #endregion

            if (mixingView.ComponentGroups.Any(c => c.Item == item))
                return;

            // Add prefabs
            var componentLabels = ReflectionUtils.GetField<ItemGroupView>("ComponentLabels");
            #region Mixing Bowl
            var itemPrefab = Object.Instantiate(item.Prefab);
            itemPrefab.transform.SetParent(mixingBowl.Prefab.transform.Find("Flavours"), false);
            itemPrefab.transform.localPosition = PosOffset;
            itemPrefab.transform.localRotation = RotOffset;
            itemPrefab.transform.localScale = Vector3.one / itemPrefab.transform.parent.localScale.x;
            #endregion
            #region Cookies
            if (addCookie)
            {
                // Unbaked
                for (int i = 0; i < cookieTray.Prefab.GetChildCount(); i++)
                {
                    var child = cookieTray.Prefab.GetChild(i);
                    if (child.name.Contains("Tray"))
                        continue;

                    SetupCookie(item, ColourblindTag, child, cookiePrefab);
                }
                // Baked
                for (int i = 0; i < bakedCookies.Prefab.GetChildCount(); i++)
                {
                    var child = bakedCookies.Prefab.GetChild(i);
                    if (child.name.Contains("Tray"))
                        continue;

                    SetupCookie(item, ColourblindTag, child, bakedCookiePrefab);
                }
                // Cookie
                SetupCookie(item, ColourblindTag, cookie.Prefab, bakedCookiePrefab);
            }
            #endregion
            #region Donut
            var donutPref = Object.Instantiate(donutPrefab);
            if (addDonut)
            {
                donutPref.transform.SetParent(donut.Prefab.transform.Find("Flavours"), false);
                donutPref.transform.localScale = Vector3.one / donutPref.transform.parent.localScale.x;
            }
            #endregion
            #region Cupcake
            var cupcakePref = Object.Instantiate(cupcakePrefab);
            if (addCupcake)
                cupcakePref.transform.SetParent(cupcake.Prefab.transform, false);
            #endregion
            #region Cake
            if (addCake)
            {
                for (int i = 0; i < cakeTin.Prefab.GetChild("Cake").GetChildCount(); i++)
                {
                    var child = cakeTin.Prefab.GetChild("Cake").GetChild(i);
                    if (!child.name.Contains("Cake"))
                        continue;

                    SetupCake(item, ColourblindTag, child, cakePrefab);
                }

                SetupCake(item, ColourblindTag, cakeSlice.Prefab, cakePrefab);
            }
            #endregion

            // Update ItemGroupViews
            #region Mixing Bowl
            mixingView.ComponentGroups.Add(new()
            {
                GameObject = itemPrefab,
                Item = item
            });
            #endregion
            #region Donut
            if (addDonut)
            {
                var donutView = donut.Prefab.GetComponent<ItemGroupView>();
                donutView.ComponentGroups.Add(new()
                {
                    GameObject = donutPref,
                    Item = item
                });
                List<ColourBlindLabel> donutLabels = (List<ColourBlindLabel>)componentLabels.GetValue(donutView);
                donutLabels.Add(new()
                {
                    Item = item,
                    Text = ColourblindTag
                });
                componentLabels.SetValue(donutView, donutLabels);
            }
            #endregion
            #region Cupcake
            if (addCupcake)
            {
                var cupcakeView = cupcake.Prefab.GetComponent<ItemGroupView>();
                cupcakeView.ComponentGroups.Add(new()
                {
                    GameObject = cupcakePref,
                    Item = item
                });
                List<ColourBlindLabel> cupcakeLabels = (List<ColourBlindLabel>)componentLabels.GetValue(cupcakeView);
                cupcakeLabels.Add(new()
                {
                    Item = item,
                    Text = ColourblindTag
                });
                componentLabels.SetValue(cupcakeView, cupcakeLabels);
            }
            #endregion
        }

        private void SetupCookie(Item item, string tag, GameObject baseObject, GameObject toInstantiate)
        {
            var componentLabels = ReflectionUtils.GetField<ItemGroupView>("ComponentLabels");
            var prefab = Object.Instantiate(toInstantiate);
            prefab.transform.SetParent(baseObject.transform.Find("Flavours"), false);
            prefab.transform.localScale = Vector3.one / prefab.transform.parent.localScale.x;

            var cookieView = baseObject.GetComponent<ItemGroupView>();
            cookieView.ComponentGroups.Add(new()
            {
                Item = item,
                GameObject = prefab
            });
            List<ColourBlindLabel> labels = (List<ColourBlindLabel>)componentLabels.GetValue(cookieView);
            labels.Add(new()
            {
                Item = item,
                Text = tag
            });
            componentLabels.SetValue(cookieView, labels);
        }

        private void SetupCake(Item item, string tag, GameObject baseObject, GameObject toInstantiate)
        {
            var componentLabels = ReflectionUtils.GetField<ItemGroupView>("ComponentLabels");
            var prefab = Object.Instantiate(toInstantiate);
            prefab.transform.SetParent(baseObject.transform, false);
            prefab.transform.localScale = Vector3.one;

            var cakeView = baseObject.GetComponent<ItemGroupView>();
            cakeView.ComponentGroups.Add(new()
            {
                Item = item,
                GameObject = prefab
            });
            List<ColourBlindLabel> labels = (List<ColourBlindLabel>)componentLabels.GetValue(cakeView);
            labels.Add(new()
            {
                Item = item,
                Text = tag
            });
            componentLabels.SetValue(cakeView, labels);
        }
        #endregion

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
                UpdateCoffee();
                AddFlavours();
                UpdateBakery();

                UpdateDirtyMugTransfer();
                UpdateGenericMugTransfers(args.gamedata);

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
