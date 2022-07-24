using UnityEngine;

namespace Redcode.Moroutines
{
    internal sealed class MoroutinesExecuter : MonoBehaviour
    {
        internal static MoroutinesExecuter Instance { get; private set; }

        internal MoroutineOwner Owner { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void CreateInstance()
        {
            Instance = new GameObject("[Redcode] MoroutinesExecuter").AddComponent<MoroutinesExecuter>();
            Instance.Owner = Instance.gameObject.AddComponent<MoroutineOwner>();

            var settings = Resources.Load<MoroutinesSettings>("Redcode/Moroutines/Settings");
            if (settings.HideMoroutinesExecuter)
                Instance.gameObject.hideFlags = HideFlags.HideInHierarchy;

            DontDestroyOnLoad(Instance.gameObject);
        }
    }
}