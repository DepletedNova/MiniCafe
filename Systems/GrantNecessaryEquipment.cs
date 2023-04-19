using MiniCafe.Items;
using System;

namespace MiniCafe.Systems
{
    [UpdateBefore(typeof(GrantNecessaryAppliances))]
    internal class GrantNecessaryEquipment : NightSystem, IModSystem
    {
        private static EntityQuery Unlocks;
        private EntityQuery CreateAppliances;
        private EntityQuery Providers;
        private EntityQuery Parcels;
        protected override void Initialise()
        {
            Unlocks = GetEntityQuery(new QueryHelper().All(typeof(CProgressionUnlock)));
            CreateAppliances = GetEntityQuery(new QueryHelper().All(typeof(CCreateAppliance)));
            Providers = GetEntityQuery(new QueryHelper().All(typeof(CAppliance)).Any(typeof(CDualLimitedProvider), typeof(CItemProvider)));
            Parcels = GetEntityQuery(new QueryHelper().All(typeof(CLetterAppliance)));
        }

        protected override void OnUpdate()
        {
            if (!CreateAppliances.IsEmpty)
                return;

            var orDef = GetOrDefault<SKitchenParameters>();
            int maxSize = orDef.Parameters.MaximumGroupSize;
            int offset = 0;
            SetupProviders();
            if (GetMinMugCount() < maxSize)
            {
                var postTiles = GetPostTiles(false);
                var parcelTile = GetParcelTile(postTiles, ref offset);
                if (GameData.Main.TryGet<Item>(SmallMug.ItemID, out var item, false) && item.DedicatedProvider != null)
                    PostHelpers.CreateApplianceParcel(EntityManager, parcelTile, item.DedicatedProvider.ID);
            }
            if (!HasCorrectKettles())
            {
                var postTiles = GetPostTiles(false);
                var parcelTile = GetParcelTile(postTiles, ref offset);
                if (GameData.Main.TryGet<Item>(GetCustomGameDataObject<Kettle>().ID, out var item, false) && item.DedicatedProvider != null)
                    PostHelpers.CreateApplianceParcel(EntityManager, parcelTile, item.DedicatedProvider.ID);
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

        private Dictionary<int, int> ProvidersOfType = new();
        private void SetupProviders()
        {
            using var providers = Providers.ToComponentDataArray<CAppliance>(Allocator.Temp);
            using var letters = Parcels.ToComponentDataArray<CLetterAppliance>(Allocator.Temp);

            ProvidersOfType.Clear();
            foreach (var appliance in providers)
            {
                if (!ProvidersOfType.TryGetValue(appliance.ID, out int current))
                    ProvidersOfType[appliance.ID] = 0;
                ProvidersOfType[appliance.ID]++;
            }
            foreach (var letter in letters)
            {
                if (!ProvidersOfType.TryGetValue(letter.ApplianceID, out int current))
                    ProvidersOfType[letter.ApplianceID] = 0;
                ProvidersOfType[letter.ApplianceID]++;
            }
        }

        private int GetMinMugCount()
        {
            if (!HasMugs())
                return 99;

            int smallMug = 0;
            int largeMug = 0;
            foreach (var pair in ProvidersOfType)
            {
                if (pair.Value <= 0 || !GameData.Main.TryGet<Appliance>(pair.Key, out var appliance, false))
                    continue;

                if (appliance.GetProperty<CDualLimitedProvider>(out var dualLimited))
                {
                    CheckID(dualLimited.Provide1, dualLimited.Maximum1, pair.Value, ref smallMug, ref largeMug);
                    CheckID(dualLimited.Provide2, dualLimited.Maximum2, pair.Value, ref smallMug, ref largeMug);
                }
                else if (appliance.GetProperty<CItemProvider>(out var itemProvider))
                {
                    CheckID(itemProvider.DefaultProvidedItem, itemProvider.Maximum, pair.Value, ref smallMug, ref largeMug);
                }
            }
            return Math.Min(smallMug, largeMug);
        }

        private bool HasCorrectKettles()
        {
            using var unlocks = Unlocks.ToComponentDataArray<CProgressionUnlock>(Allocator.Temp);
            int neededKettles = 0;
            int kettleID = GetCustomGameDataObject<Kettle>().ID;
            foreach (var unlock in unlocks)
            {
                if (unlock.Type != CardType.Default)
                    continue;

                if (MinimumHasItem(unlock.ID, kettleID))
                    neededKettles++;
            }

            int currentKettles = 0;
            foreach (var pair in ProvidersOfType)
            {
                if (pair.Value <= 0 || !GameData.Main.TryGet<Appliance>(pair.Key, out var appliance, false))
                    continue;

                if (appliance.GetProperty<CItemProvider>(out var itemProvider))
                {
                    if (itemProvider.DefaultProvidedItem == kettleID)
                        currentKettles += itemProvider.Maximum * pair.Value;
                }
            }
            return currentKettles >= neededKettles;
        }

        private void CheckID(int id, int max, int amount, ref int smallCurrent, ref int largeCurrent)
        {
            if (id == BigMug.ItemID)
                largeCurrent += max * amount;
            if (id == SmallMug.ItemID)
                smallCurrent += max * amount;
        }

        public static bool HasMugs()
        {
            using var unlocks = Unlocks.ToComponentDataArray<CProgressionUnlock>(Allocator.Temp);
            foreach (var unlock in unlocks)
            {
                if (unlock.Type != CardType.Default)
                    continue;

                if (HasProcess(unlock.ID, RequiresMugProcess.StaticID))
                    return true;
            }
            return false;
        }

        public static bool HasOnlyMugs()
        {
            using var unlocks = Unlocks.ToComponentDataArray<CProgressionUnlock>(Allocator.Temp);
            bool hasOther = false;
            bool hasEspresso = false;
            foreach (var unlock in unlocks)
            {
                if (hasEspresso && hasOther)
                    break;

                if (unlock.Type != CardType.Default)
                    continue;

                if (MinimumHasItem(unlock.ID, ItemReferences.Plate))
                {
                    hasOther = true;
                    continue;
                }

                if (HasProcess(unlock.ID, RequiresMugProcess.StaticID))
                    hasEspresso = true;
            }
            return !hasOther && hasEspresso;
        }

        private static bool MinimumHasItem(int id, int item_id)
        {
            if (!GameData.Main.TryGet<Dish>(id, out var dish, false))
                return false;

            return dish.MinimumIngredients.Any(item => item.ID == item_id);
        }

        private static bool HasProcess(int id, int process_id)
        {
            if (!GameData.Main.TryGet<Dish>(id, out var dish, false))
                return false;

            return dish.RequiredProcesses.Any(process => process.ID == process_id);
        }
    }
}
