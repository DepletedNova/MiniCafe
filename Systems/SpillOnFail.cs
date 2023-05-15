namespace MiniCafe.Systems
{
    [UpdateInGroup(typeof(ApplianceProcessReactionGroup))]
    public class SpillOnFail : GameSystemBase, IModSystem
    {
        private EntityQuery Processors;

        protected override void Initialise()
        {
            base.Initialise();
            Processors = GetEntityQuery(new QueryHelper()
                .All(typeof(CSpillsOnFail), typeof(CCompletedProcess), typeof(CPosition))
                .None(typeof(CMessRequest), typeof(CIsOnFire))
                .Any(typeof(CCompletedProcess), typeof(CTakesDuration)));
        }

        protected override void OnUpdate()
        {
            using var entities = Processors.ToEntityArray(Allocator.Temp);

            foreach (var entity in entities)
            {
                var process = GetComponent<CCompletedProcess>(entity);
                var spills = GetComponent<CSpillsOnFail>(entity);

                if (process.Process == 0 || !process.IsBad)
                    continue;

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
