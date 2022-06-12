using UnityEngine;

public class FriendController : MonoBehaviour
{
    private Transform player = null;
    public Transform Head { get; private set; } = null;
    public BoxCollider HeadCollider { get; private set; } = null;
    private PlayerController playerController = null;
    private bool isConnectedToPlayer = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerController = player.GetComponent<PlayerController>();
        Head = transform.parent.Find("Head");
        HeadCollider = Head.GetComponent<BoxCollider>();
    }

    void FixedUpdate()
    {
        var doesPlayerAction = Input.GetKey(KeyCode.E)
            || Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl);

        if (isConnectedToPlayer && !doesPlayerAction)
        {
            var distanceToPlayer = Vector3.Distance(player.position, transform.parent.position);
            var distanceToKeep = 0.71f;

            if (distanceToPlayer > distanceToKeep)
            {
                HeadCollider.enabled = false;
                var speed = 0.1f;

                transform.parent.rotation = Quaternion
                    .Slerp(transform.parent.rotation, Quaternion
                        .LookRotation(player.transform.position - transform.parent.position),
                    Time.deltaTime); // TODO fix rotation & faster

                transform.parent.position = Vector3.MoveTowards(transform.parent.position, player.position, speed);
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
            playerController.Join(this);
        }
    }
}
