using UnityEngine;

public class FriendController : MonoBehaviour
{
    private Transform player = null;
    private PlayerController playerController = null;
    private bool isConnectedToPlayer = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerController = player.GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
        var doesPlayerAction = Input.GetKey(KeyCode.E)
            || Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl);

        if (isConnectedToPlayer && !doesPlayerAction)
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
                // TODO gravity
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        var isPlayer = other.gameObject.CompareTag("Player");

        if (!isConnectedToPlayer && isPlayer)
        {
            isConnectedToPlayer = true;
            playerController.Join(this.transform);
        }
    }
}
