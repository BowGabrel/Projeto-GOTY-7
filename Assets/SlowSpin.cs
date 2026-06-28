using UnityEngine;

public class SlowSpin : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 2f;
    void Update()
    {
        GetComponent<Transform>().Rotate(0, 0, spinSpeed * Time.deltaTime);
    }
}
