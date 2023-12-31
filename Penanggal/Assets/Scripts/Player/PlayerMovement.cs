using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Security.Authentication.ExtendedProtection;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 3.0f;
    public float SprintSpeed = 6.0f;
    public float SpeedChangeRate = 10.0f;
    float SomeMaximumSpeedValue;
    float NormalMaximumSpeedValue = 10.0f;
    float SprintingMaximumSpeedValue = 15.0f;

    //
    public float JumpHeight = 1.2f;
    public float Gravity = -15.0f;
    //

    public float JumpTimeout = 0.1f;
    public float FallTimeout = 0.15f;

    private CharacterController controller;

    public bool Grounded = true;
    public float GroundedRadius = 0.5f;
    public LayerMask GroundLayers;

    private const float _threshold = 0.01f;
    private float _verticalVelocity;

    private float _terminalVelocity = 53.0f;

    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;

    private Vector2 rotation = Vector2.zero;
    //public float sensitivity = 2.0f;
    public RectTransform uiAreaRect;
    private Vector2 touchDeltaPosition;
    private bool isTouchActive;

    private Transform player;
    public UIVirtualJoystick joyStick;

    private Vector2 moveDirection;
    private float targetSpeed = 0.0f;


    [Header("Stamina Settings")]
    public float stamina;
    public float MaxStamina = 100f;
    public float StaminaDecreaseRate = 10f;
    public float StaminaRecoveryRate = 5f;
    private bool IsRunning = false;
    public Image StaminaBar;
    public bool AutoRun = false;

    private Vector2 _input;
    private float _cinemachineTargetPitch;
    private float _rotationVelocity;
    public float RotationSpeed = 0.025f;
    public float BottomClamp = -80.0f;
    public float TopClamp = 80.0f;
    public Transform CinemachineCameraTarget;
    public Collider2D RunZoneCollider2D;

    public static bool isMove;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        player = GetComponent<Transform>();

        stamina = MaxStamina;
        joyStick.joystickOutputEvent.AddListener(OnJoystickOutput);
    }

    private void Update()
    {
        #region
        //Incase after poc need use back
        //if (AutoRun)
        //{
        //    moveDirection = Vector2.up;
        //}
        #endregion

        Move(moveDirection);
        JumpAndGravity();
        GroundedCheck();
        StaminaManagement();
    }

    private void FixedUpdate()
    {
        CameraRotation();
        StaminaManagement();
    }

    private void GroundedCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - controller.bounds.extents.y - 5f, transform.position.z);

        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
    }

    private void Move(Vector2 moveDirection)
    {
        if (IsRunning)
        {
            targetSpeed = SprintSpeed;
            SomeMaximumSpeedValue = SprintingMaximumSpeedValue;
        }
        else
        {
            targetSpeed = MoveSpeed;
            SomeMaximumSpeedValue = NormalMaximumSpeedValue;
        }

        float actualSpeed = Mathf.Lerp(controller.velocity.magnitude, targetSpeed, SpeedChangeRate * Time.deltaTime);
        actualSpeed = Mathf.Clamp(actualSpeed, 0, SomeMaximumSpeedValue);

        //Debug.Log("Actual Speed: " + actualSpeed + ", Target Speed: " + targetSpeed);

        Vector3 moveDirection3D = (transform.forward * moveDirection.y + transform.right * moveDirection.x).normalized;
        controller.Move(moveDirection3D * actualSpeed * Time.deltaTime + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }


    private void StaminaManagement()
    {
        bool isMoving = moveDirection.magnitude > 0.1f;

        if (IsRunning && isMoving && stamina > 0)
        {
            stamina -= StaminaDecreaseRate * Time.deltaTime;
        }
        else if (!IsRunning && isMoving && stamina < MaxStamina)
        {
            stamina += StaminaRecoveryRate * Time.deltaTime;
        }
        else if (!isMoving && stamina < MaxStamina)
        {
            stamina += StaminaRecoveryRate * Time.deltaTime;
        }

        stamina = Mathf.Clamp(stamina, 0, MaxStamina);

        if (stamina <= 0 && IsRunning)
        {
            IsRunning = false;
            AutoRun = false;
            targetSpeed = MoveSpeed;
        }

        UpdateStaminaBar();
    }

    private void UpdateStaminaBar()
    {
        float normalizedStamina = stamina / MaxStamina;
        StaminaBar.fillAmount = normalizedStamina;
    }

    private void OnJoystickOutput(Vector2 output)
    {
        #region
        //if (output.magnitude > 0.1f)
        //{
        //    AutoRun = false;
        //}
        #endregion

        moveDirection = output;

        if (output != null)
        {
            isMove = true;
        }

        float joystickMagnitude = moveDirection.magnitude;
        IsRunning = joyStick.IsRunning && stamina > 1;

        #region
        //if (IsRunning && joystickMagnitude >= 1f)
        //{
        //    AutoRun = true;
        //}
        #endregion

        //if (!AutoRun)
        //{
        targetSpeed = (joystickMagnitude >= 1f && IsRunning) ? SprintSpeed : MoveSpeed;
        //}

    }



    private void JumpAndGravity()
    {
        if (Grounded)
        {
            _fallTimeoutDelta = FallTimeout;

            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }

            if (Input.GetButtonDown("Jump") && _jumpTimeoutDelta <= 0.0f)
            {
                _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
            }

            if (_jumpTimeoutDelta >= 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
            Debug.Log("Vertical Velocity: " + _verticalVelocity + ", Grounded: " + Grounded);
        }
        else
        {
            _jumpTimeoutDelta = JumpTimeout;

            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }
        }

        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += Gravity * Time.deltaTime;
        }
    }


    private void CameraRotation()
    {
        //if (Tutorial.cameraMoving)
        //{
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                if (IsTouchInUIArea(touch.position))
                {
                    Debug.Log("Executing Camera Rotation");

                    if (touch.phase == TouchPhase.Moved)
                    {
                        Vector2 deltaPosition = touch.deltaPosition;

                        float deltaTimeMultiplier = 1.0f;

                        _cinemachineTargetPitch -= deltaPosition.y * RotationSpeed * deltaTimeMultiplier;
                        _rotationVelocity = deltaPosition.x * RotationSpeed * deltaTimeMultiplier;

                        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

                        CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

                        transform.Rotate(Vector3.up * _rotationVelocity);
                    }
                }
            }
        //}
    }


    private bool IsTouchInUIArea(Vector2 touchPosition)
    {
        if (uiAreaRect == null)
        {
            return false;
        }
        return RectTransformUtility.RectangleContainsScreenPoint(uiAreaRect, touchPosition);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
        {
            angle += 360F;
        }
        if (angle > 360F)
        {
            angle -= 360F;
        }
        return Mathf.Clamp(angle, min, max);
    }
}
