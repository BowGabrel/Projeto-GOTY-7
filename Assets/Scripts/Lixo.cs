using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Lixo : MonoBehaviour
{
    private GameObject display;
    [SerializeField] private GameObject drop;
    [SerializeField] private int dropAmount;
    [SerializeField] private float vida;
    [SerializeField] private bool isCollectable;
    [SerializeField] private int valor;
    private Transform tr;

    private void Start()
    {
        display = GameObject.Find("DisplayPontos");
        tr = GetComponent<Transform>();
        GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-25, 25), Random.Range(-25, 25), 0));
    }

    public int CalcValor()
    {
        if (!isCollectable)
            return drop.GetComponent<Lixo>().CalcValor() * dropAmount;
        else
            return valor;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Projetil") && !isCollectable)
        {
            vida = vida - collision.gameObject.GetComponent<Projectile_logic>().dano;
            if (vida <= 0)Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Coletor") && isCollectable)
        {
            display.GetComponent<ContadorPNT>().pontos = display.GetComponent<ContadorPNT>().pontos + valor;
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy(){
        if (dropAmount > 0 && drop != null)
        {
            for (int i = 0; i < dropAmount; i++)
                Instantiate(drop, tr.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        }
    }
}
