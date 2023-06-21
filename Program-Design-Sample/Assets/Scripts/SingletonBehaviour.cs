using UnityEngine;

public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance != null) return _instance;
            var type = typeof(T);

            _instance = (T)FindObjectOfType(type);
            if (_instance != null) return _instance;
            var typeName = type.ToString();
 
            var gameObject = new GameObject( typeName, type );
            _instance = gameObject.GetComponent<T>();
 
            if( _instance == null )
            {
                Debug.LogError("Problem during the creation of " + typeName,gameObject );
            }


            return _instance;
        }
    }
    protected virtual void OnInitialize() {}
    protected virtual void Awake()
    {
        // 他のゲームオブジェクトにアタッチされているか調べる
        // アタッチされている場合は破棄する。
        Initialize();
    }

    private bool Initialize()
    {
        if (_instance == null)
        {
            _instance = this as T;
            OnInitialize();
            return true;
        }
        else if (Instance == this)
        {
            return true;
        }
        Destroy(this);
        return false;
    }
        
}