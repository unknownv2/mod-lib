namespace NoMod.ModUI
{
    public static class ValueCollectionExtensions
    {
        public static bool Toggle(this IValueCollection values, string name)
        {
            return values[name] = !values[name];
        }
    }
}
