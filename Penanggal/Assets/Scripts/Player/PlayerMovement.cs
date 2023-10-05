using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 4.0f;
    public float SprintSpeed = 6.0f;
    public float SpeedChangeRate = 10.0f;

    public float JumpHeight = 1.2f;
    public float Gravity = -15.0f;

    public float JumpTimeout = 0.1f;
    public float FallTimeout = 0.15f;

    private CharacterController _controller;

    public bool Grounded = true;
    public float GroundedRadius = 0.5f;
    public LayerMask GroundLayers;

    private const float _threshold = 0.01f;
    private float _verticalVelocity;

    private float _terminalVelocity = 53.0f;

    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;

    private Vector2 rotation = Vector2.zero;
    public float sensitivity = 2.0f;
    public RectTransform uiAreaRect;

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


    // Add your input and camera control variables here
    private Vector2 _input;
    private float _cinemachineTargetPitch;
    private float _rotationVelocity;
    public float RotationSpeed = 2.0f;
    public float BottomClamp = -80.0f;
    public float TopClamp = 80.0f;
    public Transform CinemachineCameraTarget;
    public Collider2D RunZoneCollider2D;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        player = GetComponent<Transform>();

        stamina = MaxStamina;
        joyStick.joystickOutputEvent.AddListener(OnJoystickOutput);
    }

    private void Update()
    {
        if (AutoRun)
        {
            moveDirection = Vector2.up;
        }

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
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - _controller.bounds.extents.y - 0.1f, transform.position.z);

        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
    }

    private void Move(Vector2 moveDirection)
    {
        if (AutoRun && IsRunning && stamina > 0)
        {
            targetSpeed = SprintSpeed;
        }

        float actualSpeed = Mathf.Lerp(_controller.velocity.magnitude, targetSpeed, SpeedChangeRate * Time.deltaTime);

        Vector3 moveDirection3D = (transform.forward * moveDirection.y + transform.right * moveDirection.x).normalized;

        _controller.Move(moveDirection3D * actualSpeed * Time.deltaTime + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
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
            AutoRun = false; // 体力耗尽时停止自动奔跑
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
        // 这里我们检查摇杆是否有输出，如果有，我们将AutoRun设为false
        // 这样当摇杆被操作时，角色将退出自动奔跑模式
        if (output.magnitude > 0.1f)
        {
            AutoRun = false;
        }

        moveDirection = output;

        float joystickMagnitude = moveDirection.magnitude;
        IsRunning = joyStick.IsRunning && stamina > 1;

        // 如果我们在摇杆的"奔跑"区域并且体力充足，AutoRun将被设置为true
        if (IsRunning && joystickMagnitude >= 1f)
        {
            AutoRun = true;
        }

        // 如果AutoRun是false，我们根据摇杆输入来设定速度。
        // 否则，角色将继续以奔跑速度前进（在Move函数中处理）
        if (!AutoRun)
        {
            targetSpeed = (joystickMagnitude >= 1f && IsRunning) ? SprintSpeed : MoveSpeed;
        }
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
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Check the phase of the touch
            if (touch.phase == TouchPhase.Moved && IsTouchInUIArea(touch.position))
            {
                // Get the delta position of the touch
                Vector2 deltaPosition = touch.deltaPosition;

                // Don't multiply touch input by Time.deltaTime
                float deltaTimeMultiplier = 1.0f;

                // Calculate pitch and yaw based on touch input
                _cinemachineTargetPitch -= deltaPosition.y * RotationSpeed * deltaTimeMultiplier;
                _rotationVelocity = deltaPosition.x * RotationSpeed * deltaTimeMultiplier;

                // Clamp pitch rotation
                _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

                // Update Cinemachine camera player pitch
                CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

                // Rotate the player left and right
                transform.Rotate(Vector3.up * _rotationVelocity);
            }
        }
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

