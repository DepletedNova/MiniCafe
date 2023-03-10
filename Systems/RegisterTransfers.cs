using UnityEngine;

namespace MiniCafe.Systems
{
    public class RegisterTransfers : GenericSystemBase, IModSystem
    {
        public static void AddTransfer<T>(T system) where T : GenericSystemBase => UnregisteredTransfers.Add(system);
        private static List<GenericSystemBase> UnregisteredTransfers = new();

        private static FieldInfo AcceptTransfersInfo = ReflectionUtils.GetField<ResolveTransfers>("AcceptTransfers");
        private static FieldInfo SendTransfersInfo = ReflectionUtils.GetField<ResolveTransfers>("SendTransfers");
        protected override void OnUpdate()
        {
            // Ignore update if registry is unresolved
            if (World == null || World.GetExistingSystem<ResolveTransfers>() == null)
                return;

            // Disable if unactive
            if (UnregisteredTransfers.IsNullOrEmpty())
            {
                Enabled = false;
                return;
            }

            ResolveTransfers transferResolve = World.GetExistingSystem<ResolveTransfers>();
            foreach (var transfer in UnregisteredTransfers)
            {
                if (transfer is ISendTransfers sender)
                {
                    var sendTransfers = SendTransfersInfo.GetValue(transferResolve) as Dictionary<SystemReference, ISendTransfers>;
                    sendTransfers.Add(transfer, sender);
                    SendTransfersInfo.SetValue(transferResolve, sendTransfers);
                }
                else if (transfer is IAcceptTransfers accepter)
                {
                    var acceptTransfers = AcceptTransfersInfo.GetValue(transferResolve) as Dictionary<SystemReference, IAcceptTransfers>;
                    acceptTransfers.Add(transfer, accepter);
                    AcceptTransfersInfo.SetValue(transferResolve, acceptTransfers);
                }
                UnregisteredTransfers.Remove(transfer);
            }
        }

    }
}
