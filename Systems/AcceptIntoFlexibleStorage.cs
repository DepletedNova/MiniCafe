namespace MiniCafe.Systems
{
    [UpdateInGroup(typeof(ItemTransferAccept))]
    public class AcceptIntoFlexibleStorage : TransferAcceptSystem, IModSystem
    {
        public EntityQuery proposalQuery;
        protected override void Initialise()
        {
            base.Initialise();
            proposalQuery = GetEntityQuery(new QueryHelper().All(typeof(CItemTransferProposal)));
        }

        public override void AfterLoading()
        {
            base.AfterLoading();

            this.RegisterTransfer();
        }

        public override void AcceptTransfer(Entity proposalEntity, Entity acceptance, EntityContext ctx, out Entity return_item)
        {
            return_item = default(Entity);
            if (!Require<CItemTransferProposal>(proposalEntity, out var proposal) || !Require<CFlexibleStorage>(proposal.Destination, out var storage))
                return;

            storage.ItemSet.Add(proposal.ItemData.ID == storage.Item1 ? 1 : 2);
            ctx.Set(proposal.Destination, storage);
            ctx.Destroy(proposal.Item);
        }

        protected override void OnUpdate()
        {
            using var proposals = proposalQuery.ToEntityArray(Allocator.Temp);

            foreach (var entity in proposals)
            {
                var proposal = GetComponent<CItemTransferProposal>(entity);
                if (proposal.Status == ItemTransferStatus.Pruned || (proposal.Flags & TransferFlags.RequireMerge) != 0 ||
                    !Require<CFlexibleStorage>(proposal.Destination, out var storage) || !Data.TryGet<Item>(proposal.ItemData.ID, out var item))
                    continue;

                if (proposal.ItemData.ID != storage.Item1 && proposal.ItemData.ID != storage.Item2 || storage.ItemSet.Length >= storage.Maximum)
                    continue;

                Accept(entity);
            }
        }
    }
}
