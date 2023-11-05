using HarmonyLib;
using Kitchen;
using MiniCafe.Components;

namespace MiniCafe.Patches
{
    [HarmonyPatch(typeof(TakeFromProvider))]
    static class TakeFromProviderPatch
    {
        [HarmonyPatch("IsPossible")]
        [HarmonyPrefix]
        static bool IsPossible_Prefix(ref InteractionData data, TakeFromProvider __instance) =>
            !data.Context.Require(data.Target, out CItemProviderPreventTransfer cPrevent) || !cPrevent.PreventTransfer;
    }
}
