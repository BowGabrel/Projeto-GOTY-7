using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private bool canspawn = true;

    public void Spawn()
    {
        if (enemy != null && canspawn)
        {
            Instantiate(enemy, transform);
            canspawn = false;
        }
    }
}
