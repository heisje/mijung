using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;

    public static T Ins
    {
        get
        {
            if (instance == null) // 1
            {
                instance = (T)FindObjectOfType(typeof(T)); // 2

                if (instance == null) // 3
                {
                    GameObject obj = new GameObject(typeof(T).Name, typeof(T));
                    instance = obj.GetComponent<T>();
                }
            }
            return instance;
        }
    }

    public void Awake()
    {
        if (transform.parent != null && transform.root != null) // 5
        {
            DontDestroyOnLoad(this.transform.root.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject); // 4
        }
        Instantiation();
    }

    protected virtual void Instantiation()
    {

    }
}