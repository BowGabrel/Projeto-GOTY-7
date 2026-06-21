using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CustomCameraBrain : MonoBehaviour
{
    [SerializeField] RawImage minimap;
    [SerializeField] GameObject player;
    private Camera cam;
    private Transform ptr;
    private bool minimized = true;
    void Start(){
        ptr = player.GetComponent<Transform>();
        cam = GetComponent<Camera>();
    }

    void Update(){
        if (minimized == true) {
            minimap.rectTransform.anchoredPosition = new Vector3(200, -200, 0);
            minimap.rectTransform.localScale = new Vector3(400, 400, 0);
            cam.orthographicSize = 15;
            transform.position = new Vector3(ptr.position.x, ptr.position.y, -20);
            if (Input.GetKeyDown(KeyCode.M)) minimized = false;
        } else {
            minimap.rectTransform.anchoredPosition = new Vector3(400, -400, 0);
            minimap.rectTransform.localScale = new Vector3(800, 800, 0);
            cam.orthographicSize = 40;
            if (Input.GetKeyDown(KeyCode.M)) minimized = true;
            if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed) transform.Translate(0, 30 * Time.deltaTime, 0);
            if (Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed) transform.Translate(0, -30 * Time.deltaTime, 0);
            if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed) transform.Translate(30 * Time.deltaTime, 0, 0);
            if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed) transform.Translate(-30 * Time.deltaTime, 0, 0);
        }
    }
}
