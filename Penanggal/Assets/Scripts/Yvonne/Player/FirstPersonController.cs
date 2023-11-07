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


    private void Awake()
    {
        //PositionManager.Instance.SetPlayerStartPosition(transform.position);
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

        if (rightFingerId != -1)
        {
            LookAround();
        }

        if (leftFingerId != -1)
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
            canHide = false;
        }
        //Debug.Log(moveInput.sqrMagnitude);
    }
    void FixedUpdate()
    {
        grounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayers);
    }
    void GetTouchInput()
    {
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
                        leftFingerId = t.fingerId;
                        moveTouchStartPosition = t.position;
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
                    if (!Swap.weddingPuzzle)
                    {
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
                                moveSpeed = initialMoveSpeed;
                            }
                            canHide = false;
                        }
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
                if (!Hide.isHide && !Note.noteIsSeen)
                {
                    headBob.isWalking = true;
                }
            }
        }
    }

    void LookAround()
    {
        if (!Hide.isHide && !Note.noteIsSeen && !ObjectInteract.picIsRotate && !OpenSafeCode.openSafe)
        {
            if (!TornPuzzleControl.tornPuzzleActivated && !Swap.weddingPuzzle)
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
        if(!Hide.isHide && !Note.noteIsSeen && !ObjectInteract.picIsRotate && !OpenSafeCode.openSafe)
        {
            if (!TornPuzzleControl.tornPuzzleActivated && !Swap.weddingPuzzle)
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

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * moveSpeed * Time.deltaTime);
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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("Collided with: " + hit.gameObject.name);

        if (hit.gameObject.CompareTag("Ghost"))
        {
            if (hit.gameObject.CompareTag("Ghost"))
            {
                LookAtGhost(hit.transform);
                StartCoroutine(ShowLoseUIAfterDelay());
            }
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
        yield return new WaitForSeconds(2.0f);
        LoseScene loseScene = FindObjectOfType<LoseScene>();
        loseScene.PlayerCollidedWithGhost();
        //SceneManager.LoadScene("LoseScreen");
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
            //FindObjectOfType<AudioManager>().PlaySFX("HeartBeat");
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

}