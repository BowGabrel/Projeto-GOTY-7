using UnityEngine;

public class SpawnLixo : MonoBehaviour
{
    [SerializeField] GameObject[] lixos;
    private float spawnAmount;
    private Transform tr;
    void Start(){
        tr = GetComponent<Transform>();
        spawnAmount = GameObject.Find("DisplayPontos").GetComponent<ContadorPNT>().maxPontos;
        GenerarLixos(GetComponent<BoxCollider2D>(), lixos);
    }

    private void GenerarLixos(Collider2D spawnableAreaCollider, GameObject[] lixos)
    {
        for (; spawnAmount >= lixos[0].GetComponent<Lixo>().CalcValor();)
        {
            Vector2 spawnPosition = RandomSpawnPosition(spawnableAreaCollider);
            GameObject lixoGerado = lixos[Random.Range(0, lixos.Length)];
            int valorLixo = lixoGerado.GetComponent<Lixo>().CalcValor();
            if (spawnAmount >= valorLixo){
                Instantiate(lixoGerado, spawnPosition, Quaternion.identity);
                spawnAmount -= valorLixo;
            }
        }
    }

    private Vector2 RandomSpawnPosition(Collider2D spawnableAreaCollider)
    {
        Vector2 spawnPosition = Vector2.zero;
        bool isValidSpawn = false;

        int attemptCount = 0;
        int maxAttempts = 200;

        int layerNotToSpawn = LayerMask.NameToLayer("Terreno");

        while(!isValidSpawn  && attemptCount < maxAttempts)
        {
            spawnPosition = RandomPointInCollider(spawnableAreaCollider, 20f);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, 3f);

            bool isInvalidPos = false;
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.layer == layerNotToSpawn)
                {
                    isInvalidPos = true;
                    break;
                }
            }
            if (!isInvalidPos)
            {
                isValidSpawn = true;
            }
            attemptCount++;
        }

        if (!isValidSpawn) print("uo o!");
        return spawnPosition;
    }

    private Vector2 RandomPointInCollider(Collider2D collider, float offset = 2.0f){
        Bounds colBounds = collider.bounds;

        Vector2 minBounds = new Vector2(colBounds.min.x + offset, colBounds.min.y + offset);
        Vector2 maxBounds = new Vector2(colBounds.max.x - offset, colBounds.max.y - offset);

        float posX = Random.Range(minBounds.x, maxBounds.x);
        float posY = Random.Range(minBounds.y, maxBounds.y);

        return new Vector2(posX, posY);
    }
}
