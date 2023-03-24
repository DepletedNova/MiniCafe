namespace MiniCafe.Mains.Tea
{
    public class EarlGreySteeped : CustomItem
    {
        public override string UniqueNameID => "earl_grey_steeped";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Earl Grey");
    }
}
