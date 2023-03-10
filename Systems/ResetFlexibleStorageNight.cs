namespace MiniCafe.Systems
{
    internal class ResetFlexibleStorageNight : StartOfNightSystem, IModSystem
    {
        private EntityQuery Query;
        protected override void Initialise()
        {
            base.Initialise();
            Query = GetEntityQuery(new QueryHelper().All(typeof(CFlexibleStorage)));
        }

        protected override void OnUpdate()
        {
            using var providers = Query.ToEntityArray(Allocator.TempJob);
            foreach (var provider in providers)
            {
                var flexibleStorage = EntityManager.GetComponentData<CFlexibleStorage>(provider);

                flexibleStorage.ItemSet.Clear();
                for (int i = 0; i < flexibleStorage.Maximum; i++)
                    flexibleStorage.ItemSet[i] = 0;

                EntityManager.SetComponentData(provider, flexibleStorage);
            }
        }
    }
}
