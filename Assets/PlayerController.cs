using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float maxSpeed;
    public float speed;
    public float jumpSpeed;

    private Rigidbody rigid;
    private bool isOnGround = false;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // TODO restrict movement in 3rd dimension
        float moveHorizontal = 0;
        float moveVertical = 0;

        if (Input.GetKey(KeyCode.A))
        {
            moveHorizontal = -1;

            var speedInRightDirection = Vector3.Project(rigid.velocity, transform.right).magnitude;
            if (speedInRightDirection > maxSpeed)
            {
                moveHorizontal = 0;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveHorizontal = 1;

            var speedInLeftDirection = Vector3.Project(rigid.velocity, -transform.right).magnitude;
            if (speedInLeftDirection > maxSpeed)
            {
                moveHorizontal = 0;
            }
        }
        if (isOnGround && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)))
        {
            isOnGround = false;
            moveVertical = jumpSpeed;
        }

        // TODO implement faster turning around and stopping when no button is pressed
        var movement = new Vector3(moveHorizontal * speed, 0, 0);
        rigid.AddForce(movement);

        var jumpMove = new Vector3(0, moveVertical, 0);
        rigid.AddForce(jumpMove, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (var contact in collision.contacts)
        {
            var collidesWithGround = contact.otherCollider.gameObject.CompareTag("ground");

            if (collidesWithGround)
            {
                var collidesOnBottom = Vector3.Dot(contact.normal, Vector3.up) > 0.5;
                if (collidesOnBottom)
                {
                    isOnGround = true;
                    return;
                }
            }
        }
    }
}
