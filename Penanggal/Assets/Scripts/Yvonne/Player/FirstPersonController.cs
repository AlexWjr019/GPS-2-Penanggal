using UnityEngine;
using UnityEngine.UI;

public class FirstPersonController : MonoBehaviour
{
    #region References
    [Header("References")]
    public CharacterController controller;
    public Transform cameraTransform;
    public Transform groundCheck;
    public LayerMask groundLayers;
    public Inventory inventory;
    private HeadBob headBob;
    #endregion

    #region Player and Controller Settings
    [Header("Player and Controller Settings")]
    public float moveSpeed;
    public float sprintSpeed = 10f;
    public float initialMoveSpeed;
    public float moveInputDeadZone;
    public bool keyboard = false;
    #endregion

    #region Touch Detection Variables
    int leftFingerId, rightFingerId;
    float halfScreenWidth;
    #endregion

    #region Camera Control
    [Header("Camera Control")]
    [Range(0f, 50f)]
    public float cameraSensitivity;
    public float verticalCameraSensitivity;
    public float maxClamp;
    public float minClamp;
    Vector2 lookInput;
    float cameraPitch;
    #endregion

    #region Sprint and Stamina
    public float sprintDuration = 4f;
    private float sprintTimer = 0f;
    public float staminaRecoveryDuration = 3f;
    public float staminaRecoveryThreshold = 1f;
    private bool isRecoveringStamina = false;
    public Image leftBlackoutImage;
    public Image rightBlackoutImage;
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

    #region Player Audio
    const float timeBetweenFootsteps = 0.7f;
    float lastPlayedFootstepSoundTime = -timeBetweenFootsteps;
    #endregion

    void Start()
    {
        leftFingerId = -1;
        rightFingerId = -1;
        halfScreenWidth = Screen.width / 2;
        initialMoveSpeed = moveSpeed;

        moveInputDeadZone = Mathf.Pow(Screen.height / moveInputDeadZone, 2);

        headBob = GetComponent<HeadBob>();

        inventory.ItemUsed += Inventory_ItemUsed;
    }

    private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
        IItems item = e.Item;

        // do something with the item eg put it to the hand of the player
    }

    void Update()
    {
        HandleSprintTimer();
        if (!keyboard)
        {
            GetTouchInput();
        }
        else
        {
            GetKeyboardInput();
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

        if (moveInput.x != 0.0f && leftFingerId != -1)
        {
            if (Time.timeSinceLevelLoad - lastPlayedFootstepSoundTime > timeBetweenFootsteps)
            {
                FindObjectOfType<AudioManager>().PlaySFX("Footsteps");
                lastPlayedFootstepSoundTime = Time.timeSinceLevelLoad;
            }
        }
        Debug.Log(moveInput.sqrMagnitude);
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
                        //Debug.Log("Tracking left finger");
                    }
                    else if (t.position.x > halfScreenWidth && rightFingerId == -1)
                    {
                        rightFingerId = t.fingerId;
                        //Debug.Log("Tracking right finger");
                    }
                    break;
                     
                case TouchPhase.Ended:

                case TouchPhase.Canceled:
                    if (t.fingerId == leftFingerId)
                    {
                        // stop tracking the left finger
                        leftFingerId = -1;
                        moveSpeed = initialMoveSpeed;
                        moveInput = Vector3.zero;
                    }
                    else if (t.fingerId == rightFingerId)
                    {
                        // stop tracking the right finger
                        rightFingerId = -1;
                    }
                    break;

                case TouchPhase.Moved:
                    //get input for looking around
                    if(t.fingerId == rightFingerId)
                    {
                        lookInput = t.deltaPosition * cameraSensitivity * Time.deltaTime;
                    }
                    if(t.fingerId == leftFingerId)
                    {
                        // calculating the postion delta from the start position
                        moveInput = t.position - moveTouchStartPosition;

                        if (moveInput.magnitude > 200f)
                        {
                            moveSpeed = sprintSpeed; 
                        }
                        else
                        {
                            moveSpeed = initialMoveSpeed;
                        }
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
            if (moveInput.sqrMagnitude <= 0)
            {
                headBob.isWalking = false;
            }
            else
            {
                headBob.isWalking = true;
            }
        }
    }

    void LookAround()
    {
        // vertical (pitch) rotation
        cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y * verticalCameraSensitivity, minClamp, maxClamp);
        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);

        // horizontal (yaw) rotation
        transform.Rotate(transform.up, lookInput.x);
    }
    void Move()
    {
        // don't move if the touch delta is shorter than the designated dead zone
        if (moveInput.sqrMagnitude <= moveInputDeadZone)
        {
            return;
        }

        // multiply the normalized direction by the speed
        Vector2 movementDirection = moveInput.normalized * moveSpeed * Time.deltaTime;

        // move relatively to the local transform's direction
        controller.Move(transform.right * movementDirection.x + transform.forward * movementDirection.y);
    }
    void GetKeyboardInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * moveSpeed * Time.deltaTime);
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
        controller.Move(verticalMovement * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        IItems item = hit.collider.GetComponent<IItems>();
        if(item != null)
        {
            inventory.AddItem(item);
        }
    }

    public void HandleSprintTimer()
    {
        if (moveSpeed == sprintSpeed)
        {
            sprintTimer += Time.deltaTime;
            if (sprintTimer >= sprintDuration && !isRecoveringStamina)
            {
                moveSpeed = initialMoveSpeed;
                isRecoveringStamina = true;
            }
        }
        if (isRecoveringStamina)
        {
            sprintTimer -= Time.deltaTime / staminaRecoveryDuration;
            if (sprintTimer <= 0)
            {
                sprintTimer = 0;
                isRecoveringStamina = false;
            }
            if (sprintTimer <= sprintDuration - staminaRecoveryThreshold)
            {
                if (moveInput.magnitude > 100f)
                {
                    moveSpeed = sprintSpeed;
                }
            }
        }
        float alpha = sprintTimer / sprintDuration;
        Color fadeColor = new Color(0, 0, 0, alpha);
        leftBlackoutImage.color = fadeColor;
        rightBlackoutImage.color = fadeColor;
    }
}

//it will move forward/backwards after you've already walked when u tap tap - i think needs to reset position when u let go of your finger
//need to add sprint to joystick
//stamina bar