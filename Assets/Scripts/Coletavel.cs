using UnityEngine;

public class Coletavel : MonoBehaviour
{
    [SerializeField] private AudioClip colectSFX;
    public float valor;
    private Transform tr;
    private GameObject display;


    private void Awake()
    {
        display = GameObject.Find("DisplayPontos");
        tr = GetComponent<Transform>();
    }

    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce (new Vector3 (Random.Range(50,-50), Random.Range(50, -50), 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coletor"))
        {
            if (colectSFX != null)
            AudioSource.PlayClipAtPoint(colectSFX, tr.position, 1f);
            display.GetComponent<ContadorPNT>().AddScore(valor);
            Destroy(this.gameObject);
        }
    }
}

