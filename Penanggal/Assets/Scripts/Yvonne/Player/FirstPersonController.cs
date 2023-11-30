using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

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
    private float touchStartTime;
    private const float touchDelay = 0.1f;
    private bool canPickUpItem = true;
    public static bool canHide = false;
    public Volume volume;
    Vignette vignette;
    private bool isMobile;
    #endregion

    #region Player and Controller Settings
    [Header("Player and Controller Settings")]
    public float moveSpeed;
    public float sprintSpeed = 10f;
    public float initialMoveSpeed;
    public float moveInputDeadZone;
    public bool keyboard = false;
    public static bool hasCollidedWithGhost = false;
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
    public GameObject volumeObject;
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

    public Camera playerCamera;
    public Camera ghostCamera;
    private bool canMove = true;
    private bool canLookAround = true;
    //[HideInInspector] public bool playerIsDead = false;

    public Animator playerAnimator;
    public Enemy enemy;

    private void Awake()
    {
        //PositionManager.Instance.SetPlayerStartPosition(transform.position);
        //isMobile = Application.isMobilePlatform;
        //if (!isMobile)
        //{
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        //}
    }
    void Start()
    {
        leftFingerId = -1;
        rightFingerId = -1;
        halfScreenWidth = Screen.width / 2;
        initialMoveSpeed = moveSpeed;

        moveInputDeadZone = Mathf.Pow(Screen.height / moveInputDeadZone, 2);

        headBob = GetComponent<HeadBob>();
        if (volume.profile.TryGet(out vignette))
        {
            // Vignette effect found in the volume
        }
        volumeObject.SetActive(true);
        PositionManager.Instance.SetPlayerStartPosition(transform.position);
    }

    void Update()
    {
        if (canMove)
        {
            HandleSprintTimer();

            if (!keyboard)
            {
                GetTouchInput();
            }
            else
            {
                GetKeyboardInput();
                CameraMovement();
            }
            Gravity();
        }

        if (canLookAround && rightFingerId != -1)
        {
            LookAround();
        }

        if (leftFingerId != -1)
        {
            Move();
        }

        if (moveInput.x != 0.0f && leftFingerId != -1)
        {
            if(!Hide.isHide && !Note.noteIsSeen && !Note2.noteIsSeen && !ObjectInteract.picIsRotate && !OpenSafeCode.openSafe && !TornPuzzleControl.tornPuzzleActivated && !Swap.weddingPuzzle && !PuzzleActive.alterPuzzleActivated)
            {
                if (Time.timeSinceLevelLoad - lastPlayedFootstepSoundTime > timeBetweenFootsteps && Hide.isHide == false)
                {
                    FindObjectOfType<AudioManager>().PlaySFX("Footsteps");
                    lastPlayedFootstepSoundTime = Time.timeSinceLevelLoad;
                }
            }

            canHide = false;
        }
        //Debug.Log(hasCollidedWithGhost);
    }
    void FixedUpdate()
    {
        grounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayers);
    }
    void GetTouchInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            canHide = true;
        }
        else
        {
            canHide = false;
        }

        for (int touches = 0; touches < Input.touchCount; touches++)
        {
            Touch t = Input.GetTouch(touches);

            switch (t.phase)
            {
                case TouchPhase.Began:
                    touchStartTime = Time.time;
                    canHide = false;
                    if (t.position.x < halfScreenWidth && leftFingerId == -1)
                    {
                        if (!Hide.isHide && !Note.noteIsSeen && !Note2.noteIsSeen && !ObjectInteract.picIsRotate && !OpenSafeCode.openSafe && !TornPuzzleControl.tornPuzzleActivated && !Swap.weddingPuzzle && !PuzzleActive.alterPuzzleActivated)
                        {
                            leftFingerId = t.fingerId;
                            moveTouchStartPosition = t.position;
                        }
                    }
                    else if (t.position.x > halfScreenWidth && rightFingerId == -1)
                    {
                        rightFingerId = t.fingerId;
                    }
                    break;

                case TouchPhase.Ended:
                    if (Time.time - touchStartTime < touchDelay)
                    {
                        canPickUpItem = true;
                        canHide = true;
                    }
                    else
                    {
                        canPickUpItem = false;
                        canHide = false;
                    }

                    if (t.fingerId == leftFingerId)
                    {
                        leftFingerId = -1;
                        moveSpeed = initialMoveSpeed;
                        moveInput = Vector3.zero;
                        isRecoveringStamina = false;
                    }
                    else if (t.fingerId == rightFingerId)
                    {
                        rightFingerId = -1;
                    }
                    break;

                case TouchPhase.Moved:
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = t.deltaPosition * cameraSensitivity * Time.deltaTime;
                        canHide = false;
                    }
                    if (t.fingerId == leftFingerId)
                    {
                        moveInput = t.position - moveTouchStartPosition;

                        if (moveInput.magnitude > 100f && !isRecoveringStamina)
                        {
                            moveSpeed = sprintSpeed;
                        }
                        else
                        {
                            Debug.Log("walk");
                            moveSpeed = initialMoveSpeed;
                        }
                        canHide = false;
                    }
                    break;

                case TouchPhase.Stationary:
                    if (t.fingerId == rightFingerId)
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
                if (!Hide.isHide || !Note.noteIsSeen || !Note2.noteIsSeen || !ObjectInteract.picIsRotate || !OpenSafeCode.openSafe || !TornPuzzleControl.tornPuzzleActivated || !Swap.weddingPuzzle || !PuzzleActive.alterPuzzleActivated)
                {
                    headBob.isWalking = true;
                }
            }
        }
    }

    void LookAround()
    {
        if (!Hide.isHide && !Note.noteIsSeen && !ObjectInteract.picIsRotate && !OpenSafeCode.openSafe && !Note2.noteIsSeen)
        {
            if (!TornPuzzleControl.tornPuzzleActivated && !Swap.weddingPuzzle && !PuzzleActive.alterPuzzleActivated)
            {
                // vertical (pitch) rotation
                cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y * verticalCameraSensitivity, minClamp, maxClamp);
                cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);

                // horizontal (yaw) rotation
                transform.Rotate(transform.up, lookInput.x);

                canHide = false;
            }
        }
    }
    void Move()
    {
        if(!Hide.isHide && !Note.noteIsSeen && !ObjectInteract.picIsRotate && !OpenSafeCode.openSafe && !Note2.noteIsSeen)
        {
            if (!TornPuzzleControl.tornPuzzleActivated && !Swap.weddingPuzzle && !PuzzleActive.alterPuzzleActivated)
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
        }
    }

    void GetKeyboardInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            moveSpeed = sprintSpeed;
        }
        else
        {
            moveSpeed = initialMoveSpeed;
        }

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    void CameraMovement()
    {
        if (!isMobile)
        {
            float mouseX = Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
            float increasedVerticalSensitivity = verticalCameraSensitivity * 4;
            float mouseY = Input.GetAxis("Mouse Y") * increasedVerticalSensitivity * Time.deltaTime;

            cameraPitch -= mouseY;
            cameraPitch = Mathf.Clamp(cameraPitch, minClamp, maxClamp);

            cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);
            transform.Rotate(Vector3.up * mouseX);
        }
    }

    void Gravity()
    {
        // calculate yRotation (vertical) movement
        if (grounded && verticalVelocity <= 0)
        {
            verticalVelocity = -groundedGravity * Time.deltaTime;
        }
        else
        {
            verticalVelocity -= groundedGravity * Time.deltaTime;
        }

        // apply yRotation (vertical) movement
        Vector3 verticalMovement = transform.up * verticalVelocity;
        controller.Move(verticalMovement * Time.deltaTime);

    }

    //code doesnt run unless player runs into the ai
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if ((hit.gameObject.CompareTag("Ghost") || hit.gameObject.CompareTag("BabyPenanggal")) && !hasCollidedWithGhost)
        {
            hasCollidedWithGhost = true;

            canMove = false;
            canLookAround = false;
            Debug.Log("entered");
            playerCamera.enabled = false;
            //ghostCamera.enabled = true;
            Debug.Log("player cam off");
            LookAtGhost(hit.transform);
            StartCoroutine(ShowLoseUIAfterDelay());
        }
    }

    private void LookAtGhost(Transform ghostTransform)
    {
        Vector3 directionToGhost = (ghostTransform.position - cameraTransform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToGhost);
        cameraTransform.rotation = lookRotation;
        cameraPitch = cameraTransform.localEulerAngles.x;
    }

    private System.Collections.IEnumerator ShowLoseUIAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        if (hasCollidedWithGhost)
        {
            LoseScene loseScene = FindObjectOfType<LoseScene>();
            LoseScene2 loseScene2 = FindObjectOfType<LoseScene2>();
            if (loseScene != null)
            {
                loseScene.PlayerCollidedWithGhost();
            }
            else if (loseScene2 != null)
            {
                loseScene2.PlayerCollidedWithGhost();
            }
            
            // SceneManager.LoadScene("LoseScreen");
        }
    }

    public void HandleSprintTimer()
    {
        if (moveSpeed == sprintSpeed && vignette.smoothness.value < 1)
        {
            sprintTimer += Time.deltaTime;
            if (sprintTimer >= sprintDuration && !isRecoveringStamina)
            {
                moveSpeed = initialMoveSpeed;
                isRecoveringStamina = true;
            }

        }
        if (isRecoveringStamina || moveSpeed == initialMoveSpeed)
        {
            sprintTimer -= Time.deltaTime / staminaRecoveryDuration;
            if (sprintTimer <= 0)
            {
                sprintTimer = 0;
                isRecoveringStamina = false;
                //FindObjectOfType<AudioManager>().StopSFX("HeartBeat");
            }
        }
        if (vignette != null)
        {
            float alpha = Mathf.Clamp(sprintTimer / sprintDuration, 0, 1);
            vignette.smoothness.Override(alpha);
        }
    }

    public void ResetPlayerState()
    {
        Enemy enemy = FindObjectOfType<Enemy>();
        hasCollidedWithGhost = false;
        playerAnimator.SetBool("Dead", false);
        //enemy.playerDied = false;
        canMove = true;
        canLookAround = true;
        enemy.playerDied = false;

        Debug.Log("ResetPlayerState called");
        Debug.Log("canMove: " + canMove);
        Debug.Log("canLookAround: " + canLookAround);

        if (playerCamera != null)
        {
            playerCamera.enabled = true;
        }

        if (ghostCamera != null)
        {
            ghostCamera.enabled = false;
        }
        Debug.Log("playerCamera enabled after reset: " + playerCamera.enabled);
    }

}