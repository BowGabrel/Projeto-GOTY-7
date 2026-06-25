using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipControl : MonoBehaviour
{
    public static ShipControl instance;

    private Transform tr;
    private Rigidbody2D rb;
    private Camera mainCam;
    private bool cooldown = true;
    public float speed;
    public float shootingSpeed;
    public float damage;
    [SerializeField] private GameObject bala;
    [SerializeField] private GameObject dmgDrop;
    private GameObject coletor;
    private ContadorPNT display;

    private void Awake()
    {
        display = GameObject.Find("DisplayPontos").GetComponent<ContadorPNT>();
        if (instance == null) instance = this;
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
        coletor = GameObject.FindWithTag("Coletor");
    }

    private void LookAtMouse()
    {
        Vector2 mouseDistance = (Vector3)mainCam.ScreenToWorldPoint(Input.mousePosition) - tr.position;
        float rotZ = (Mathf.Atan2(mouseDistance.y, mouseDistance.x) * Mathf.Rad2Deg) - 90;
        rb.transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    private void Propell()
    {
        float ang;
        ang = tr.eulerAngles.z / 180 * math.PI;
        Vector3 vect = new Vector3((speed * 100 * Mathf.Sin(-ang)), (speed * 100 * Mathf.Cos(-ang)));
        rb.AddForce(vect);
        rb.linearDamping = 1f + speed;
    }

    private IEnumerator Shoot()
    {
        cooldown = false;
        GameObject temp = Instantiate(bala, tr.position, tr.rotation);
        temp.GetComponent<Projectile_logic>().dano = damage * 10;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.5f / shootingSpeed);
        cooldown = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float dropValue;
        if (collision.CompareTag("ProjetilInimigo"))
        {
            dropValue = display.RemoveScore(collision.GetComponent<Projectile_logic>().dano);
            if (dropValue > 0) {
                GameObject temp = Instantiate(dmgDrop, tr.position, tr.rotation);
                temp.GetComponent<Coletavel>().valor = dropValue;
            }
        }
    }

    private void FixedUpdate()
    {
        LookAtMouse();
        if (Keyboard.current.spaceKey.isPressed) Propell();
    }

    private void Update()
    {
        if (Mouse.current.leftButton.isPressed & cooldown == true && !Mouse.current.rightButton.isPressed) StartCoroutine(Shoot());
        if (Mouse.current.rightButton.isPressed)
        {
            coletor.GetComponent<PolygonCollider2D>().enabled = true;
            coletor.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            coletor.GetComponent<PolygonCollider2D>().enabled = false;
            coletor.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
