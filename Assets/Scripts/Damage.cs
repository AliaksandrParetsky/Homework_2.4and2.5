using UnityEngine;
using UnityEngine.Playables;

public class Damage : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
