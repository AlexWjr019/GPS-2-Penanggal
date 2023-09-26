//using UnityEngine;

//public class PlayerMovement : MonoBehaviour
//{
//    public float MoveSpeed = 4.0f;
//    public float SprintSpeed = 6.0f;
//    public float SpeedChangeRate = 10.0f;

//    public float JumpHeight = 1.2f;
//    public float Gravity = -15.0f;

//    public float JumpTimeout = 0.1f;
//    public float FallTimeout = 0.15f;

//    private CharacterController _controller;

//    public bool Grounded = true;
//    public float GroundedRadius = 0.5f;
//    public LayerMask GroundLayers;

//    private const float _threshold = 0.01f;
//    private float _verticalVelocity;

//    private float _terminalVelocity = 53.0f;

//    private float _jumpTimeoutDelta;
//    private float _fallTimeoutDelta;

//    private Vector2 rotation = Vector2.zero;
//    public float sensitivity = 2.0f;
//    public RectTransform uiAreaRect;

//    private Transform player;
//    public UIVirtualJoystick joyStick;

//    private Vector2 moveDirection;
//    private float targetSpeed = 0.0f;

//    private void Start()
//    {
//        _controller = GetComponent<CharacterController>();
//        player = GetComponent<Transform>();

//        joyStick.joystickOutputEvent.AddListener(OnJoystickOutput);
//    }

//    private void Update()
//    {
//        Move(moveDirection);
//        JumpAndGravity();
//        GroundedCheck();

//    }

//    private void FixedUpdate()
//    {
//        CameraRotation();
//    }

//    private void GroundedCheck()
//    {
//        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - _controller.bounds.extents.y - 0.1f, transform.position.z);

//        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
//    }

//    private void Move(Vector2 moveDirection)
//    {
//        float actualSpeed = Mathf.Lerp(_controller.velocity.magnitude, targetSpeed, SpeedChangeRate * Time.deltaTime);

//        Vector3 moveDirection3D = (transform.forward * moveDirection.y + transform.right * moveDirection.x).normalized;

//        _controller.Move(moveDirection3D * actualSpeed * Time.deltaTime + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
//    }

//    private void OnJoystickOutput(Vector2 output)
//    {
//        moveDirection = output;

//        float joystickMagnitude = moveDirection.magnitude;

//        targetSpeed = joystickMagnitude >= 1f ? SprintSpeed : MoveSpeed;
//    }

//    private void JumpAndGravity()
//    {
//        if (Grounded)
//        {
//            _fallTimeoutDelta = FallTimeout;

//            if (_verticalVelocity < 0.0f)
//            {
//                _verticalVelocity = -2f;
//            }

//            if (Input.GetButtonDown("Jump") && _jumpTimeoutDelta <= 0.0f)
//            {
//                _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
//            }

//            if (_jumpTimeoutDelta >= 0.0f)
//            {
//                _jumpTimeoutDelta -= Time.deltaTime;
//            }
//        }
//        else
//        {
//            _jumpTimeoutDelta = JumpTimeout;

//            if (_fallTimeoutDelta >= 0.0f)
//            {
//                _fallTimeoutDelta -= Time.deltaTime;
//            }
//        }

//        if (_verticalVelocity < _terminalVelocity)
//        {
//            _verticalVelocity += Gravity * Time.deltaTime;
//        }
//    }

//    private void CameraRotation()
//    {
//        if (Input.touchCount == 1)
//        {
//            Touch touch = Input.GetTouch(0);

//            switch (touch.phase)
//            {
//                case UnityEngine.TouchPhase.Began:
//                    if (IsTouchInUIArea(touch.position))
//                    {

//                    }
//                    break;

//                case UnityEngine.TouchPhase.Moved:
//                    if (IsTouchInUIArea(touch.position))
//                    {
//                        Vector2 deltaPosition = touch.deltaPosition;

//                        rotation.x += deltaPosition.x * sensitivity;
//                        rotation.y -= deltaPosition.y * sensitivity;

//                        //rotation.y = Mathf.Clamp(rotation.y, -90f, 90f);

//                        Debug.Log("Rotation X: " + rotation.x);
//                        Debug.Log("Rotation Y: " + rotation.y);

//                        transform.localRotation = Quaternion.Euler(rotation.y, rotation.x, 0);
//                        player.rotation = Quaternion.Euler(0, rotation.x, 0);
//                    }
//                    break;
//            }
//            Debug.Log(Input.touchCount);
//        }

//        if (Input.touchCount == 2)
//        {
//            Touch touch2 = Input.GetTouch(1);

//            switch (touch2.phase)
//            {
//                case UnityEngine.TouchPhase.Began:
//                    if (IsTouchInUIArea(touch2.position))
//                    {

//                    }
//                    break;

//                case UnityEngine.TouchPhase.Moved:
//                    if (IsTouchInUIArea(touch2.position))
//                    {
//                        Vector2 deltaPosition = touch2.deltaPosition;

//                        rotation.x += deltaPosition.x * sensitivity;
//                        rotation.y -= deltaPosition.y * sensitivity;

//                        //rotation.y = Mathf.Clamp(rotation.y, -90f, 90f);

//                        Debug.Log("Rotation X: " + rotation.x);
//                        Debug.Log("Rotation Y: " + rotation.y);

//                        transform.localRotation = Quaternion.Euler(rotation.y, rotation.x, 0);
//                        player.rotation = Quaternion.Euler(0, rotation.x, 0);
//                    }
//                    break;
//            }
//            Debug.Log(Input.touchCount);
//        }
//    }

//    private bool IsTouchInUIArea(Vector2 touchPosition)
//    {
//        if (uiAreaRect == null)
//        {
//            return false;
//        }
//        return RectTransformUtility.RectangleContainsScreenPoint(uiAreaRect, touchPosition);
//    }

//}

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
    public float sensitivity = 2.0f;
    public RectTransform uiAreaRect;

    private Transform player;
    public UIVirtualJoystick joyStick;

    private Vector2 moveDirection;
    private float targetSpeed = 0.0f;

    // Add your input and camera control variables here
    private Vector2 _input;
    private float _cinemachineTargetPitch;
    private float _rotationVelocity;
    public float RotationSpeed = 2.0f;
    public float BottomClamp = -80.0f; // Adjust as needed
    public float TopClamp = 80.0f; // Adjust as needed
    public Transform CinemachineCameraTarget;
    //private bool IsCurrentDeviceMouse = true; // Adjust as needed

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
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - _controller.bounds.extents.y - 0.1f, transform.position.z);

        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
    }

    private void Move(Vector2 moveDirection)
    {
        float actualSpeed = Mathf.Lerp(_controller.velocity.magnitude, targetSpeed, SpeedChangeRate * Time.deltaTime);

        Vector3 moveDirection3D = (transform.forward * moveDirection.y + transform.right * moveDirection.x).normalized;

        _controller.Move(moveDirection3D * actualSpeed * Time.deltaTime + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }

    private void OnJoystickOutput(Vector2 output)
    {
        moveDirection = output;

        float joystickMagnitude = moveDirection.magnitude;

        targetSpeed = joystickMagnitude >= 1f ? SprintSpeed : MoveSpeed;
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

                // Update Cinemachine camera target pitch
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

