namespace MiniCafe.Systems
{
    [UpdateAfter(typeof(GrantNecessaryAppliances))]
    internal class GrantNecessaryKettles : NightSystem, IModSystem
    {
        private EntityQuery Unlocks;
        private EntityQuery CreateAppliances;
        private EntityQuery Providers;
        private EntityQuery Parcels;

        protected override void Initialise()
        {
            base.Initialise();
            Unlocks = GetEntityQuery(new QueryHelper().All(typeof(CProgressionUnlock)));
            CreateAppliances = GetEntityQuery(new QueryHelper().All(typeof(CCreateAppliance)));
            Providers = GetEntityQuery(new QueryHelper().All(typeof(CAppliance)).Any(typeof(CItemProvider)));
            Parcels = GetEntityQuery(new QueryHelper().All(typeof(CLetterAppliance)));
        }

        protected override void OnUpdate()
        {
            if (!CreateAppliances.IsEmpty)
                return;

            using var providers = Providers.ToComponentDataArray<CAppliance>(Allocator.Temp);
            using var parcels = Parcels.ToComponentDataArray<CLetterAppliance>(Allocator.Temp);
            using var unlocks = Unlocks.ToComponentDataArray<CProgressionUnlock>(Allocator.Temp);

            var countedKettles = 0;
            foreach (var appliance in providers)
            {
                if (appliance.ID == KettleStand.KettleID)
                    countedKettles += 2;
            }
            foreach (var parcel in parcels)
            {
                if (parcel.ApplianceID == KettleStand.KettleID)
                    countedKettles += 2;
            }

            var neededKettles = 0;
            foreach (var unlock in unlocks)
            {
                if (unlock.Type != CardType.Default)
                    continue;

                if (GameData.Main.TryGet<Dish>(unlock.ID, out var dish) && dish.MinimumIngredients.Any(item => item.ID == Kettle.KettleID))
                    neededKettles++;
            }

            int offset = 0;
            if (neededKettles > countedKettles)
            {
                var postTiles = GetPostTiles();
                var parcelTile = GetParcelTile(postTiles, ref offset);
                PostHelpers.CreateApplianceParcel(EntityManager, parcelTile, KettleStand.KettleID);
            }
        }

        private Vector3 GetParcelTile(List<Vector3> tiles, ref int offset)
        {
            Vector3 vector = Vector3.zero;
            bool flag = false;
            while (!flag && offset < tiles.Count)
            {
                int num = offset;
                offset = num + 1;
                vector = tiles[num];
                flag |= GetOccupant(vector, OccupancyLayer.Default) == default(Entity) && !GetTile(vector).HasFeature;
            }
            return flag ? vector : GetFallbackTile();
        }
    }
}
