using System.ComponentModel;
using TMPro;
using Unity.Burst;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ContadorPNT : MonoBehaviour
{
    private TextMeshProUGUI txmp;
    private Image fundo;
    public float money;
    public float pontos;

    private Color colorPNT = Color.cornflowerBlue;
    private Color colorMNY = Color.mediumSpringGreen;


    void Start()
    {
        fundo = GameObject.Find("Display Background").GetComponent<Image>();
        txmp = GetComponent<TextMeshProUGUI>();
    }

    private void UpdateScore()
    {
        fundo.color = colorPNT;
        txmp.text = pontos + " kg";
    }

    private void UpdateMoney()
    {
        fundo.color = colorMNY;
        txmp.text = money + " $";
    }

    public void AddScore(float valor)
    {
        pontos += valor;
        UpdateScore();
    }

    public float RemoveScore(float valor)
    {
        if (pontos == 0)
        {
            return 0;
        }
        else if (valor < pontos)
        {
            pontos -= valor;
            UpdateScore();
            return valor;
        }
        else{
            valor = pontos;
            pontos = 0;
            UpdateScore();
            return valor;
        }
    }

    public void ConvertToMoney(){
        money += pontos * 10;
        pontos = 0;
        UpdateMoney();
    }

    public void RemoveMoney(float valor){
        money -= valor;
        UpdateMoney();
    }
}
