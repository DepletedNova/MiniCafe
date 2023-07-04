namespace MiniCafe.Systems
{
    [UpdateInGroup(typeof(ApplianceProcessReactionGroup))]
    public class OverfillSpills : GameSystemBase, IModSystem
    {
        private EntityQuery Processors;

        protected override void Initialise()
        {
            base.Initialise();
            Processors = GetEntityQuery(new QueryHelper()
                .All(typeof(COverfills), typeof(CPosition))
                .None(typeof(CMessRequest), typeof(CIsOnFire))
                .Any(typeof(CCompletedProcess), typeof(CTakesDuration)));
        }

        protected override void OnUpdate()
        {
            if (!HasStatus(Main.OVERFILLING_STATUS)) return;

            using var entities = Processors.ToEntityArray(Allocator.Temp);

            foreach (var entity in entities)
            {
                if (Require(entity, out CCompletedProcess process) && process.Process == 0)
                    continue;

                if (Require(entity, out CTakesDuration duration) && (duration.Remaining > 0f || !duration.Active))
                    continue;

                if (Random.value > 0.5f)
                    continue;

                var spills = GetComponent<COverfills>(entity);
                Entity spillEntity = EntityManager.CreateEntity();
                Set<CMessRequest>(spillEntity, new()
                {
                    ID = spills.ID,
                    OverwriteOtherMesses = false
                });
                Set(spillEntity, GetComponent<CPosition>(entity));
            }
        }
    }
}
