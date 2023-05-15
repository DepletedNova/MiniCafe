namespace MiniCafe.Mains.Tea
{
    public class SageSteeped : CustomItem
    {
        public override string UniqueNameID => "sage_steeped";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Matcha");
    }
}
