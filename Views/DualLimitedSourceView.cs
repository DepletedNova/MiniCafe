using Kitchen;
using KitchenLib.Utils;
using KitchenMods;
using MessagePack;
using MiniCafe.Components;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace MiniCafe.Views
{
    public class DualLimitedSourceView : UpdatableObjectView<DualLimitedSourceView.ViewData>
    {
        protected override void UpdateData(ViewData data)
        {
            Animator.SetInteger(Index, data.Current);
            if (Items1.IsNullOrEmpty() || Items2.IsNullOrEmpty())
                return;
            var current = data.Current == 1;
            UpdateAmount(current ? data.CurrentAvailable : data.Available1, ref Displayed1, ref Items1);
            UpdateAmount(current ? data.Available2 : data.CurrentAvailable, ref Displayed2, ref Items2);
        }

        private void UpdateAmount(int amount, ref int displayed, ref List<GameObject> items)
        {
            if (amount == displayed)
                return;
            for (int i = 0; i < items.Count; i++)
            {
                items[i].SetActive(i < amount);
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

        [MessagePackObject(false)]
        public struct ViewData : ISpecificViewData, IViewData, IViewResponseData, IViewData.ICheckForChanges<ViewData>
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
