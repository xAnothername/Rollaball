using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public float jumpForce = 0;
    public float gravity = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private float movementZ;
    private int jump;
    private float activeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementZ = movementVector.y;
    }

    void OnJump()
    {
        if(rb.transform.position.y <= 0.51)
        {
            movementY = jumpForce;
            jump = 1;
        }
        else if (jump <= 2)
        {
            movementY = jumpForce + 3;
            jump += 1;
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {   
        if (rb.transform.position.y > 0.5)
        {
            activeSpeed = speed - 2;
        } else activeSpeed = speed;
        movementY -= gravity;
        Vector3 movement = new Vector3(movementX, movementY, movementZ);
        rb.AddForce(movement * activeSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
        }
    }
}
