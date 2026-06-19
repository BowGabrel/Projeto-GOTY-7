using Unity.VisualScripting;
using UnityEngine;

public class Coletavel : MonoBehaviour
{
    private GameObject display;
    [SerializeField] private int valor;
    private Rigidbody2D rb;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0));
        display = GameObject.Find("DisplayPontos");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coletor")){
            display.GetComponent<ContadorPNT>().pontos = display.GetComponent<ContadorPNT>().pontos + valor;
            Destroy(this.gameObject);
        }
    }
}
