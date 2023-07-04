using MiniCafe.Coffee.Large;

namespace MiniCafe.Systems
{
    public class AddLargeCards : RestaurantSystem, IModSystem
    {
        private EntityQuery UnlockQuery;
        private EntityQuery NewDishes;
        protected override void Initialise()
        {
            base.Initialise();
            UnlockQuery = GetEntityQuery(new QueryHelper().All(typeof(CProgressionUnlock)));
            NewDishes = GetEntityQuery(new QueryHelper().All(typeof(CNewDish)));
        }

        private List<int> SmallUnlocks = new();
        private List<int> LargeUnlocks = new();

        protected override void OnUpdate()
        {
            if (!HasStatus(Main.LARGE_MUG_STATUS) || !NewDishes.IsEmpty)
                return;

            SmallUnlocks.Clear();
            LargeUnlocks.Clear();

            using var unlocks = UnlockQuery.ToComponentDataArray<CProgressionUnlock>(Allocator.Temp);
            foreach (var unlock in unlocks)
            {
                if (SmallUnlocks.Contains(unlock.ID) || LargeUnlocks.Contains(unlock.ID))
                    continue;

                if (LargeDishRegistry.Registry.ContainsKey(unlock.ID))
                    SmallUnlocks.Add(unlock.ID);
                else if (LargeDishRegistry.Registry.ContainsValue(unlock.ID))
                    LargeUnlocks.Add(unlock.ID);
            }

            if (SmallUnlocks.Count == LargeUnlocks.Count)
                return;

            foreach (var small in SmallUnlocks)
            {
                var associatedLarge = LargeDishRegistry.Registry[small];
                if (LargeUnlocks.Contains(associatedLarge))
                    continue;

                Set(EntityManager.CreateEntity(), new CNewDish
                {
                    ID = associatedLarge,
                    ShowRecipe = false
                });

                Set(EntityManager.CreateEntity(), new CProgressionUnlock
                {
                    ID = associatedLarge,
                    FromFranchise = false,
                    Type = CardType.Default
                });
            }
        }
    }
}
