using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private Transform playerModel;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerModel = transform.Find("PlayerModel");
        // TODO restrict movement in 3rd dimension
        // TODO prevent rotation
        // TODO allow moving when in air
    }

    void Update()
    {
        Move();
        Jump();
        SubstractGravity();
        ApplyMoves();
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
                    playerModel.rotation = Quaternion.Euler(0, 0, 0);
                }
                else if (moveLeft)
                {
                    moveDirection = -transform.right;
                    playerModel.rotation = Quaternion.Euler(0, 180, 0);
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
        moveDirection.y += Physics.gravity.y * Time.deltaTime;
    }

    private void ApplyMoves()
    {
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
