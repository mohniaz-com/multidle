using FishNet.Object;
using UnityEngine;

namespace Multidle
{
    /// <summary>
    /// MonoBehaviours that derive from the Singleton class
    /// are expected to have only one instance in a scene.
    /// </summary>
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        protected virtual void Awake() => Instance = this as T;

        protected virtual void OnDestroy()
        {
            if (Instance != this)
                Instance = null;
        }
    }

    /// <summary>
    /// The PersistentSingleton class performs the same task
    /// as the Singleton class, except it destroys itself if
    /// an instance of it already exists.
    /// </summary>
    public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            base.Awake();
        }
    }

    /// <summary>
    /// The NetworkSingleton class performs the same task
    /// as the Singleton class, except it derives from
    /// NetworkBehaviour
    /// </summary>
    public abstract class NetworkSingleton<T> : NetworkBehaviour where T : NetworkBehaviour
    {
        public static T Instance { get; private set; }

        protected virtual void Awake() => Instance = this as T;

        protected virtual void OnDestroy()
        {
            if (Instance != this)
                Instance = null;
        }
    }
}