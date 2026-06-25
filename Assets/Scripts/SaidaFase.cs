using UnityEngine;
using UnityEngine.SceneManagement;

public class SaidaFase : MonoBehaviour
{
    [Header("Configuração de Fluxo")]
    [SerializeField] private int indexDesteProximoLevel; 
    private void OnTriggerEnter2D(Collider2D colisao)
    {
        if (colisao.CompareTag("Player"))
        {
            GameManager.proximoLevelIndex = indexDesteProximoLevel;

            SceneManager.LoadScene("Loja");
        }
    }
}