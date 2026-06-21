using UnityEngine;

public class MusicaContinua : MonoBehaviour
{
    private static MusicaContinua instancia;

    void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(this.gameObject); 
            return;
        }

        instancia = this;
        DontDestroyOnLoad(this.gameObject);
    }
}