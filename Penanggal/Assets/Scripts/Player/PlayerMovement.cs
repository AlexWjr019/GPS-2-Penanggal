using UnityEngine;

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
    public float sensitivity = 2.0f; // 控制相机旋转灵敏度
    public RectTransform uiAreaRect; // 引用UI区域的RectTransform

    private Transform player;
    public UIVirtualJoystick joyStick;

    private Vector2 moveDirection;
    private float targetSpeed = 0.0f;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        player = GetComponent<Transform>();

        joyStick.joystickOutputEvent.AddListener(OnJoystickOutput);
    }

    private void Update()
    {
        Move(moveDirection);
        JumpAndGravity();
        GroundedCheck();
        
    }

    private void FixedUpdate()
    {
        CameraRotation();
    }

    private void GroundedCheck()
    {
            // Set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - _controller.bounds.extents.y - 0.1f, transform.position.z);

        // Check if the player is grounded
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
    }

    private void Move(Vector2 moveDirection)
    {
        // Calculate the actual speed as a combination of target speed and move input magnitude
        float actualSpeed = Mathf.Lerp(_controller.velocity.magnitude, targetSpeed, SpeedChangeRate * Time.deltaTime);

        // Calculate the move direction in world space
        Vector3 moveDirection3D = (transform.forward * moveDirection.y + transform.right * moveDirection.x).normalized;

        // Move the player
        _controller.Move(moveDirection3D * actualSpeed * Time.deltaTime + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }

    private void OnJoystickOutput(Vector2 output)
    {
        moveDirection = output; // 更新移动方向

        float joystickMagnitude = moveDirection.magnitude;

        // 如果虚拟摇杆的拉动范围大于等于1，表示完全拉动，使用 SprintSpeed；否则使用 MoveSpeed。
        targetSpeed = joystickMagnitude >= 1f ? SprintSpeed : MoveSpeed;
    }

    private void JumpAndGravity()
    {
        if (Grounded)
        {
            // Reset the fall timeout timer
            _fallTimeoutDelta = FallTimeout;

            // Stop our velocity dropping infinitely when grounded
            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }

            // Jump
            if (Input.GetButtonDown("Jump") && _jumpTimeoutDelta <= 0.0f)
            {
                // The square root of H * -2 * G = how much velocity needed to reach desired height
                _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
            }

            // Jump timeout
            if (_jumpTimeoutDelta >= 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            // Reset the jump timeout timer
            _jumpTimeoutDelta = JumpTimeout;

            // Fall timeout
            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }
        }

        // Apply gravity over time if under terminal velocity
        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += Gravity * Time.deltaTime;
        }
    }

    private void CameraRotation()
    {
        // CameraFollowTouch logic
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case UnityEngine.TouchPhase.Began:
                    // Check if the touch is within the UI area
                    if (IsTouchInUIArea(touch.position))
                    {
                        // Optionally, you can reset rotation.x and rotation.y here if needed.
                    }
                    break;

                case UnityEngine.TouchPhase.Moved:
                    // Check if the touch is within the UI area
                    if (IsTouchInUIArea(touch.position))
                    {
                        // Get the distance of touch swipe
                        Vector2 deltaPosition = touch.deltaPosition;

                        // Calculate camera rotation based on touch swipe
                        rotation.x += deltaPosition.x * sensitivity;
                        rotation.y -= deltaPosition.y * sensitivity;

                        // Use Mathf.Clamp to limit the rotation angles to prevent the camera from going out of bounds
                        rotation.y = Mathf.Clamp(rotation.y, -180f, 180f);

                        // Apply the rotation to the camera and player objects
                        transform.localRotation = Quaternion.Euler(rotation.y, rotation.x, 0);
                        player.rotation = Quaternion.Euler(0, rotation.x, 0);
                    }
                    break;
            }
            Debug.Log(Input.touchCount);
        }

        if (Input.touchCount == 2)
        {
            Touch touch2 = Input.GetTouch(1);

            switch (touch2.phase)
            {
                case UnityEngine.TouchPhase.Began:
                    // Check if the touch is within the UI area
                    if (IsTouchInUIArea(touch2.position))
                    {
                        // Optionally, you can reset rotation.x and rotation.y here if needed.
                    }
                    break;

                case UnityEngine.TouchPhase.Moved:
                    // Check if the touch is within the UI area
                    if (IsTouchInUIArea(touch2.position))
                    {
                        // Get the distance of touch swipe
                        Vector2 deltaPosition = touch2.deltaPosition;

                        // Calculate camera rotation based on touch swipe
                        rotation.x += deltaPosition.x * sensitivity;
                        rotation.y -= deltaPosition.y * sensitivity;

                        // Use Mathf.Clamp to limit the rotation angles to prevent the camera from going out of bounds
                        rotation.y = Mathf.Clamp(rotation.y, -180f, 180f);

                        // Apply the rotation to the camera and player objects
                        transform.localRotation = Quaternion.Euler(rotation.y, rotation.x, 0);
                        player.rotation = Quaternion.Euler(0, rotation.x, 0);
                    }
                    break;
            }
            Debug.Log(Input.touchCount);
        }
    }

    private bool IsTouchInUIArea(Vector2 touchPosition)
    {
        if (uiAreaRect == null)
        {
            return false; // If the UI area is not specified, return false by default
        }

        // Check if the touch position is within the UI area
        return RectTransformUtility.RectangleContainsScreenPoint(uiAreaRect, touchPosition);
    }

}
