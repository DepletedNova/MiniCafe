namespace MiniCafe.Extras
{
    internal static class ExtraHelper
    {
        internal static HashSet<Dish.IngredientUnlock> GetUnlocks(Item GDO)
        {
            return new()
            {
                // Espresso
                new()
                {
                    Ingredient = GDO,
                    MenuItem = GetCastedGDO<ItemGroup, PlatedBigEspresso>()
                },
                new()
                {
                    Ingredient = GDO,
                    MenuItem = GetCastedGDO<ItemGroup, PlatedSmallEspresso>()
                },
                // Americano
                new()
                {
                    Ingredient = GDO,
                    MenuItem = GetCastedGDO<ItemGroup, PlatedBigAmericano>()
                },
                new()
                {
                    Ingredient = GDO,
                    MenuItem = GetCastedGDO<ItemGroup, PlatedSmallAmericano>()
                },
                // Iced
                new()
                {
                    Ingredient = GDO,
                    MenuItem = GetCastedGDO<ItemGroup, PlatedBigIced>()
                },
                new()
                {
                    Ingredient = GDO,
                    MenuItem = GetCastedGDO<ItemGroup, PlatedSmallIced>()
                },
                // Cappuccino
                new()
                {
                    Ingredient = GDO,
                    MenuItem = GetCastedGDO<ItemGroup, PlatedBigCappuccino>()
                },
                new()
                {
                    Ingredient = GDO,
                    MenuItem = GetCastedGDO<ItemGroup, PlatedSmallCappuccino>()
                },

                // Earl Grey
                new()
                {
                    Ingredient = GDO,
                    MenuItem = GetCastedGDO<ItemGroup, PlatedBigEarlGrey>()
                },
                new()
                {
                    Ingredient = GDO,
                    MenuItem = GetCastedGDO<ItemGroup, PlatedSmallEarlGrey>()
                },
                // Herbal Sage
                new()
                {
                    Ingredient = GDO,
                    MenuItem = GetCastedGDO<ItemGroup, PlatedBigSage>()
                },
                new()
                {
                    Ingredient = GDO,
                    MenuItem = GetCastedGDO<ItemGroup, PlatedSmallSage>()
                },
                // Hibiscus
                new()
                {
                    Ingredient = GDO,
                    MenuItem = GetCastedGDO<ItemGroup, PlatedBigHibiscus>()
                },
                new()
                {
                    Ingredient = GDO,
                    MenuItem = GetCastedGDO<ItemGroup, PlatedSmallHibiscus>()
                },
            };
        }

        public static ItemGroup.ItemSet ExtrasSet => new()
        {
            Items = new()
            {
                GetCastedGDO<Item, Teaspoon>(),
                GetCastedGDO<Item, Croissant>(),
                GetCastedGDO<Item, Scone>()
            },
            RequiresUnlock = true,
            Max = 1,
            Min = 1,
        };
    }
}
