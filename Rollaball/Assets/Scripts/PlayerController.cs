using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public float jumpForce = 5f;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    //jump mecahnics
    private int jumpCtr;
    private bool isGrounded;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();

        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue){
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText(){
        countText.text = "Count: " + count.ToString();
        if(count >= 12){
            winTextObject.SetActive(true);
        }
    }

    void OnJump(InputValue jumpValue){
        if(jumpValue.isPressed){
            if(isGrounded){
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpCtr = 1;
                isGrounded = false;
            }
            else if(jumpCtr < 2){
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpCtr++;
            }
        }
    }

    void FixedUpdate(){

        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("PickUp")){
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.CompareTag("Ground")){
            isGrounded = true;
            jumpCtr = 0;
        }
    }

    void OnCollisionExit(Collision other){
        if(other.gameObject.CompareTag("Ground")){
            isGrounded = false;
        }
    }
}