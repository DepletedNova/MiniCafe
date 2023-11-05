using KitchenData;
using KitchenMods;

namespace MiniCafe.Components
{
    public struct CItemProviderPreventTransfer : IApplianceProperty, IModComponent
    {
        public bool PreventTransfer;
    }
}
