using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace MiniCafe.Coffee.Large
{
    public static class LargeDishRegistry
    {
        public static int ID { get; internal set; }

        public static Dictionary<int, int> Registry = new();

        public static void RegisterLarge(this Dish largeDish, int smallDish) => Registry.Add(smallDish, largeDish.ID);
        public static void RegisterLarge<T>(this Dish largeDish) where T : CustomDish => Registry.Add(GetCastedGDO<Dish, T>().ID, largeDish.ID);
    }

    public class LargeCoffeeDish : CustomDish
    {
        public override string UniqueNameID => "large_coffee_dish";
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Large Coffee");
        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Large Coffee");
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
        public override bool IsUnlockable => false;

        public override bool RequiredNoDishItem => true;

        public override bool HideInfoPanel => true;

        public override DishType Type => DishType.Dessert;
        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, LargeCoffee>(),
                Phase = MenuPhase.Dessert,
                Weight = 1f
            }
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetCastedGDO<Item, LargeCup>(),
        };
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Take a large cup from the machine and place it on the machine to fill it." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Large Black Coffee", "Adds large black coffee as a coffee variant", ""))
        };

        public override List<Unlock> HardcodedRequirements => new()
        {
            GetCastedGDO<Unlock, LargeMugsCard>(),
            GetGDO<Unlock>(CoffeeBaseDish)
        };

        public override void OnRegister(Dish gdo)
        {
            gdo.RegisterLarge(CoffeeBaseDish);
        }
    }

    public class LargeIcedCoffeeDish : CustomDish
    {
        public override string UniqueNameID => "large_iced_coffee_dish";
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Large Coffee");
        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Large Coffee");
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
        public override bool IsUnlockable => false;

        public override bool RequiredNoDishItem => true;

        public override bool HideInfoPanel => true;

        public override DishType Type => DishType.Dessert;
        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, LargeIced>(),
                Phase = MenuPhase.Dessert,
                Weight = 1f
            }
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetCastedGDO<Item, LargeCup>(),
        };
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Prepare large black coffee, place on ice dispenser and interact to create iced coffee." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Large Iced Coffee", "Adds large iced coffee as a coffee variant", ""))
        };

        public override List<Unlock> HardcodedRequirements => new()
        {
            GetCastedGDO<Unlock, LargeMugsCard>(),
            GetGDO<Unlock>(IcedDish),
        };

        public override void OnRegister(Dish gdo)
        {
            gdo.RegisterLarge(IcedDish);
        }
    }

    public class LargeAffogatoDish : CustomDish
    {
        public override string UniqueNameID => "large_affogato_dish";
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Large Coffee");
        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Large Coffee");
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
        public override bool IsUnlockable => false;

        public override bool RequiredNoDishItem => true;

        public override bool HideInfoPanel => true;

        public override DishType Type => DishType.Dessert;
        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, LargeAffogato>(),
                Phase = MenuPhase.Dessert,
                Weight = 1f
            }
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetCastedGDO<Item, LargeCup>(),
        };
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Prepare large black coffee, add vanilla ice cream and serve." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Large Affogato", "Adds large affogato as a coffee variant", ""))
        };

        public override List<Unlock> HardcodedRequirements => new()
        {
            GetCastedGDO<Unlock, LargeMugsCard>(),
            GetGDO<Unlock>(UnlockReferences.Affogato),
        };

        public override void OnRegister(Dish gdo)
        {
            gdo.RegisterLarge(UnlockReferences.Affogato);
        }
    }

    public class LargePumpkinSpiceDish : CustomDish
    {
        public override string UniqueNameID => "large_pumpkin_spice_dish";
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Large Coffee");
        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Large Coffee");
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
        public override bool IsUnlockable => false;

        public override bool RequiredNoDishItem => true;

        public override bool HideInfoPanel => true;

        public override DishType Type => DishType.Dessert;
        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, LargePumpkinSpice>(),
                Phase = MenuPhase.Dessert,
                Weight = 1f
            }
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetCastedGDO<Item, LargeCup>(),
        };
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Prepare large latte. Dispose pumpkin seeds and chop up pumpkin before adding sugar. Add to latte." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Large Pumpkin Spice", "Adds large pumpkin spice latte as a coffee variant", ""))
        };

        public override List<Unlock> HardcodedRequirements => new()
        {
            GetCastedGDO<Unlock, LargeMugsCard>(),
            GetCastedGDO<Unlock, PumpkinSpiceDish>(),
        };

        public override void OnRegister(Dish gdo)
        {
            gdo.RegisterLarge<PumpkinSpiceDish>();
        }
    }

    public class LargeLatteDish : CustomDish
    {
        public override string UniqueNameID => "large_latte_dish";
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Large Coffee");
        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Large Coffee");
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
        public override bool IsUnlockable => false;

        public override bool RequiredNoDishItem => true;

        public override bool HideInfoPanel => true;

        public override DishType Type => DishType.Dessert;
        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, LargeLatte>(),
                Phase = MenuPhase.Dessert,
                Weight = 1f
            }
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetCastedGDO<Item, LargeCup>(),
        };
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Place milk in steamer to fill it. Prepare black coffee, place on machine and interact to create latte." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Large Latte", "Adds large latte as a coffee variant", ""))
        };

        public override List<Unlock> HardcodedRequirements => new()
        {
            GetCastedGDO<Unlock, LargeMugsCard>(),
            GetGDO<Unlock>(LatteDish)
        };

        public override void OnRegister(Dish gdo)
        {
            gdo.RegisterLarge(LatteDish);
        }
    }

    public class LargeAmericanoDish : CustomDish
    {
        public override string UniqueNameID => "large_americano_dish";
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Large Coffee");
        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Large Coffee");
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
        public override bool IsUnlockable => false;

        public override bool RequiredNoDishItem => true;

        public override bool HideInfoPanel => true;

        public override DishType Type => DishType.Dessert;
        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, LargeAmericano>(),
                Phase = MenuPhase.Dessert,
                Weight = 1f
            }
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetCastedGDO<Item, LargeCup>(),
        };
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Prepare black coffee, place on machine to create americano." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Large Americano", "Adds large americano as a coffee variant", ""))
        };

        public override List<Unlock> HardcodedRequirements => new()
        {
            GetCastedGDO<Unlock, LargeMugsCard>(),
            GetCastedGDO<Unlock, AmericanoDish>(),
        };

        public override void OnRegister(Dish gdo)
        {
            gdo.RegisterLarge<AmericanoDish>();
        }
    }
}
