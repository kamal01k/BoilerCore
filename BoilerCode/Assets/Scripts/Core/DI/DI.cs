using UnityEngine;

namespace Core.DI
{
    public static class DI
    {
        public static DIContainer Container { get; } = new DIContainer();

        // If you need initialization, you can still use this attribute
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            // Any global initialization logic here
        }
    }
}
