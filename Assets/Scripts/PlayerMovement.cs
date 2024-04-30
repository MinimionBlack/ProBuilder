using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 10f;
    public float momentumDamping = 5f;
    private CharacterController myCC;
    public Animator camAnim;
    private bool isWalking;

    UnityEngine.Vector3 inputVector;
    UnityEngine.Vector3 movementVector;
    private float myGravity = -10f;
    void Start()
    {
        myCC = GetComponent<CharacterController>();
    }


    void Update()
    {
        GetInput();
        MovePlayer();


    }
    void GetInput()
    {
        // if we're holding down wasd, then give us -1, 0, 1
        if (UnityEngine.Input.GetKey(KeyCode.W) ||
           UnityEngine.Input.GetKey(KeyCode.A) ||
           UnityEngine.Input.GetKey(KeyCode.S) ||
           UnityEngine.Input.GetKey(KeyCode.D))
        {
            inputVector = new Vector3(x: UnityEngine.Input.GetAxisRaw("Horizontal"), y: 0f, z: UnityEngine.Input.GetAxisRaw("Vertical"));
            inputVector.Normalize();
            inputVector = transform.TransformDirection(inputVector);

            isWalking = true;
        }
        else
        {
            inputVector = Vector3.Lerp(inputVector, Vector3.zero, momentumDamping * Time.deltaTime);

            isWalking = false;
        }

        // if we're not then give us whatever inputVector was at when it was last checked and lerp it towards zero
        movementVector = (inputVector * playerSpeed) + (Vector3.up * myGravity);
    }
    void MovePlayer()
    {
        myCC.Move(movementVector * Time.deltaTime);
    }


}
