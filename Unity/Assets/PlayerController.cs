using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        // TODO restrict movement in 3rd dimension
        // TODO prevent rotation
    }

    void Update()
    {
        Move();
        Jump();
        SubstractGravity();
        ApplyMoves();
    }

    private void Move()
    {
        var moveRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow);
        var moveLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.RightArrow);

        if (moveRight || moveLeft)
        {
            if (characterController.isGrounded)
            {
                //anim.SetTrigger("IsWalking");
                if (moveRight)
                {
                    moveDirection = transform.right;
                }
                else if (moveLeft)
                {
                    moveDirection = -transform.right;
                }
                moveDirection *= speed;
            }
        }
        else
        {
            moveDirection = new Vector3(0f, moveDirection.y, 0f);
            //anim.SetTrigger("IsStanding");
        }
    }

    private void Jump()
    {
        if (characterController.isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        //else
        //{
        //    anim.SetTrigger("IsStanding");
        //}
    }

    private void SubstractGravity()
    {
        moveDirection.y -= 20.0f * Time.deltaTime;
    }

    private void ApplyMoves()
    {
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
