using UnityEngine;

public class SingletonGameObject : MonoBehaviour
{
    public static SingletonGameObject instance;
    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
        Destroy(this.gameObject);
        DontDestroyOnLoad(gameObject);
        instance = this;
    }
}
