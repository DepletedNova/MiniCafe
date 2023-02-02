namespace MiniCafe
{
    [UpdateInGroup(typeof(CreationGroup))]
    internal class AppliancePropertyRegistry : GenericSystemBase, IModSystem
    {
        private EntityQuery applianceQuery;
        protected override void Initialise()
        {
            base.Initialise();
            applianceQuery = GetEntityQuery(new QueryHelper().All(typeof(CCreateAppliance)));
        }

        protected override void OnUpdate()
        {
            using var appliances = applianceQuery.ToEntityArray(Allocator.TempJob);
            foreach (var appliance in appliances)
            {
                int applianceID = EntityManager.GetComponentData<CCreateAppliance>(appliance).ID;
                if (!GameData.Main.TryGet(applianceID, out Appliance gdo))
                    continue;
                foreach (var property in gdo.Properties)
                {
                    AddComponentData<CDualLimitedProvider>(appliance, property);
                }
            }
        }

        private void AddComponentData<T>(Entity appliance, IApplianceProperty property) where T : struct, IApplianceProperty
        {
            if (property == null || !(property is T setProperty))
                return;
            EntityManager.AddComponentData(appliance, setProperty);
        }
    }
}
