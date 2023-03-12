namespace MiniCafe.Systems
{
    [UpdateInGroup(typeof(InteractionGroup))]
    public class DualLimitedProviderSwitch : ItemInteractionSystem, IModSystem
    {
        protected override bool IsPossible(ref InteractionData data) => Require(data.Target, out LimitedProvider) && Require(data.Target, out ItemProvider);

        protected override void Perform(ref InteractionData data)
        {
            if (LimitedProvider.Current == 1)
            {
                LimitedProvider.Available1 = ItemProvider.Available;
                ItemProvider.Available = LimitedProvider.Available2;
            }
            else
            {
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
}
