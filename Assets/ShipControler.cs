using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipControl : MonoBehaviour
{
    private Transform tr;
    private Rigidbody2D rb;
    private Camera mainCam;
    private Vector3 mousePos;
    private bool cooldown = true;
    [SerializeField] private float speed;
    [SerializeField] private float shootingSpeed;
    [SerializeField] GameObject bala;

    void Start(){
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
    }

    private void LookAtMouse(){
        mousePos = (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseDistance = mousePos - tr.position;
        float rotZ = (Mathf.Atan2(mouseDistance.y, mouseDistance.x) * Mathf.Rad2Deg) - 90;

        rb.transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    private void Propell(){
        float ang;

        ang = tr.eulerAngles.z / 180 * math.PI;

        Vector3 vect = new Vector3((speed * Mathf.Sin(-ang)), (speed * Mathf.Cos(-ang)));
        rb.AddForce(vect);
    }

    private IEnumerator Shoot()
    {
        cooldown = false;
        Instantiate(bala, tr.position, tr.rotation);
        yield return new WaitForSeconds(shootingSpeed);
        cooldown = true;
    }

    private void FixedUpdate(){
        LookAtMouse();
        if (Keyboard.current.spaceKey.isPressed) Propell();
        if (Mouse.current.leftButton.isPressed & cooldown == true) StartCoroutine(Shoot());
    }
}
