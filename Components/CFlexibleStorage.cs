using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;

namespace MiniCafe.Components
{
    public struct CFlexibleStorage : IApplianceProperty, IModComponent
    {
        public FixedListInt64 ItemSet;
        public int Maximum;
        public int Item1;
        public int Item2;
    }
}
