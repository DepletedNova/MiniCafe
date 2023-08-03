using Kitchen;
using KitchenData;
using KitchenLib.Utils;
using KitchenMods;
using MiniCafe.Components;
using MiniCafe.Extensions;
using System.Linq;
using Unity.Collections;
using Unity.Entities;

namespace MiniCafe.Systems
{
    [UpdateInGroup(typeof(CreationGroup))]
    [UpdateBefore(typeof(CreateNewAppliances))]
    public class InvertPlacement : GenericSystemBase, IModSystem
    {
        private EntityQuery applianceQuery;
        protected override void Initialise()
        {
            applianceQuery = GetEntityQuery(new QueryHelper().All(typeof(CCreateAppliance), typeof(CPosition)));
        }

        protected override void OnUpdate()
        {
            using var entities = applianceQuery.ToEntityArray(Allocator.Temp);
            
            foreach (var entity in entities)
            {
                var createAppliance = GetComponent<CCreateAppliance>(entity);
                if (!GameData.Main.TryGet<Appliance>(createAppliance.ID, out var appliance))
                    continue;

                if (appliance.Properties.OfType<CInvertedPlacement>().IsNullOrEmpty())
                    continue;

                Set(entity, GetComponent<CPosition>(entity).Flip());
            }
        }
    }
}
