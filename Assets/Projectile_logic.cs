using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile_logic : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifeSpan;

    private void Start(){
        StartCoroutine(Lifespan());
    }

    private void OnTriggerEnter2D(Collider2D collision){
        Destroy(this.gameObject);
    }

    private void Update(){
        transform.Translate(0, bulletSpeed * Time.deltaTime, 0);
    }

    private IEnumerator Lifespan(){
        yield return new WaitForSeconds(bulletLifeSpan);
        Destroy(this.gameObject);
    }
}
