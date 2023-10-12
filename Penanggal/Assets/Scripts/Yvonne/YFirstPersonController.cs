using UnityEngine;

public class YFirstPersonController : MonoBehaviour
{
    #region References
    public Transform cameraTransform;
    public CharacterController characterController;
    public Transform groundCheck;
    public LayerMask groundLayers;
    #endregion

    #region Player Settings
    public float cameraSensitivity;
    public float moveSpeed;
    public float moveInputDeadZone;
    public bool keyboard = false;
    #endregion

    #region Touch Detection Variables
    int leftFingerId, rightFingerId;
    float halfScreenWidth;
    #endregion

    #region Camera Control
    Vector2 lookInput;
    float cameraPitch;

    #endregion

    #region Player Movement
    Vector2 moveTouchStartPosition;
    Vector2 moveInput;
    #endregion

    #region Gravity & Jumping
    public float groundedGravity = 10;
    public float airborneGravity = 10;
    private float verticalVelocity;
    #endregion

    #region Ground Check
    private float groundCheckRadius = 0.1f;
    private bool grounded;
    #endregion

    void Start()
    {
        // id = -1 means the finger is not being tracked
        leftFingerId = -1;
        rightFingerId = -1;

        // only calculate once
        halfScreenWidth = Screen.width / 2;

        // calculate the movement input dead zone
        moveInputDeadZone = Mathf.Pow(Screen.height / moveInputDeadZone, 2);
    }

    void Update()
    {
        if(!keyboard)
        {
            GetTouchInput();
        }
        else
        {
            //keyboard controls;
        }
        Gravity();

        if(rightFingerId != -1)
        {
            LookAround();
        }

        if(leftFingerId != -1)
        {
            Move();
        }
    }

    void FixedUpdate()
    {
        grounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayers);
    }

    void GetTouchInput()
    {
        // iterate through all the detected touches
        for (int touches = 0; touches < Input.touchCount; touches++)
        {
            Touch t = Input.GetTouch(touches);

            // check each touch's phase
            switch (t.phase)
            {
                case TouchPhase.Began:
                    if (t.position.x < halfScreenWidth && leftFingerId == -1)
                    {
                        // start tracking the left finger if it was not previously being tracked
                        leftFingerId = t.fingerId;

                        // set the start position for the movement control finger
                        moveTouchStartPosition = t.position;
                        Debug.Log("Tracking left finger");
                    }
                    else if (t.position.x > halfScreenWidth && rightFingerId == -1)
                    {
                        rightFingerId = t.fingerId;
                        Debug.Log("Tracking right finger");
                    }
                    break;

                case TouchPhase.Ended:

                case TouchPhase.Canceled:
                    if (t.fingerId == leftFingerId)
                    {
                        // stop tracking the left finger
                        leftFingerId = -1;
                        Debug.Log("Stopped tracking left finger");
                    }
                    else if (t.fingerId == rightFingerId)
                    {
                        // stop tracking the right finger
                        rightFingerId = -1;
                        Debug.Log("Stopped tracking right finger");
                    }
                    break;

                case TouchPhase.Moved:
                    //get input for looking around
                    if(t.fingerId == rightFingerId)
                    {
                        lookInput = t.deltaPosition * cameraSensitivity * Time.deltaTime;
                    }
                    else if(t.fingerId == leftFingerId)
                    {
                        // calculating the postion delta from the start position
                        moveInput = t.position - moveTouchStartPosition;
                    }
                    break;

                case TouchPhase.Stationary:
                    // set the look input to zero if the finger is still
                    if(t.fingerId == rightFingerId)
                    {
                        lookInput = Vector2.zero;
                    }
                    break;
            }
        }
    }

    void LookAround()
    {
        // vertical (pitch) rotation
        cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);

        // horizontal (yaw) rotation
        transform.Rotate(transform.up, lookInput.x);
    }

    void Move()
    {
        // don't move if the touch delta is shorter than the designated dead zone
        if (moveInput.sqrMagnitude <= moveInputDeadZone) return;

        // multiply the normalized direction by the speed
        Vector2 movementDirection = moveInput.normalized * moveSpeed * Time.deltaTime;

        // move relatively to the local transform's direction
        characterController.Move(transform.right * movementDirection.x + transform.forward * movementDirection.y);
    }

    void GetKeyboardInput()
    {

    }

    void Gravity()
    {
        // calculate y (vertical) movement
        if (grounded && verticalVelocity <= 0)
        {
            verticalVelocity = -groundedGravity * Time.deltaTime;
        }
        else
        {
            verticalVelocity -= groundedGravity * Time.deltaTime;
        }

        // apply y (vertical) movement
        Vector3 verticalMovement = transform.up * verticalVelocity;
        characterController.Move(verticalMovement * Time.deltaTime);
    }
}
