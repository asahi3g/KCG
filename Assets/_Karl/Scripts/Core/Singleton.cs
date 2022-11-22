using UnityEngine;

public abstract class Singleton<T> : BaseMonoBehaviour where T : BaseMonoBehaviour
{
    private static bool m_ShuttingDown = false;
    private static object m_Lock = new object();
    private static T m_Instance;

    /// <summary>
    /// Access singleton instance through this propriety.
    /// </summary>
    public static T Instance
    {
        get
        {
            lock (m_Lock)
            {
                if (m_Instance == null)
                {
                    m_Instance = (T)FindObjectOfType(typeof(T));
                    if (m_Instance == null)
                    {
                        string goName = typeof(T).Name;
                        GameObject go = GameObject.Find(goName);
                        if (go != null)
                        {
                            m_Instance = go.GetComponent<T>();
                        }
                    }
                    if (m_Instance == null)
                    {
                        Debug.LogError($"Instance of '{nameof(T)}' does not exist");
                    }
                }

                return m_Instance;
            }
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        TrySet();
    }

    protected override void Awake()
    {
        base.Awake();
        TrySet();
    }

    private void TrySet()
    {
        lock (m_Lock)
        {
            if (m_Instance == null)
            {
                m_Instance = GetComponent<T>();
            }
        }
    }


    protected override void OnApplicationQuit()
    {
        base.OnApplicationQuit();
        m_ShuttingDown = true;
    }


    protected override void OnDestroy()
    {
        base.OnDestroy();
        m_ShuttingDown = true;
    }
}