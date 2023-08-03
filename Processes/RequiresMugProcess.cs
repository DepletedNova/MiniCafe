using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using MiniCafe.Appliances;
using System.Collections.Generic;
using static KitchenLib.Utils.GDOUtils;

namespace MiniCafe.Processes
{
    public class RequiresMugProcess : CustomProcess
    {
        public static int StaticID { get; private set; }

        public override string UniqueNameID => "requires_mug";
        public override bool CanObfuscateProgress => true;
        public override int EnablingApplianceCount => 1;
        public override GameDataObject BasicEnablingAppliance => GetCustomGameDataObject<MugCabinet>().GameDataObject;
        public override List<(Locale, ProcessInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateProcessInfo("Requires Mug", "?"))
        };

        public override void AttachDependentProperties(GameData gameData, GameDataObject gdo)
        {
            base.AttachDependentProperties(gameData, gdo);
            StaticID = gdo.ID;
        }
    }
}
