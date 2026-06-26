using UnityEngine;

public class EnemySpawnControll : MonoBehaviour
{
    [SerializeField] private EnemySpawner[] spawnPoints;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach(EnemySpawner spawner in spawnPoints)
            {
                if(spawner != null)
                spawner.Spawn();
            }
        }
    }
}
