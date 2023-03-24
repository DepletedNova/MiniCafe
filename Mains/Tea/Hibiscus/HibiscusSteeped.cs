namespace MiniCafe.Mains.Tea
{
    public class HibiscusSteeped : CustomItem
    {
        public override string UniqueNameID => "hibiscus_steeped";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Hibiscus");
    }
}
