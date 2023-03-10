namespace MiniCafe.Extensions
{
    public static class SystemExtensions
    {
        private static List<GenericSystemBase> registeredSystems = new();

        private static FieldInfo AcceptTransfersInfo = ReflectionUtils.GetField<ResolveTransfers>("AcceptTransfers");
        private static FieldInfo SendTransfersInfo = ReflectionUtils.GetField<ResolveTransfers>("SendTransfers");
        public static bool RegisterTransfer<T>(this T system) where T : GenericSystemBase
        {
            if (registeredSystems.Contains(system)) return true;
            try
            {
                ResolveTransfers transfers = system.World.GetExistingSystem<ResolveTransfers>();
                if (system is ISendTransfers send)
                {
                    var sendTransfers = SendTransfersInfo.GetValue(transfers) as Dictionary<SystemReference, ISendTransfers>;
                    sendTransfers.Add(system, send);
                    SendTransfersInfo.SetValue(transfers, sendTransfers);
                }
                else if (system is IAcceptTransfers accept)
                {
                    var acceptTransfers = AcceptTransfersInfo.GetValue(transfers) as Dictionary<SystemReference, IAcceptTransfers>;
                    acceptTransfers.Add(system, accept);
                    AcceptTransfersInfo.SetValue(transfers, acceptTransfers);
                }

                registeredSystems.Add(system);
                return true;
            } catch
            {
                return false;
            }
        }
    }
}
