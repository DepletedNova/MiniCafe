using Kitchen;
using KitchenMods;
using MiniCafe.Components;
using Unity.Collections;
using Unity.Entities;

namespace MiniCafe.Systems
{
    public class DualLimitedNightReset : StartOfNightSystem, IModSystem
    {
        private EntityQuery Query;
        protected override void Initialise()
        {
            base.Initialise();
            Query = GetEntityQuery(new QueryHelper().All(typeof(CDualLimitedProvider), typeof(CItemProvider)));
        }

        protected override void OnUpdate()
        {
            using var providers = Query.ToEntityArray(Allocator.Temp);
            foreach (var provider in providers)
            {
                var limitedProvider = EntityManager.GetComponentData<CDualLimitedProvider>(provider);

                limitedProvider.Available1 = limitedProvider.Maximum1;
                limitedProvider.Available2 = limitedProvider.Maximum2;

                EntityManager.SetComponentData(provider, limitedProvider);
            }
        }
    }
}
