using KitchenData;

namespace MiniCafe.Coffee.Large
{
    internal class LargeCup : CustomItem
    {
        public override string UniqueNameID => "large_cup";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Large Cup");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override Appliance DedicatedProvider => GetCastedGDO<Appliance, LargeCoffeeMachine>();

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Process = GetGDO<Process>(ProcessReferences.FillCoffee),
                Duration = 4,
                Result = GetCastedGDO<Item, LargeCoffee>()
            }
        };

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("cup", "Light Coffee Cup");
        }
    }
}
