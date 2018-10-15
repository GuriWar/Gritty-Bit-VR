using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Transform camera;
    [SerializeField]
    Transform controllingHand;
    Vector3 cameraInitialPosition;
    public float moveSpeed;
    float speedMultiplier;
    [SerializeField]
    float boostMoveSpeed;
    [SerializeField]
    float rotateDegrees;
    [SerializeField]
    float rotateSpeed;
    Vector3 targetRotation;
    float yOffset;
    float speedX;
    float speedY;
    float speedZ;
    float distanceToGround;
    bool spinning;
    bool turningLeft;
    // Use this for initialization
    void Start()
    {
        setMoveSpeed(0);
        distanceToGround = GetComponent<Collider>().bounds.extents.y;
        StartCoroutine(StartCalibrateCenter());
    }

    IEnumerator StartCalibrateCenter()
    {
        yield return new WaitForSeconds(.5f);
        ChangeCameraInitialPos();
        setMoveSpeed(moveSpeed);
    }
    // Update is called once per frame
    void Update()
    {
        spinning = true;
        CalculateSpeedAndDirection();
        Vector3 moveVelocity = new Vector3(speedX, 0, speedZ);
        moveVelocity = Quaternion.Euler(0, camera.localEulerAngles.y, 0) * moveVelocity;
        GetComponent<Rigidbody>().velocity = moveVelocity;

        transform.eulerAngles = Vector3.RotateTowards(transform.eulerAngles, targetRotation, Time.deltaTime * rotateSpeed, rotateSpeed * Time.deltaTime);
        if ((transform.eulerAngles - targetRotation).magnitude < 1)
        {
            transform.eulerAngles = targetRotation;
            spinning = false;
        }

        if ((transform.eulerAngles - targetRotation).magnitude >= 360)
        {
            if (turningLeft)
            {
                targetRotation = new Vector3(0, 270, 0);
            }
            else
            {
                transform.eulerAngles = Vector3.zero;
                targetRotation = Vector3.zero;
            }
            spinning = false;
        }
    }

    void CalculateSpeedAndDirection()
    {
        //get the y angle on the hand, make a vector out of xchange and ychange and rotate the vector by the y value
        float zChange = 0;
        float xChange = 0;
        if (Input.GetAxisRaw("LeftJoystick_Vertical") != 0)
            zChange = Input.GetAxisRaw("LeftJoystick_Vertical");
        if (Input.GetAxisRaw("LeftJoystick_Horizontal") != 0)
            xChange = Input.GetAxisRaw("LeftJoystick_Horizontal");
        //after getting x and z change, rotate by 
        speedZ = zChange * speedMultiplier;
        speedX = xChange * speedMultiplier;
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 1f);
    }

    public void setMoveSpeed(float speed)
    {
        speedMultiplier = speed;
    }

    public void ChangeCameraInitialPos()
    {
        cameraInitialPosition = camera.localPosition;
    }
    public void RotatePlayer(bool left)
    {
        //if (!spinning)
        //{
        //    turningLeft = left;
        //    if (left)
        //        targetRotation.y -= rotateDegrees;
        //    else
        //        targetRotation.y += rotateDegrees;
        //}
    }
}
