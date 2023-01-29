using UnityEngine;

namespace MiniCafe.Appliances
{
    // Appliance
    public class MugCabinet : CustomAppliance
    {
        public static int ApplianceID { get; private set; }

        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Mug Cabinet");

        public override string UniqueNameID => "mug_provider";
        public override string Name => "Mugs";
        public override bool IsPurchasable => true;
        public override bool SellOnlyAsDuplicate => true;
        public override PriceTier PriceTier => PriceTier.Cheap;
        public override ShoppingTags ShoppingTags => ShoppingTags.Cooking | ShoppingTags.Basic;

        public override List<IApplianceProperty> Properties => new()
        {
            GetCItemProvider(SmallMug.ItemID, 6, 6, false, false, false, false, true, true, false),
        };

        public override void AttachDependentProperties(GameData gameData, GameDataObject gdo)
        {
            base.AttachDependentProperties(gameData, gdo);
            ApplianceID = gdo.ID;
        }

        public override void OnRegister(GameDataObject gdo)
        {
            gdo.name = "Mug Cabinet";
            var cabinet = Prefab.GetChild("Cabinet");
            var bigMugs = cabinet.GetChild("Large Mugs");
            var smallMugs = cabinet.GetChild("Small Mugs");

            // View
            if (!Prefab.HasComponent<DualLimitedSourceView>())
            {
                var sourceView = Prefab.AddComponent<DualLimitedSourceView>();

                for (int i = 0; i < smallMugs.GetChildCount(); i++)
                    sourceView.Items1.Add(smallMugs.GetChild(i).gameObject);

                for (int i = 0; i < bigMugs.GetChildCount(); i++)
                    sourceView.Items2.Add(bigMugs.GetChild(i).gameObject);

                sourceView.Animator = Prefab.GetComponent<Animator>();
            }

            // Materials
            GameObject parent = Prefab.GetChildFromPath("Block/Counter2");
            var paintedWood = GetMaterialArray("Wood 4 - Painted");
            var defaultWood = GetMaterialArray("Wood - Default");
            parent.ApplyMaterialToChild("Counter", paintedWood);
            parent.ApplyMaterialToChild("Counter Doors", paintedWood);
            parent.ApplyMaterialToChild("Counter Surface", defaultWood);
            parent.ApplyMaterialToChild("Counter Top", defaultWood);
            parent.ApplyMaterialToChild("Handles", "Knob");

            for (int i = 0; i < smallMugs.GetChildCount(); i++)
                SmallMug.ApplyMugMaterials(smallMugs.GetChild(i));
            for (int i = 0; i < bigMugs.GetChildCount(); i++)
                BigMug.ApplyMugMaterials(bigMugs.GetChild(i));

            cabinet.ApplyMaterialToChild("cabinet", "Wood");
            cabinet.ApplyMaterialToChild("door", "Door Glass", "Wood", "Metal");
        }
    }

    // Component
    public struct CDualLimitedProvider : IApplianceProperty, IAttachableProperty, IComponentData, IModComponent
    {
        public int Provided { get => Current == 1 ? Provide1 : Provide2; }
        public int Current;

        public int Provide1;
        public int Available1;

        public int Provide2;
        public int Available2;
    }

    // Systems
    public class DualLimitedProviderSwitch : ItemInteractionSystem, IModSystem
    {
        protected override bool IsPossible(ref InteractionData data) => Require(data.Target, out LimitedProvider) && Require(data.Target, out ItemProvider);

        protected override void Perform(ref InteractionData data)
        {
            if (LimitedProvider.Current == 1)
            {
                LimitedProvider.Available1 = ItemProvider.Available;
                ItemProvider.Available = LimitedProvider.Available2;
            } else {
                LimitedProvider.Available2 = ItemProvider.Available;
                ItemProvider.Available = LimitedProvider.Available1;
            }
            LimitedProvider.Current = (LimitedProvider.Current + 1) % 2;
            SetComponent(data.Target, LimitedProvider);
            ItemProvider.SetAsItem(LimitedProvider.Provided);
            SetComponent(data.Target, ItemProvider);
        }

