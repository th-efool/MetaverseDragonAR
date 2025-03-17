using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static object m_Lock = new object();
    private static T m_Instance;
    private static bool m_ShuttingDown = false;

    [SerializeField] public bool dontDestroyOnLoad = true;  // Toggle for persistence
    [SerializeField] public bool CanICreateItAgain = false;  // Toggle for persistence


    public static T Instance
    {
        get
        {
            if (m_ShuttingDown)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                    "' already destroyed. Returning null.");
                return null;
            }


            lock (m_Lock)
            {
                if (m_Instance == null)
                {
                    m_Instance = (T)FindAnyObjectByType(typeof(T));

                    if (m_Instance == null)
                    {
                        var singletonObject = new GameObject();
                        m_Instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";
                    }
                }

                return m_Instance;
            }
        }
    }

    protected virtual void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this as T;
            if (dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }
        else if (m_Instance != this)
        {
            Destroy(gameObject);  // Destroy duplicate instances
        }
    }
    private void OnApplicationQuit()
    {
        if (!CanICreateItAgain) { m_ShuttingDown = true; }
    }

    protected virtual void OnDestroy()
    {
        if (CanICreateItAgain) { 
            if (m_Instance == this){m_Instance = null;}
        } else { m_ShuttingDown = true; }
    }


}
