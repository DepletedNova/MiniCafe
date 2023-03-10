using UnityEngine;

namespace MiniCafe.Systems
{
    [UpdateInGroup(typeof(ItemTransferPropose))]
    public class TakeFromFlexibleStorage : TransferInteractionProposalSystem, IModSystem
    {
        public override void AfterLoading()
        {
            base.AfterLoading();

            this.RegisterTransfer();
        }

        protected override bool AllowActOrGrab => true;

        protected override bool IsPossible(ref InteractionData data)
        {
            var target = data.Target;
            if (Has<CPreventUse>(target) || Has<CPreventItemTransfer>(target) || !Require<CFlexibleStorage>(target, out var storage))
                return false;

            if (storage.Maximum < 1 ||  storage.ItemSet.Length < 1)
                return false;

            var current = storage.ItemSet[storage.ItemSet.Length - 1];

            CInteractionTransferProposal proposal = InteractionTransferProposal(data.Interactor);
            proposal.AllowGrab = true;
            proposal.AllowAct = false;
            Context.Set(TransferProposalSystem.CreateProposal(Context, this, Context.CreateItem(current == 1 ? storage.Item1 : storage.Item2), data.Attempt.Target, data.Interactor,
                TransferFlags.Interaction), proposal);
            return false;
        }

        public override void SendTransfer(Entity transfer, Entity acceptance, EntityContext ctx)
        {
            if (Require<CItemTransferProposal>(transfer, out var proposal) && Require<CFlexibleStorage>(proposal.Source, out var storage) )
            {
                storage.ItemSet.RemoveAtSwapBack(storage.ItemSet.Length - 1);
                SetComponent(proposal.Source, storage);
            }
        }

        public override void ReceiveResult(Entity result, Entity transfer, Entity acceptance, EntityContext ctx) { }

        public override void Tidy(EntityContext ctx, CItemTransferProposal proposal)
        {
            if (proposal.Status != ItemTransferStatus.Resolved)
            {
                ctx.Destroy(proposal.Item);
            }
        }
    }
}
