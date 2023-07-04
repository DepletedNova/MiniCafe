namespace MiniCafe.Coffee
{
    public class OverfillingCard : CustomUnlockCard
    {
        public override string UniqueNameID => "overfill_card";
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Imperfect Brewing", "Cups have a 50% chance to overfill!", ""))
        };
        public override List<UnlockEffect> Effects => new()
        {
            new StatusEffect
            {
                Status = Main.OVERFILLING_STATUS
            }
        };
        public override List<Unlock> HardcodedRequirements => new()
        {
            GetGDO<Unlock>(CoffeeshopMode)
        };

        public override string IconOverride => "<sprite name=\"fill_coffee\">";
        public override Color ColourOverride => ColorFromHex(0x6D5140);
    }
}
