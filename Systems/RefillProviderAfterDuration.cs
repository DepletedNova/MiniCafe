using Kitchen;
using KitchenData;
using KitchenMods;
using MiniCafe.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace MiniCafe.Systems
{
    public class RefillProviderAfterDuration : GameSystemBase, IModSystem
    {
        private EntityQuery Query;
        protected override void Initialise()
        {
            base.Initialise();
            Query = GetEntityQuery(new QueryHelper()
                .All(typeof(CAppliance), typeof(CTakesDuration), typeof(CItemProvider), typeof(CRefillOnEmpty)));
        }

        protected override void OnUpdate()
        {
            using var entities = Query.ToEntityArray(Allocator.Temp);

            foreach (var entity in entities)
            {
                if (!GameData.Main.TryGet<Appliance>(GetComponent<CAppliance>(entity).ID, out var _))
                    continue;

                var cDuration = GetComponent<CTakesDuration>(entity);

                if (!(cDuration.Remaining <= 0f && cDuration.Active))
                    continue;

                var cRefill = GetComponent<CRefillOnEmpty>(entity);

                var cProvider = GetComponent<CItemProvider>(entity);
                cProvider.Available = math.min(cProvider.Maximum, cProvider.Available + math.max(cRefill.FillIncrement, 1));
                Set(entity, cProvider);

                if (cProvider.Available >= cProvider.Maximum && cRefill.Active)
                {
                    cRefill.Active = false;
                    Set(entity, cRefill);
                }
            }
        }
    }
}
