using UnityEngine;

public class Exit : MonoBehaviour
{
    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        var isPlayer = other.gameObject.CompareTag("Player");

        if (isPlayer)
        {
            // TODO load next level or show winning screen
            Debug.Log("Exit reached!");
        }
    }
}
