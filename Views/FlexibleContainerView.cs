namespace MiniCafe.Views
{
    public class FlexibleContainerView : UpdatableObjectView<FlexibleContainerView.ViewData>
    {
        protected override void UpdateData(ViewData data)
        {
            for (int i = 0; i < data.Maximum; i++)
            {
                bool useValue = true;
                if (i > data.ItemSet.Length - 1)
                    useValue = false;

                ItemList1[i].SetActive(useValue ? data.ItemSet[i] == 1 : false);
                ItemList2[i].SetActive(useValue ? data.ItemSet[i] == 2 : false);
            }
        }

        public List<GameObject> ItemList1;
        public List<GameObject> ItemList2;

        public class UpdateView : IncrementalViewSystemBase<ViewData>, IModSystem
        {
            private EntityQuery query;
            protected override void Initialise()
            {
                base.Initialise();
                query = GetEntityQuery(new QueryHelper().All(typeof(CLinkedView), typeof(CFlexibleStorage)));
            }

            protected override void OnUpdate()
            {
                using var entities = query.ToEntityArray(Allocator.Temp);
                foreach (var entity in entities)
                {
                    if (!Require<CFlexibleStorage>(entity, out var storage) || !Require<CLinkedView>(entity, out var view))
                        continue;

                    SendUpdate(view, new ViewData()
                    {
                        ItemSet = storage.ItemSet,
                        Maximum = storage.Maximum
                    });
                }
            }
        }

        [MessagePackObject(false)]
        public struct ViewData : ISpecificViewData, IViewData, IViewData.ICheckForChanges<ViewData>
        {
            [Key(0)] public FixedListInt64 ItemSet;
            [Key(1)] public int Maximum;

            public IUpdatableObject GetRelevantSubview(IObjectView view) => view.GetSubView<FlexibleContainerView>();

            public bool IsChangedFrom(ViewData check) => check.ItemSet != ItemSet;
        }
    }
}
