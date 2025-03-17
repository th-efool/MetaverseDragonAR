using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static object m_Lock = new object();
    private static T m_Instance;

    [SerializeField] public bool dontDestroyOnLoad = true;  // Toggle for persistence

    public static T Instance
    {
        get
        {

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

    protected virtual void OnDestroy()
    {
        if (m_Instance == this)
        {
            m_Instance = null;
        }
    }


}
