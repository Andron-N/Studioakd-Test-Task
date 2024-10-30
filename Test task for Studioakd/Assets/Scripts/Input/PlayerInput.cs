using UnityEngine;

namespace Scripts.Input
{
  public class PlayerInput : MonoBehaviour
  {
    public Vector2 MovementInput { get; private set; }
    public Vector2 MouseInput { get; private set; }

    public delegate void JumpAction();
    public event JumpAction OnJump;

    public delegate void PickDropItemAction();
    public event PickDropItemAction OnPickDropItem;

    public delegate void ThrowItemAction();
    public event ThrowItemAction OnThrowItem;

    public delegate void CrouchAction(bool isCrouching);
    public event CrouchAction OnCrouch;

    private void Update()
    {
      MovementInput = new Vector2(UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical"));
      MouseInput = new Vector2(UnityEngine.Input.GetAxis("Mouse X"), UnityEngine.Input.GetAxis("Mouse Y"));

      if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
        OnJump?.Invoke();

      if (UnityEngine.Input.GetKeyDown(KeyCode.LeftControl))
        OnCrouch?.Invoke(true);
      else if (UnityEngine.Input.GetKeyUp(KeyCode.LeftControl))
        OnCrouch?.Invoke(false);

      if (UnityEngine.Input.GetKeyDown(KeyCode.E))
        OnPickDropItem?.Invoke();

      if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
        OnThrowItem?.Invoke();
    }
  }
}
