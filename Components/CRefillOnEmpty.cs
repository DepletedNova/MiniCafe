using KitchenData;
using KitchenMods;
using Unity.Entities;

namespace MiniCafe.Components
{
    public struct CRefillOnEmpty : IApplianceProperty, IModComponent 
    {
        public bool Active;
        public int MinimumProvided;
    }

    public struct CRefillOnEmptySurrogate : IApplianceProperty, IModComponent, TypeHash.ISurrogate<CRefillOnEmpty>
    {
        public bool Active;

        public IComponentData Convert() => new CRefillOnEmpty { Active = Active, MinimumProvided = 0 };
    }
}
