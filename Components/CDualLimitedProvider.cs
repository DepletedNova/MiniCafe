using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniCafe.Components
{
    public struct CDualLimitedProvider : IApplianceProperty, IAttachableProperty, IComponentData, IModComponent
    {
        public int Provided { get => Current == 1 ? Provide1 : Provide2; }
        public int Current;

        public int Provide1;
        public int Available1;
        public int Maximum1;

        public int Provide2;
        public int Available2;
        public int Maximum2;
    }
}
