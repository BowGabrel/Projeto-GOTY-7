using System.Collections;
using UnityEngine;

public class Coletavel : MonoBehaviour
{
    [SerializeField] private AudioClip colectSFX;
    [SerializeField] private float tempoDespawn;
    public float valor;
    private Transform tr;
    private SpriteRenderer sr;
    private GameObject display;
    private bool pickUpColldown = true;

    private void Awake()
    {
        display = GameObject.Find("DisplayPontos");
        tr = GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce (new Vector3 (Random.Range(50,-50), Random.Range(50, -50), 0));
        StartCoroutine(TempoPickUp());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coletor") && !pickUpColldown)
        {
            if (colectSFX != null)
            AudioSource.PlayClipAtPoint(colectSFX, tr.position, 1f);
            display.GetComponent<ContadorPNT>().AddScore(valor);
            Destroy(this.gameObject);
        }
    }

    private IEnumerator TempoPickUp()
    {
        sr.color = new Vector4(1f, 1f, 1f, 0.25f);
        yield return new WaitForSeconds(1f);
        pickUpColldown = false;
        sr.color = new Vector4(1f, 1f, 1f, 1f);
        StartCoroutine(DespawnTimer());
    }

    private IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(tempoDespawn / 2);
        for (int i = 0; i < tempoDespawn / 2; i++)
        {
            sr.color = new Vector4(1f, 1f, 1f, 0.25f);
            yield return new WaitForSeconds(0.5f);
            sr.color = new Vector4(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.5f);
        }
        Destroy(this.gameObject);
    }
}

