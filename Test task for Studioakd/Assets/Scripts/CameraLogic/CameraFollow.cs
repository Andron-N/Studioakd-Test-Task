using Scripts.Input;
using UnityEngine;

namespace Scripts.CameraLogic
{
  public class CameraFollow : MonoBehaviour
  {
    [SerializeField] private Transform _player;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private PlayerInput _playerInput;

    [SerializeField] private float _xRotationMin = -90f;
    [SerializeField] private float _xRotationMax = 90f;

    private float _rotationX = 0f;

    private void Start() => Cursor.lockState = CursorLockMode.Locked;

    private void Update()
    {
      float mouseX = _playerInput.MouseInput.x * _mouseSensitivity * Time.deltaTime;
      float mouseY = _playerInput.MouseInput.y * _mouseSensitivity * Time.deltaTime;

      _rotationX -= mouseY;
      _rotationX = Mathf.Clamp(_rotationX, _xRotationMin, _xRotationMax);

      transform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
      _player.Rotate(Vector3.up * mouseX);
    }
  }
}
