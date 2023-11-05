using Kitchen;
using KitchenMods;
using MiniCafe.Components;
using Unity.Collections;
using Unity.Entities;

namespace MiniCafe.Systems
{
    public class PreventDurationTransfer : DaySystem, IModSystem
    {
        private EntityQuery Query;
        protected override void Initialise()
        {
            base.Initialise();
            Query = GetEntityQuery(typeof(CPreventTransferDuringDuration), typeof(CTakesDuration));
        }

        protected override void OnUpdate()
        {
            using (var entities = Query.ToEntityArray(Allocator.Temp))
            {
                for (int i = 0; i < entities.Length; i++)
                {
                    var entity = entities[i];
                    var duration = GetComponent<CTakesDuration>(entity);
                    var active = duration.Active && !duration.IsLocked;

                    if (Require(entity, out CItemProvider cProvider) && Require(entity, out CRefillOnEmpty cRefill))
                        active &= cProvider.Available <= cRefill.MinimumProvided;

                    Set(entity, new CItemHolderPreventTransfer { PreventInsertingInto = active });
                    Set(entity, new CItemProviderPreventTransfer { PreventTransfer = active });
                }
            }
        }
    }
}
