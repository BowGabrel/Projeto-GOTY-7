using UnityEngine;
using UnityEngine.SceneManagement;

public class LojaManager : MonoBehaviour
{
    public void ClicarNoItem(string nomeDaMelhoria)
    {
        // IF para checar se tem pontos suficientes
        // if (meusPontos >= preco) { ... }

        Debug.Log("O jogador clicou para comprar: " + nomeDaMelhoria);
    }

    public void IrParaProximoPlaneta()
    {
        SceneManager.LoadScene(GameManager.proximoLevelIndex);
    }
}