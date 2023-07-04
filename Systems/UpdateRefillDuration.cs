namespace MiniCafe.Systems
{
    [UpdateInGroup(typeof(DurationLocks))]
    public class UpdateRefillDuration : GameSystemBase, IModSystem
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
                var cRefill = GetComponent<CRefillOnEmpty>(entity);

                if (Has<SIsNightTime>())
                {
                    cDuration.IsLocked = true;
                    cDuration.Remaining = cDuration.Total;
                    Set(entity, cDuration);

                    Set(entity, new CRefillOnEmpty());

                    continue;
                }

                var cProvider = GetComponent<CItemProvider>(entity);

                if (cProvider.Available <= 0)
                {
                    cDuration.IsLocked = false;
                    Set(entity, cDuration);

                    cRefill.Active = true;
                    Set(entity, cRefill);
                    continue;
                }

                if (cProvider.Available >= cProvider.Maximum || !cRefill.Active)
                {
                    cDuration.IsLocked = true;
                    Set(entity, cDuration);
                }
            }
        }
    }
}
