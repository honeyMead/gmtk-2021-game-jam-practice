using System;
using UnityEngine;

public class FriendController : MonoBehaviour
{
    private Transform player = null;
    public Transform Head { get; private set; } = null;

    private Rigidbody rigid;
    private PlayerController playerController = null;
    private bool isConnectedToPlayer = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerController = player.GetComponent<PlayerController>();
        Head = transform.Find("Head");
        rigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        FollowPlayer();
        DieWhenTooLow();
    }

    private void FollowPlayer()
    {
        var doesPlayerAction = Input.GetKey(KeyCode.E)
                    || Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl);

        if (isConnectedToPlayer && !doesPlayerAction)
        {
            var distanceToPlayer = Vector3.Distance(player.position, transform.position);
            var distanceToKeep = 0.8f;

            if (distanceToPlayer > distanceToKeep)
            {
                var speed = 0.3f;

                //var rotationTowardsPlayer = Quaternion
                //    .Slerp(transform.rotation, Quaternion
                //        .LookRotation(player.transform.position - transform.position),
                //    Time.deltaTime); // TODO fix rotation & faster

                //var positionNearPlayer = Vector3.MoveTowards(transform.position, player.position, speed);
                //transform.position = positionNearPlayer;

                // TODO move with force
                var direction = (player.transform.position - transform.position).normalized * speed;
                rigid.AddForce(direction, ForceMode.VelocityChange);

                //transform.SetPositionAndRotation(positionNearPlayer, rotationTowardsPlayer);
                // TODO try to not move too deep into other objects/ground
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

    private void DieWhenTooLow()
    {
        if (transform.position.y < -3f)
        {
            Destroy(gameObject);
        }
    }
}
