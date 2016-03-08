using UnityEngine;

namespace Assets.Other_Assets.Scripts
{
    /// <summary>
    /// Simple behavior that enables one object to follow another.
    /// </summary>
    public class FollowsBehavior : MonoBehaviour
    {
        /// <summary>
        /// Whether the object should be following.
        /// </summary>
        public bool Enabled = true;
            
        /// <summary>
        /// The object that should be followed.
        /// </summary>
        public GameObject Target;

        /// <summary>
        /// The amount of force that should be applied on every update towards the target.
        /// </summary>
        public float MaximumForceMagnitude = 1.0f;

        /// <summary>
        /// The minimum distance that should be maintained.
        /// </summary>
        public float MinimumDistance = 0.0f;

        /// <summary>
        /// Whether the following should happen along the X axis.
        /// </summary>
        public bool EnableMoveX = true;

        /// <summary>
        /// Whether the following should happen along the Y axis.
        /// </summary>
        public bool EnableMoveY = true;

        /// <summary>
        /// Whether the following should happen along the Z axis.
        /// </summary>
        public bool EnableMoveZ = true;

        /// <summary>
        /// Rigidbody component of this object.
        /// </summary>
        private Rigidbody _rigidbody;

        // Use this for initialization
        public void Start ()
        {
            if (Target == null) throw new UnityException("Target not set.");
            _rigidbody = GetComponent<Rigidbody>();
            if (_rigidbody == null)
            {
                throw new UnityException("This behavior can only be applied to GameObjects that have a Rigidbody component.");
            }
        }

        public void FixedUpdate()
        {
            if (!Enabled) return;
            
            var distance = Vector3.Distance(this.transform.position, Target.transform.position);
            print("distance between this and the target: " + distance);
            if (distance > MinimumDistance)
            {
                var direction = (Target.transform.position - this.transform.position).normalized;

                if (!EnableMoveX) direction.x = 0;
                if (!EnableMoveY) direction.y = 0;
                if (!EnableMoveZ) direction.z = 0;

                _rigidbody.AddRelativeForce(direction * MaximumForceMagnitude * distance);
            }
        }
	
        // Update is called once per frame
        private void Update () {
	
        }
    }
}
