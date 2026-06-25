using UnityEngine;
using UnityEngine.SceneManagement;

public class LojaManager : MonoBehaviour
{
    // Função única para todas as compras
    public void ClicarNoItem(string nomeDaMelhoria)
    {
        // IF para checar se tem pontos suficientes
        // if (meusPontos >= preco) { ... }

        Debug.Log("O jogador clicou para comprar: " + nomeDaMelhoria);
    }

    public void IrParaProximoPlaneta()
    {
        // Como o ciclo do jogo será Fase -> Loja -> Fase,
        // mandamos o jogador de volta para a cena da fase
        SceneManager.LoadScene(2);
    }
}