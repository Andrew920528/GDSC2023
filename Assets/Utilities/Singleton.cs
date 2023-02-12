using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour 
{
    private T instance;
    // Start is called before the first frame update

    public T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
            } else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
            return instance;
        }
    }
}
