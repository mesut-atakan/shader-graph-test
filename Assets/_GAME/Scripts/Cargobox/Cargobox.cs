using UnityEngine;

namespace Aventra.Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public class Cargobox : MonoBehaviour, IHoldable
    {
        [SerializeField] private CargoboxSettings cargoboxSettings;
        [SerializeField] private float mass = 1.0f;

        private Rigidbody _rb;
        private BoxCollider _collider;

        public Transform UseObject => transform;
        public Transform HoldObject { get; set; }

        public float Mass => mass;

        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.mass = mass;
            _collider = GetComponent<BoxCollider>();
        }

        public void OnHold()
        {
            SetPhysicsActive(false);
            Debug.Log("Holding the box");
        }

        public void OnRelease()
        {
            SetPhysicsActive(true);
            Debug.Log("Released the box");
            Vector3 forceDirection;
            if (HoldObject != null)
                forceDirection = (transform.position - HoldObject.position).normalized;
            else
                forceDirection = Vector3.zero;
            
            _rb.AddForce(forceDirection * cargoboxSettings.DefaultForceValue, ForceMode.Impulse);

            HoldObject = null;
        }

        private void SetPhysicsActive(bool isActive)
        {
            _rb.isKinematic = !isActive;
            _rb.useGravity = isActive;
            _collider.enabled = isActive;
        }
    }
}