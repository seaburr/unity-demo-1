using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool isSpacePressed;
    private float horizontalInput;
    private Rigidbody rigidBodyComponent;
    private TextMesh playerScoreText;
    private int playerScore;
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
        playerScoreText = GameObject.Find("PlayerScore").GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isSpacePressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
        playerScoreText.text = playerScore.ToString();
    }

    // FixedUpdate is called once every physics update
    private void FixedUpdate()
    {

        rigidBodyComponent.velocity = new Vector3(horizontalInput, rigidBodyComponent.velocity.y, 0);

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }
        if (isSpacePressed)
        {
            rigidBodyComponent.AddForce(Vector3.up * 6.5f, ForceMode.VelocityChange);
            isSpacePressed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
            playerScore++;
        }
    }

    // private void OnCollisionEnter()
    // {
    //     isOnGround = true;
    // }

    // private void OnCollisionExit()
    // {
    //     isOnGround = false;
    // }

}