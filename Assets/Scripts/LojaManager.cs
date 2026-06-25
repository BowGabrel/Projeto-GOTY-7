using UnityEngine;
using UnityEngine.SceneManagement;

public class LojaManager : MonoBehaviour
{
    [SerializeField] private float custoArtilharia;
    [SerializeField] private float custoVelocidade;
    [SerializeField] private float custoCanhao;
    public void ComprarArtilharia()
    {
        if (custoArtilharia < display.money)
        {
            display.RemoveMoney(custoArtilharia);
            ShipControl.instance.shootingSpeed++;
        }
    }
    public void ComprarVelocidade()
    {
        if (custoVelocidade < display.money)
        {
            display.RemoveMoney(custoVelocidade);
            ShipControl.instance.speed++;
        }
    }
        public void ComprarCanhao()
    {
        if (custoCanhao < display.money)
        {
            display.RemoveMoney(custoCanhao);
            ShipControl.instance.damage++;
        }
    }

    private GameObject nave;

    private ContadorPNT display;
    private void Awake()
    {
        display = GameObject.Find("DisplayPontos").GetComponent<ContadorPNT>();
        nave = GameObject.FindGameObjectWithTag("Player");
        display.ConvertToMoney();
    }
    

    public void IrParaProximoPlaneta()
    {
        SceneManager.LoadScene(GameManager.proximoLevelIndex);
    }
}