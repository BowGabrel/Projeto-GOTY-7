using UnityEngine;

public class SpawnLixo : MonoBehaviour
{
    [SerializeField] GameObject spawns;
    [SerializeField] float tamanhoX;
    [SerializeField] float tamanhoY;
    [SerializeField] int spawnAmount;
    private Transform tr;
    void Start(){
        tr = GetComponent<Transform>();
        for (int i = 0; i < spawnAmount; i++) GenerarLixo();
    }

    private void GenerarLixo(){
        Vector3 rndPosition =  new Vector3((Random.Range(tamanhoX + tr.position.x, -tamanhoX + tr.position.x)), (Random.Range(tamanhoY + tr.position.y, -tamanhoY + tr.position.y)), 0);
        Instantiate(spawns, rndPosition, Quaternion.identity);
    }
}
