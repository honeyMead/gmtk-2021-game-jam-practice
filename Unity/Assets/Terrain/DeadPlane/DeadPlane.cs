using UnityEngine;

public class DeadPlane : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        var isPlayer = other.gameObject.CompareTag("Player");
        if (isPlayer)
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            player.Die();
        }
    }
}
