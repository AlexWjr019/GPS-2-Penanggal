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
        //Incase after poc need use back
        //if (AutoRun)
        //{
        //    moveDirection = Vector2.up;
        //}

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
        //if (output.magnitude > 0.1f)
        //{
        //    AutoRun = false;
        //}

        moveDirection = output;

        float joystickMagnitude = moveDirection.magnitude;
        IsRunning = joyStick.IsRunning && stamina > 1;

        //if (IsRunning && joystickMagnitude >= 1f)
        //{
        //    AutoRun = true;
        //}

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

            if (touch.phase == TouchPhase.Moved && IsTouchInUIArea(touch.position))
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

