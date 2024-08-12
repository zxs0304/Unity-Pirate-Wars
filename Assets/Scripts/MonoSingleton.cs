using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonoSingleton<T> : MonoBehaviour where T : Component
{
    protected static T _instance;
    /// <summary>
    /// On awake, we initialize our instance. Make sure to call base.Awake() in override if you need awake.
    /// </summary>
    protected virtual void Awake()
    {
        if (!Application.isPlaying) return;
        _instance = this as T;
        Init();
    }

    /// <summary>
    /// Singleton design pattern
    /// </summary>
    /// <value>The instance.</value>
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name);
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    protected virtual void Init()
    {

    }
}

