namespace MiniCafe.Extensions
{
    public static class CPositionExtensions
    {
        public static CPosition Flip(this CPosition position)
        {
            var newRot = OrientationHelpers.Flip(OrientationHelpers.ToOrientation(position.Rotation));
            position.Rotation = OrientationHelpers.ToRotation(newRot);
            return position;
        }
    }
}
