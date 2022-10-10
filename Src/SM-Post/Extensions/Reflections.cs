namespace Extensions;

using System.Reflection;

public static class Reflections
{
    public static List<Type> FindAllDerivedTypes<T>()
    {
        if (Assembly.GetAssembly(typeof(T)) is null)
        {
            throw new NullReferenceException($"The Type {nameof(T)} does not exist");
        }

        return FindAllDerivedTypes<T>(Assembly.GetAssembly(typeof(T))!);
    }

    public static List<Type> FindAllDerivedTypes<T>(Assembly assembly)
    {
        var baseType = typeof(T);
        return assembly
            .GetTypes()
            .Where(t =>
                t != baseType &&
                baseType.IsAssignableFrom(t)
                ).ToList();
    }
}
