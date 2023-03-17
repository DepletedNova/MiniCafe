using HarmonyLib;
using MiniCafe.Systems;

namespace MiniCafe.Patches
{
    [HarmonyPatch(typeof(GrantNecessaryAppliances))]
    internal class GrantNecessaryAppliances_Patch
    {
        [HarmonyPatch("TotalPlates")]
        [HarmonyPostfix]
        internal static void TotalPlates_Postfix(ref int __result, Dictionary<int, int> ___ProvidersOfType)
        {
            if (GrantNecessaryMugs.HasOnlyMugs())
                __result = 99;
        }
    }
}
