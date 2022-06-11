using UnityEngine;

public class FriendController : MonoBehaviour
{
    private Transform player = null;
    private bool isConnectedToPlayer = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (isConnectedToPlayer)
        {
            var distanceToPlayer = Vector3.Distance(player.position, transform.position);
            var distanceToKeep = 0.65f;

            if (distanceToPlayer > distanceToKeep)
            {
                var speed = 0.1f;

                transform.rotation = Quaternion
                    .Slerp(transform.rotation, Quaternion
                        .LookRotation(player.transform.position - transform.position),
                    Time.deltaTime); // TODO fix rotation & faster

                transform.position = Vector3.MoveTowards(transform.position, player.position, speed);
                // TODO try to not move too deep into other objects/ground
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        var isPlayer = other.gameObject.CompareTag("Player");

        if (isPlayer)
        {
            isConnectedToPlayer = true;
        }
    }
}
