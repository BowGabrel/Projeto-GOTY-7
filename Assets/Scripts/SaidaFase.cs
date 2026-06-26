using UnityEngine;
using UnityEngine.SceneManagement;

public class SaidaFase : MonoBehaviour
{
    [Header("Configuração de Fluxo")]
    [SerializeField] private int indexDesteProximoLevel; 
    private void OnTriggerExit2D(Collider2D colisao)
    {
        if (colisao.CompareTag("Player"))
        {
            GameManager.proximoLevelIndex = indexDesteProximoLevel;

            ShipControl.instance.ativo = false;

            SceneManager.LoadScene("Loja");
        }
    }
}