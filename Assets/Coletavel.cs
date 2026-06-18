using UnityEngine;

public class Coletavel : MonoBehaviour
{
    private Rigidbody2D rb;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0));
    }
}
