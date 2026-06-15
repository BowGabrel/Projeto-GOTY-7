using UnityEngine;

public class CustomCameraBrain : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Transform ptr;
    void Start(){
        ptr = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update(){
        transform.position = new Vector3(ptr.position.x, ptr.position.y, -20);
    }
}
