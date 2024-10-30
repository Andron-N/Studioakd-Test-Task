using UnityEngine;

namespace Scripts.Garage
{
  public class GarageDoorController : MonoBehaviour
  {
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _interactionZone;
    [SerializeField] private float _interactionDistance = 3f;

    private const string Open = "Open";
    private static readonly int OpenHash = Animator.StringToHash(Open);

    private bool _isOpen = false;

    private void Update()
    {
      if (!CanOpen() || !UnityEngine.Input.GetKeyDown(KeyCode.F))
        return;

      if (!_isOpen)
      {
        _animator.SetBool(OpenHash, true);
        _isOpen = true;
      }
      else
      {
        _animator.SetBool(OpenHash, false);
        _isOpen = false;
      }
    }

    private bool CanOpen() => 
      Vector3.Distance(_player.position, _interactionZone.position) <= _interactionDistance;
  }
}