        private CDualLimitedProvider LimitedProvider;
        private CItemProvider ItemProvider;
    }
    public class DualLimitedProviderRegistry : GenericSystemBase, IModSystem
    {
        private EntityQuery Query;
        protected override void Initialise()
        {
            base.Initialise();
            Query = GetEntityQuery(new QueryHelper().All(typeof(CAppliance), typeof(CItemProvider)).None(typeof(CDualLimitedProvider)));
        }

        protected override void OnUpdate()
        {
            using var appliances = Query.ToEntityArray(Allocator.TempJob);
            foreach (var appliance in appliances)
            {
                int id = EntityManager.GetComponentData<CAppliance>(appliance).ID;
                if (id == MugCabinet.ApplianceID)
                {
                    EntityManager.AddComponentData(appliance, new CDualLimitedProvider()
                    {
                        Current = 1,
                        Provide1 = SmallMug.ItemID,
                        Available1 = 6,
                        Provide2 = BigMug.ItemID,
                        Available2 = 6,
                    });
                }
            }
        }

    }

    // View
    public class DualLimitedSourceView : UpdatableObjectView<DualLimitedSourceView.ViewData>
    {
        protected override void UpdateData(ViewData data)
        {
            Animator.SetInteger(Index, data.Current);
            if (Items1 == null || Items2 == null)
                return;
            var current = data.Current == 1;
            UpdateAmount(current ? data.CurrentAvailable : data.Available1, ref Displayed1, ref Items1);
            UpdateAmount(current ? data.Available2 : data.CurrentAvailable, ref Displayed2, ref Items2);
        }

        private void UpdateAmount(int amount, ref int displayed, ref List<GameObject> items)
        {
            Debug.Log(amount + " : " + displayed);
            if (amount == displayed)
                return;
            for (int i = 0; i < items.Count; i++)
            {
                items[i].SetActive(i < amount);
                Debug.Log(items[i].name);
            }
            displayed = amount;
        }

        public int Displayed1 = -1;
        public List<GameObject> Items1 = new();

        public int Displayed2 = -1;
        public List<GameObject> Items2 = new();

        public Animator Animator;
        private static readonly int Index = Animator.StringToHash("Index");

        public class UpdateView : IncrementalViewSystemBase<ViewData>, IModSystem
        {
            private EntityQuery query;
            protected override void Initialise()
            {
                base.Initialise();
                query = GetEntityQuery(new QueryHelper().All(typeof(CLinkedView), typeof(CDualLimitedProvider)));
            }

            protected override void OnUpdate()
            {
                using var views = query.ToComponentDataArray<CLinkedView>(Allocator.Temp);
                using var limitedComponents = query.ToComponentDataArray<CDualLimitedProvider>(Allocator.Temp);
                using var providerComponents = query.ToComponentDataArray<CItemProvider>(Allocator.Temp);

                for (var i = 0; i < views.Length; i++)
                {
                    var view = views[i];
                    var limitedData = limitedComponents[i];
                    var providerData = providerComponents[i];

                    SendUpdate(view, new ViewData
                    {
                        Current = limitedData.Current,
                        Available1 = limitedData.Available1,
                        Available2 = limitedData.Available2,
                        CurrentAvailable = providerData.Available
                    }, MessageType.SpecificViewUpdate);
                }
            }
        }

        public struct ViewData : ISpecificViewData, IViewData.ICheckForChanges<ViewData>
        {
            [Key(0)] public int Current;
            [Key(1)] public int Available1;
            [Key(2)] public int Available2;
            [Key(3)] public int CurrentAvailable;

            public IUpdatableObject GetRelevantSubview(IObjectView view) => view.GetSubView<DualLimitedSourceView>();

            public bool IsChangedFrom(ViewData check) => check.Current != Current || check.Available1 != Available1 || check.Available2 != Available2 || check.CurrentAvailable != CurrentAvailable;
        }
    }
}
