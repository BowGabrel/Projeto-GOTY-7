using UnityEngine;

public class TamanhoBala : MonoBehaviour
{
    private Transform tr;
    private Projectile_logic pl;
    private void Start()
    {
        pl = GetComponent<Projectile_logic>();
        tr = GetComponent<Transform>();
        tr.localScale = new Vector3(0.03f + pl.dano/360, 0.04f + pl.dano/180, 0);
    }
}
