using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableObjectLibrary<T1, T2> : ScriptableObject
{
    [SerializeField] protected List<T2> m_Values;

    protected Dictionary<T1, T2> m_Entries = new Dictionary<T1, T2>();
    public Dictionary<T1, T2> Entries
    {
        get
        {
            if (m_IsInitialized)
            {
                return m_Entries;
            }
            else
            {
                Debug.LogError($"{typeof(T2).Name} Library not initialized yet.");
                return null;
            }
        }
    }

    protected abstract T1 ExtractKey(T2 value);
    protected virtual void FindAndUpdateValues() { }

    private bool m_IsInitialized = false;

    private void OnEnable()
    {
#if UNITY_EDITOR
        FindAndUpdateValues();
#endif

        Initialize();
    }

    private void Initialize()
    {
        m_Entries.Clear();

        foreach (var value in m_Values)
        {
            m_Entries.Add(ExtractKey(value), value);
        }

        m_IsInitialized = true;
    }
}