namespace MiniCafe.Views
{
    public class LocalLimitedItemSourceView : UpdatableObjectView<LocalLimitedItemSourceView.ViewData>
    {
        protected override void UpdateData(ViewData data)
        {
            if (Items.IsNullOrEmpty())
                return;
            for (int i = 0; i < Items.Count; i++)
                Items[i].SetActive(data.Amount > i);
        }

        public List<GameObject> Items;

        public class UpdateView : IncrementalViewSystemBase<ViewData>, IModSystem
        {
            private EntityQuery query;
            protected override void Initialise()
            {
                query = GetEntityQuery(new QueryHelper().All(typeof(CLinkedView), typeof(CAppliance), typeof(CItemProvider)));
            }

            protected override void OnUpdate()
            {
                using var views = query.ToComponentDataArray<CLinkedView>(Allocator.Temp);
                using var providers = query.ToComponentDataArray<CItemProvider>(Allocator.Temp);

                for (int i = 0; i < views.Length; i++)
                {
                    var provider = providers[i];
                    if (provider.Maximum <= 0)
                        continue;

                    SendUpdate(views[i], new ViewData()
                    {
                        Amount = provider.Available
                    }, MessageType.SpecificViewUpdate);
                }
            }
        }

        [MessagePackObject(false)]
        public struct ViewData : ISpecificViewData, IViewData.ICheckForChanges<ViewData>
        {
            [Key(0)] public int Amount;

            public IUpdatableObject GetRelevantSubview(IObjectView view) => view.GetSubView<LocalLimitedItemSourceView>();

            public bool IsChangedFrom(ViewData check) => true;
        }
    }
}
