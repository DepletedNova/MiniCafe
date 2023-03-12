namespace MiniCafe.Systems
{
    [UpdateInGroup(typeof(CreationGroup))]
    [UpdateBefore(typeof(CreateNewAppliances))]
    internal class AppliancePropertyRegistry : GenericSystemBase, IModSystem
    {
        private void AddProperties(Entity appliance, IApplianceProperty property)
        {
            AddComponentData<CDualLimitedProvider>(appliance, property);
            AddComponentData<CFlexibleStorage>(appliance, property);
            // ...
        }

        private EntityQuery applianceQuery;
        protected override void Initialise()
        {
            base.Initialise();
            applianceQuery = GetEntityQuery(new QueryHelper().All(typeof(CCreateAppliance)));
        }

        protected override void OnUpdate()
        {
            using var appliances = applianceQuery.ToEntityArray(Allocator.Temp);
            foreach (var appliance in appliances)
            {
                int applianceID = EntityManager.GetComponentData<CCreateAppliance>(appliance).ID;
                if (!GameData.Main.TryGet(applianceID, out Appliance gdo))
                    continue;
                foreach (var property in gdo.Properties)
                {
                    AddProperties(appliance, property);
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
