using TMPro;

namespace MiniCafe
{
    internal static class Utilities
    {
        public static LocalisationObject<UnlockInfo> CreateUnlockLocalisation(params (Locale locale, string name, string desc, string flavour)[] localisations)
        {
            var info = new LocalisationObject<UnlockInfo>();
            foreach (var item in localisations)
            {
                var obj = ScriptableObject.CreateInstance<UnlockInfo>();
                obj.name = item.name;
                obj.Name = item.name;
                obj.Description = item.desc;
                obj.FlavourText = item.flavour;
                info.Add(item.locale, obj);
            }
            return info;
        }
    }
}
