using UnityEngine;

public class Lixo : MonoBehaviour
{
    [SerializeField] private float vida;

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Projetil"))
        {
            vida = vida - collision.gameObject.GetComponent<Projectile_logic>().dano;
            if (vida <= 0)Destroy(this.gameObject);
        }
    }
}
