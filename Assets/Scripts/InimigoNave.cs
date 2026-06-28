using UnityEngine;

public class InimigoNave : MonoBehaviour
{
    [Header("Movimentação")]
    public float velocidade = 3f;
    private Transform jogador;

    [Header("Combate")]
    public GameObject prefabTiro; 
    public Transform pontoDeTiro; 
    public float tempoEntreTiros = 2f;
    public float vida = 1f;
    private float contadorTiro;

    [Header("Drops")]
    [SerializeField] private GameObject drop;
    [SerializeField] private float dropAmount;

    void Start()
    {
        GameObject objJogador = GameObject.FindGameObjectWithTag("Player");
        if (objJogador != null)
        {
            jogador = objJogador.transform;
        }

        contadorTiro = tempoEntreTiros;
    }

    void Update()
    {
        if (jogador == null) return;

        transform.position = Vector2.MoveTowards(transform.position, jogador.position, velocidade * Time.deltaTime);

        Vector2 direcao = jogador.position - transform.position;
        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angulo);

        contadorTiro -= Time.deltaTime;
        if (contadorTiro <= 0f)
        {
            Atirar();
            contadorTiro = tempoEntreTiros; 
        }
    }

    void Atirar()
    {
        Instantiate(prefabTiro, pontoDeTiro.position, pontoDeTiro.rotation);
    }
    private void OnTriggerEnter2D(Collider2D colisao)
    {
        if (colisao.CompareTag("Projetil"))
        {
            vida -= colisao.GetComponent<Projectile_logic>().dano;
            if (vida <= 0f)
            {
                if (drop != null)
                    for (int i = 0; i < dropAmount; i++) Instantiate(drop, GetComponent<Transform>().position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }
}