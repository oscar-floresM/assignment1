using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public float jumpForce = 5f;

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    //jump mecahnics
    private int jumpCtr;
    private bool isGrounded;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMove(InputValue movementValue){
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump(InputValue jumpValue){
        if(jumpValue.isPressed){
            if(isGrounded){
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpCtr = 1;
                isGrounded = false;
            }
            else if(jumpCtr < 2){
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpCtr++;
            }
        }
    }

    void FixedUpdate(){

        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "Ground"){
            isGrounded = true;
            jumpCtr = 0;
        }
    }

    void OnCollisionExit(Collision other){
        if(other.gameObject.tag == "Ground"){
            isGrounded = false;
        }
    }
}