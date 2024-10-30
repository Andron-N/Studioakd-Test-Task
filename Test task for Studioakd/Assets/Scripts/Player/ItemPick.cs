using Scripts.Input;
using UnityEngine;

namespace Scripts.Player
{
  public class ItemPickup : MonoBehaviour
  {
    [SerializeField] private Transform _holdPosition;
    [SerializeField] private float _pickupRange = 2.0f;
    [SerializeField] private float _throwForce = 10f;
    [SerializeField] private PlayerInput _playerInput;

    private GameObject _currentItem;
    private const string Pickable = "Pickable";

    private void Start()
    {
      _playerInput.OnPickDropItem += PickDropItem;
      _playerInput.OnThrowItem += ThrowItem;
    }
    
    private void OnDestroy()
    {
      _playerInput.OnPickDropItem -= PickDropItem;
      _playerInput.OnThrowItem -= ThrowItem;
    }

    private void PickDropItem()
    {
      if (_currentItem == null)
        TryPickupItem();
      else
        DropItem();
    }

    private void TryPickupItem()
    {
      Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

      if (Physics.Raycast(ray, out RaycastHit hit, _pickupRange))
        if (hit.collider.CompareTag(tag: Pickable))
          PickupItem(hit.collider.gameObject);
    }

    private void DropItem()
    {
      Rigidbody itemRigidbody = _currentItem.GetComponent<Rigidbody>();

      itemRigidbody.isKinematic = false;
      _currentItem.transform.SetParent(null);

      _currentItem = null;
    }

    private void ThrowItem()
    {
      if (_currentItem != null)
      {
        Rigidbody itemRigidbody = _currentItem.GetComponent<Rigidbody>();
        itemRigidbody.isKinematic = false;
        _currentItem.transform.SetParent(null);

        Vector3 throwDirection = Camera.main.transform.forward;
        itemRigidbody.AddForce(throwDirection * _throwForce, ForceMode.VelocityChange);

        _currentItem = null;
      }
    }

    private void PickupItem(GameObject item)
    {
      _currentItem = item;
      Rigidbody itemRigidbody = item.GetComponent<Rigidbody>();

      itemRigidbody.isKinematic = true;
      item.transform.SetParent(_holdPosition);
      item.transform.localPosition = Vector3.zero;
    }
  }
}
