using System.ComponentModel;
using TMPro;
using Unity.Burst;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class ContadorPNT : MonoBehaviour
{
    private TextMeshProUGUI txmp;
    private SpriteRenderer fundo;
    public float maxPontos;
    public float pontos;

    void Start(){
        fundo = GameObject.FindGameObjectWithTag("Fundo").GetComponent<SpriteRenderer>();
        txmp = GetComponent<TextMeshProUGUI>();
    }

    private void Update(){
        float cor = pontos / maxPontos;
        fundo.color = new Color(cor, 1, cor);
        txmp.text = pontos + " kg";
    }
}
