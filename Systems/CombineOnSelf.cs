namespace MiniCafe.Systems
{
    [UpdateAfter(typeof(GrabItems))]
    [UpdateBefore(typeof(InteractionGroup))]
    public class CombineOnSelf : GenericSystemBase, IModSystem
    {
        private EntityContext ctx;

        private EntityQuery Query;
        protected override void Initialise()
        {
            Query = GetEntityQuery(new QueryHelper().All(typeof(CAppliance), typeof(CCombinesOnSelf), typeof(CItemHolder), typeof(CItemProvider)));
        }

        protected override void OnUpdate()
        {
            ctx = new EntityContext(EntityManager);

            using var appliances = Query.ToEntityArray(Allocator.Temp);
            foreach (var appliance in appliances)
            {
                var cHolder = GetComponent<CItemHolder>(appliance);
                var cProvider = GetComponent<CItemProvider>(appliance);

                if (cHolder.HeldItem == Entity.Null || !Has<CItem>(cHolder.HeldItem))
                    continue;

                if (cProvider.ProvidedItem == 0 || (cProvider.Maximum > 0 && cProvider.Available <= 0))
                    continue;

                Entity newItem;
                if (!ctx.AttemptItemMerge(out newItem, cHolder.HeldItem, cProvider.ProvidedItem, new ItemList(cProvider.ProvidedItem)))
                    continue;

                if (cProvider.Maximum > 0)
                {
                    cProvider.Available--;
                    ctx.Set(appliance, cProvider);
                }

                ctx.Destroy(cHolder.HeldItem);
                ctx.Set(newItem, new CHeldBy { Holder = appliance });
                ctx.Set(appliance, new CItemHolder { HeldItem = newItem });
            }
        }
    }
}
