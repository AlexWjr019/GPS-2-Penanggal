using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class CombinedController : MonoBehaviour
    {
        [Header("Player")]
        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 4.0f;
        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed = 6.0f;
        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate = 10.0f;

        [Space(10)]
        [Tooltip("The height the player can jump")]
        public float JumpHeight = 1.2f;
        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float Gravity = -15.0f;

        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float JumpTimeout = 0.1f;
        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float FallTimeout = 0.15f;

        [Header("Player Grounded")]
        [Tooltip("If the character is grounded or not. Not part of the CharacterController built-in grounded check")]
        public bool Grounded = true;
        [Tooltip("Useful for rough ground")]
        public float GroundedOffset = -0.14f;
        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius = 0.5f;
        [Tooltip("What layers the character uses as ground")]
        public LayerMask GroundLayers;

#if ENABLE_INPUT_SYSTEM
        private PlayerInput _playerInput;
#endif
        private CharacterController _controller;
        private StarterAssetsInputs _input;

        private const float _threshold = 0.01f;
        private float _verticalVelocity;

        private float _terminalVelocity = 53.0f;

        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;

        private Vector2 rotation = Vector2.zero;
        public float sensitivity = 2.0f; // 控制相机旋转灵敏度
        public RectTransform uiAreaRect; // 引用UI区域的RectTransform

        private Transform player; // Add this to reference the player's transform

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
#if ENABLE_INPUT_SYSTEM
            _playerInput = GetComponent<PlayerInput>();
#else
            Debug.LogError("Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif

            // reset our timeouts on start
            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;

            player = GetComponent<Transform>(); // Assign the player's transform
        }

        private void Update()
        {
            JumpAndGravity();
            GroundedCheck();
            Move();
            CameraRotation(); // Call the camera rotation method
        }
        private void FixedUpdate()
        {
            CameraRotation(); // Call the camera rotation method
        }


        private void GroundedCheck()
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
        }

        private void Move()
        {
            // Get the magnitude of the move input (normalized analog stick position)
            float moveInputMagnitude = _input.move.magnitude;

            // Set the target speed based on move speed, sprint speed, and if sprint is pressed
            float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;

            // Calculate the actual speed as a combination of target speed and move input magnitude
            float actualSpeed = targetSpeed * moveInputMagnitude;

            // Get the forward direction of the camera
            Vector3 cameraForward = Camera.main.transform.forward;

            // Ignore the vertical (y) component of the camera's forward vector
            cameraForward.y = 0.0f;
            cameraForward.Normalize();

            // Calculate the input direction in world space
            Vector3 inputDirection = cameraForward * _input.move.y + Camera.main.transform.right * _input.move.x;

            // Move the player
            _controller.Move(inputDirection.normalized * (actualSpeed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
        }

        private void JumpAndGravity()
        {
            if (Grounded)
            {
                // reset the fall timeout timer
                _fallTimeoutDelta = FallTimeout;

                // stop our velocity dropping infinitely when grounded
                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }

                // Jump
                if (_input.jump && _jumpTimeoutDelta <= 0.0f)
                {
                    // the square root of H * -2 * G = how much velocity needed to reach desired height
                    _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
                }

                // jump timeout
                if (_jumpTimeoutDelta >= 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                // reset the jump timeout timer
                _jumpTimeoutDelta = JumpTimeout;

                // fall timeout
                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }

                // if we are not grounded, do not jump
                _input.jump = false;
            }

            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
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
            }
        }


        // 检查触摸是否在UI区域内
        private bool IsTouchInUIArea(Vector2 touchPosition)
        {
            if (uiAreaRect == null)
            {
                return false; // 如果没有指定UI区域，则默认为false
            }

            // 检查触摸位置是否在UI区域内
            return RectTransformUtility.RectangleContainsScreenPoint(uiAreaRect, touchPosition);
        }
    }
}
