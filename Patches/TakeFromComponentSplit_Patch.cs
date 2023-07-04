using HarmonyLib;

namespace MiniCafe.Patches
{
    [HarmonyPatch(typeof(TakeFromComponentSplit))]
    public class TakeFromComponentSplit_Patch
    {
        [HarmonyPatch(nameof(TakeFromComponentSplit.SendTransfer))]
        [HarmonyPrefix]
        internal static bool SendTransfer_Prefix(Entity transfer, Entity acceptance, EntityContext ctx)
        {
            if (!ctx.Require<CItemTransferProposal>(transfer, out var proposal) ||
                !ctx.Require<CItemHolder>(proposal.Source, out var holder) ||
                !ctx.Require<CSplittableItem>(holder, out var splittable))
                return false;

            splittable.RemainingCount--;
            if (splittable.RemainingCount <= 0)
            {
                Entity entity = ctx.CreateItem(ctx.Require<CComponentSplitDepleted>(holder, out var depleted) ? depleted.DepletionItem : splittable.SplitByComponentsHolder);
                ctx.Destroy(holder);
                ctx.Set(entity, new CHeldBy
                {
                    Holder = proposal.Source
                });
                ctx.Set(proposal.Source, new CItemHolder
                {
                    HeldItem = entity
                });
            }
            else
            {
                ctx.Set(holder, splittable);
                ctx.Remove<CItemUndergoingProcess>(holder);
            }

            return false;
        }
    }
}
