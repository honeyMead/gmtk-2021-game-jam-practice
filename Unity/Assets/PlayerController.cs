using System.Collections.Generic;
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
    private List<Transform> friends;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerModel = transform.Find("PlayerModel");
        friends = new List<Transform>();
        // TODO restrict movement in 3rd dimension
        // TODO prevent rotation
        // TODO allow moving when in air
    }

    void Update()
    {
        Move();
        Jump();
        var hadAction = PerformAction();
        if (!hadAction)
        {
            SubstractGravity();
        }
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

    public void Join(Transform friend)
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
        //else
        //{
        //    anim.SetTrigger("IsStanding");
        //}
    }

    private bool PerformAction()
    {
        if (Input.GetKey(KeyCode.E)
            || Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl))
        {
            if (friends.Count == 1)
            {
                var headOfFriend = friends[0].transform.position; // TODO on top of fren
                var distanceToFriend = Vector3.Distance(transform.position, headOfFriend);

                if (distanceToFriend > 0.2f)
                {
                    moveDirection = Vector3.MoveTowards(transform.position, headOfFriend, 1f);
                    return true; // TODO false if both fall
                }
                else
                {
                    Jump();
                }
            }
        }
        return false;
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
}
