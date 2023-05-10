namespace MiniCafe.Sides
{
    internal class WafelMix : CustomItemGroup<WafelMix.View>
    {
        public override string UniqueNameID => "wafel_mix";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Wafel Mix");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Process = GetGDO<Process>(ProcessReferences.Knead),
                Duration = 1.3f,
                Result = GetCastedGDO<Item, UncookedWafelTray>()
            }
        };
        public override List<ItemGroupView.ColourBlindLabel> Labels => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, Butter>(),
                Text = "Bu"
            }
        };
        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetGDO<Item>(ItemReferences.Flour),
                    GetCastedGDO<Item, Cinnamon>(),
                },
                Max = 2,
                Min = 2,
                IsMandatory = true
            },
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, Butter>()
                },
                Max = 1,
                Min = 1
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Bowl", "Metal");
            Prefab.ApplyMaterialToChildCafe("Mix", "Flour", "Cinnamon");
            Prefab.ApplyMaterialToChildCafe("Butter", "Butter");

            Prefab.GetComponent<View>().Setup(gdo);
        }

        public class View : AccessedItemGroupView
        {
            protected override List<ComponentGroup> groups => new()
            {
                new()
                {
                    GameObject = gameObject.GetChild("Butter"),
                    Item = GetCastedGDO<Item, Butter>()
                }
            };
        }
    }
}
