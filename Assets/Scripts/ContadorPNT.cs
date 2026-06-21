using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ContadorPNT : MonoBehaviour
{
    private TextMeshProUGUI txmp;
    public int maxPontos;
    public int pontos;

    void Start(){
        txmp = GetComponent<TextMeshProUGUI>();
    }

    private void Update(){
        txmp.text = pontos + " kg";
    }
}
