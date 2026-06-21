using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [Header("Componentes da UI")]
    public Image fundoImagem;
    public TextMeshProUGUI textoDialogo;
    public GameObject painelDialogo;

    [Header("Fading Final")]
    public CanvasGroup fadeOverlayGroup;
    public float duracaoFadeOut = 1.5f;

    // VARIÁVEIS DE ÁUDIO DECLARADAS AQUI:
    private AudioSource musicaFundo;
    private float volumeInicialAudio;

    [Header("Configurações")]
    public float velocidadeDigitacao = 0.05f;
    public float delayInicial = 1f;

    [Header("História")]
    public Sprite[] imagens;
    [TextArea(3, 5)]
    public string[] dialogos;

    private int indiceAtual = 0;
    private bool podeAvancar = false;
    private bool estaEscrevendo = false;

    void Start()
    {
        fadeOverlayGroup.alpha = 0f;
        fadeOverlayGroup.gameObject.SetActive(false);

        // Busca automaticamente o objeto "MusicaMenu" que sobreviveu à troca de cena
        GameObject objetoMusica = GameObject.Find("MusicaMenu");
        if (objetoMusica != null)
        {
            musicaFundo = objetoMusica.GetComponent<AudioSource>();
            volumeInicialAudio = musicaFundo.volume;
        }

        StartCoroutine(ExibirCena(indiceAtual));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (estaEscrevendo)
            {
                StopAllCoroutines();
                textoDialogo.text = dialogos[indiceAtual];
                estaEscrevendo = false;
                podeAvancar = true;
            }
            else if (podeAvancar)
            {
                AvancarCena();
            }
        }
    }

    void AvancarCena()
    {
        indiceAtual++;
        if (indiceAtual < imagens.Length)
        {
            StartCoroutine(ExibirCena(indiceAtual));
        }
        else
        {
            StartCoroutine(FadeOutAndLoadGame());
        }
    }

    IEnumerator FadeOutAndLoadGame()
    {
        podeAvancar = false;

        fadeOverlayGroup.gameObject.SetActive(true);
        fadeOverlayGroup.blocksRaycasts = true;

        float counter = 0f;

        while (counter < duracaoFadeOut)
        {
            counter += Time.deltaTime;
            float progresso = counter / duracaoFadeOut;

            fadeOverlayGroup.alpha = Mathf.Lerp(0f, 1f, progresso);

            if (musicaFundo != null)
            {
                musicaFundo.volume = Mathf.Lerp(volumeInicialAudio, 0f, progresso);
            }

            yield return null;
        }

        fadeOverlayGroup.alpha = 1f;

        // Destrói a música do menu após o fade out para limpar a memória para o jogo
        if (musicaFundo != null)
        {
            Destroy(musicaFundo.gameObject);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator ExibirCena(int index)
    {
        podeAvancar = false;
        textoDialogo.text = "";
        fundoImagem.sprite = imagens[index];
        painelDialogo.SetActive(false);
        yield return new WaitForSeconds(delayInicial);
        painelDialogo.SetActive(true);
        StartCoroutine(DigitarTexto(dialogos[index]));
    }

    IEnumerator DigitarTexto(string frase)
    {
        estaEscrevendo = true;
        textoDialogo.text = "";
        foreach (char letra in frase.ToCharArray())
        {
            textoDialogo.text += letra;
            yield return new WaitForSeconds(velocidadeDigitacao);
        }
        estaEscrevendo = false;
        podeAvancar = true;
    }
}