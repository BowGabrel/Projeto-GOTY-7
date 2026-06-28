using System.Collections;
using Unity.Cinemachine;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ShipControl : MonoBehaviour
{
    public static ShipControl instance;

    private Transform tr;
    private Rigidbody2D rb;
    private Camera mainCam;
    private Transform strtpnt;
    private bool cooldown = true;
    public float speed;
    public float shootingSpeed;
    public float damage;
    private bool ativo = true;
    public bool restartCheck = false;
    [SerializeField] private GameObject bala;
    [SerializeField] private GameObject dmgDrop;
    [SerializeField] private Timer timer;
    private AudioSource music;
    private GameObject coletor;
    private ContadorPNT display;

    private void Awake()
    {
        music = GameObject.FindWithTag("Aim").GetComponent<AudioSource>();
        display = GameObject.Find("DisplayPontos").GetComponent<ContadorPNT>();
        if (instance == null) instance = this;
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
        strtpnt = GameObject.FindWithTag("StartPoint").transform;
        coletor = GameObject.FindWithTag("Coletor");
        timer.RestartTimer();
        if(strtpnt != null)
            tr.position = strtpnt.position;
    }

    private void Continue()
    {
        timer.Go();
        display.UpdateScore();
        music.Play();
        ativo = true;
    }

    public void Restart()
    {
        restartCheck = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Reset();
    }

    public void Reset()
    {
        strtpnt = GameObject.FindWithTag("StartPoint").transform;
        if (strtpnt != null)
            tr.position = strtpnt.position;
        display.ResetScore();
        timer.RestartTimer();
        Continue();
    }

    public void Stop()
    {
        ativo = false;
        timer.Stop();
        music.Stop();
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
        Vector3 vect = new Vector3((50 + (speed * 50)) * Mathf.Sin(-ang), (50 + (speed * 50)) * Mathf.Cos(-ang));
        rb.AddForce(vect);
        rb.linearDamping = 1f + (speed / 2);
    }

    private IEnumerator Shoot()
    {
        cooldown = false;
        GameObject temp = Instantiate(bala, tr.position, tr.rotation);
        temp.GetComponent<Projectile_logic>().dano = damage * 10;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.1f + (0.4f / shootingSpeed));
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
        if (ativo)
        {
            LookAtMouse();
            if (Keyboard.current.spaceKey.isPressed) Propell();
        }
    }

    private void Update()
    {
        if (ativo)
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
}
