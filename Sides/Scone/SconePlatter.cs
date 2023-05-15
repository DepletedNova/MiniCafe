namespace MiniCafe.Extras
{
    public class SconePlatter : CustomItem
    {
        public override string UniqueNameID => "scone_platter";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Scone Platter");
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override int SplitCount => 7;
        public override Item SplitSubItem => GetCastedGDO<Item, Scone>();
        public override List<Item> SplitDepletedItems => new() { GetCastedGDO<Item, Scone>() };

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Platter", "Plate", "Plate - Ring", "Plastic - Blue");
            var scones = Prefab.GetChild("Scones");
            var sconeList = new List<GameObject>();
            for (int i = 0; i < scones.GetChildCount(); i++)
            {
                var scone = scones.GetChild(i);
                scone.ApplyMaterialCafe("Bread - Inside Cooked", "Chocolate");
                if (i > 0)
                    sconeList.Add(scone);
            }

            ReflectionUtils.GetField<ObjectsSplittableView>("Objects").SetValue(Prefab.AddComponent<ObjectsSplittableView>(), sconeList);
        }
    }
}
