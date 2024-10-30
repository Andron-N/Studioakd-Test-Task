using UnityEngine;
using Scripts.Input;

namespace Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _crouchSpeed = 2.5f;
        [SerializeField] private float _jumpHeight = 1.5f;
        [SerializeField] private float _gravity = -9.81f;
        [SerializeField] private float _crouchHeight = 0.5f;
        [SerializeField] private float _standingHeight = 2f;

        private Vector3 _velocity;
        private bool _isCrouching;

        private void Start()
        {
            _playerInput.OnJump += Jump;
            _playerInput.OnCrouch += UpdateCrouchState;
        }

        private void Update()
        {
            Move();
            ApplyGravity();
            Rotate();
        }
        
        private void OnDestroy()
        {
            _playerInput.OnJump -= Jump;
            _playerInput.OnCrouch -= UpdateCrouchState;
        }

        private void Move()
        {
            Vector2 movement = _playerInput.MovementInput;
            float currentSpeed = _isCrouching ? _crouchSpeed : _moveSpeed;
            Vector3 move = new Vector3(movement.x, 0, movement.y);
            move = transform.TransformDirection(move);
            move *= currentSpeed;

            _characterController.Move(move * Time.deltaTime);
        }

        private void ApplyGravity()
        {
            if (_characterController.isGrounded)
                _velocity.y = 0;

            _velocity.y += _gravity * Time.deltaTime;
            _characterController.Move(_velocity * Time.deltaTime);
        }

        private void Jump()
        {
            if (_characterController.isGrounded)
                _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }

        private void UpdateCrouchState(bool isCrouching)
        {
            _isCrouching = isCrouching;
            _characterController.height = isCrouching ? _crouchHeight : _standingHeight;
        }

        private void Rotate()
        {
            Vector2 look = _playerInput.MouseInput;
            transform.Rotate(0, look.x, 0);
        }
    }
}
