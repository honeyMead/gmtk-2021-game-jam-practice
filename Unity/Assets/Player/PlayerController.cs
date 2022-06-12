using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public GameControl gameControl;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private Transform playerModel;
    private List<FriendController> friends;
    private Animator anim;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerModel = transform.Find("PlayerModel");
        friends = new List<FriendController>();
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
        SubstractGravity();
        ApplyMoves();
        DieWhenTooLow();
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
            var currentY = moveDirection.y;
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
            moveDirection.y = currentY;
        }
        else
        {
            moveDirection = new Vector3(0f, moveDirection.y, 0f);
            //anim.SetTrigger("IsStanding");
        }
    }

    public void Join(FriendController friend)
    {
        friends.Add(friend);
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
        else
        {
            //anim.SetTrigger("IsStanding");
        }
    }

    private void SubstractGravity()
    {
        if (!characterController.isGrounded)
        {
            moveDirection.y += Physics.gravity.y * Time.deltaTime;
        }
    }

    private void ApplyMoves()
    {
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void DieWhenTooLow()
    {
        if (transform.position.y < -3f)
        {
            Die();
        }
    }
}
