using UnityEngine;

public class Lixo : MonoBehaviour
{
    [SerializeField] private GameObject drop;
    [SerializeField] private int dropAmount;
    [SerializeField] private float vida;
    private Transform tr;

    private void Start()
    {
        tr = GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Projetil"))
        {
            vida = vida - collision.gameObject.GetComponent<Projectile_logic>().dano;
            if (vida <= 0)Destroy(this.gameObject);
        }
    }

    private void OnDestroy(){
        for (int i = 0; i < dropAmount; i++)
            Instantiate(drop, tr.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
    }
}
