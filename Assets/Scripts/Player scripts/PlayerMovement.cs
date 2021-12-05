using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    public CharacterController controller;

    float speed;
    public float stamina = 100;
    public float gravity = -9.81f;
    public float jumpHeight;
    Vector3 velocity;
    [Header("Ground Information")]

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    public bool isGrounded;

    private void Start()
    {
        instance = this;
    }

    void Update()
    {
        stamina = Mathf.Clamp(stamina, 0, 100);
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if(stamina > 0)
            {
                StopAllCoroutines();
                speed = 10;
                stamina -= speed * Time.deltaTime;
            }
            else
            {
                speed = 5;
            }
        }
        else
        {
            speed = 5;
            StartCoroutine(AddStamina());
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded && stamina >= 10)
        {
            StopAllCoroutines();
            stamina -= 10;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isGrounded = false;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    IEnumerator AddStamina()
    {
        yield return new WaitForSeconds(5f);
        stamina = Mathf.Lerp(stamina, 100, 0.005f);
    }
}
