using Kitchen;
using KitchenMods;
using MessagePack;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace MiniCafe.Views
{
    public class ItemProcessView : UpdatableObjectView<ItemProcessView.ViewData>
    {
        public List<GameObject> Objects = new();
        protected override void UpdateData(ViewData data)
        {
            foreach (var go in Objects)
            {
                go.SetActive(data.UndergoingProcess);
            }
        }

        private class UpdateView : IncrementalViewSystemBase<ViewData>, IModSystem
        {
            private EntityQuery Items;
            protected override void Initialise()
            {
                Items = GetEntityQuery(new QueryHelper().All(typeof(CLinkedView), typeof(CItem)));
            }

            protected override void OnUpdate()
            {
                using var entities = Items.ToEntityArray(Allocator.Temp);
                foreach (var entity in entities)
                {
                    SendUpdate(GetComponent<CLinkedView>(entity), new()
                    {
                        UndergoingProcess = HasComponent<CItemUndergoingProcess>(entity)
                    });
                }
            }
        }

        [MessagePackObject(false)]
        public struct ViewData : ISpecificViewData, IViewData.ICheckForChanges<ViewData>
        {
            [Key(0)] public bool UndergoingProcess;

            public IUpdatableObject GetRelevantSubview(IObjectView view) => view.GetSubView<ItemProcessView>();

            public bool IsChangedFrom(ViewData check) => UndergoingProcess != check.UndergoingProcess;
        }
    }
}
