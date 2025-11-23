using KitchenData;
using KitchenMods;
using Unity.Entities;

namespace MiniCafe.Components
{
    public struct CRefillOnEmpty : IApplianceProperty, IModComponent 
    {
        public bool Active;
        public int MinimumProvided;
        public int FillIncrement;
    }

    public struct CRefillOnEmptySurrogate : IApplianceProperty, IModComponent, TypeHash.ISurrogate<CRefillOnEmpty>
    {
        public bool Active;

        public IComponentData Convert() => new CRefillOnEmpty { Active = Active, MinimumProvided = 0, FillIncrement = 1 };
    }

    public struct CRefillOnEmptySurrogate2 : IApplianceProperty, IModComponent, TypeHash.ISurrogate<CRefillOnEmpty>
    {
        public bool Active;
        public int MinimumProvided;

        public IComponentData Convert() => new CRefillOnEmpty { Active = Active, MinimumProvided = MinimumProvided, FillIncrement = 1 };
    }
}
