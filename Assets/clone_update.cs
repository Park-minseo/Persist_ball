using UnityEngine;

public class clone_update : MonoBehaviour
{
    public GameObject target; // ≈∏∞Ÿ¿∏∑Œ ªÔ¿ª ø¿∫Í¡ß∆Æ
    public GameObject me;
    void Update()
    {
            me.transform.position = new Vector3(me.transform.position.x, target.transform.position.y + 0.5f, me.transform.position.z);
        me.transform.rotation = Quaternion.Euler(-90f, -90f, -30f); 
    }
}