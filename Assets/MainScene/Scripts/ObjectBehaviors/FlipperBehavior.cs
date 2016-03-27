using System;
using System.Linq;
using UnityEngine;

namespace Assets.MainScene.Scripts.ObjectBehaviors
{
    /// <summary>
    /// Defines the default behavior for a flipper GameObject.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class FlipperBehavior : MonoBehaviour
    {
        /// <summary>
        /// The velocity applied (amount of movement) in meters per second (m/s). 
        /// </summary>
        public float BaseVelocity = 10.0f;

        /// <summary>
        /// Movement amount must be higher than this.
        /// </summary>
        public float MovementThreshold = 0.1f;

        /// <summary>
        /// List of possible limitations for movement of the flipper.
        /// </summary>
        public Collider[] Limits; 

        /// <summary>
        /// Amount of movement that is being applied to the object.
        /// </summary>
        float _movementAmount;

        /// <summary>
        /// The initial position of the flipper
        /// </summary>
        Vector3 _initialPosition;

        /// <summary>
        /// Means there's a collision with a limit.
        /// </summary>
        bool _isCollided;

        /// <summary>
        /// Move the flipper in the horizontal (x) axis.
        /// </summary>
        /// <param name="amount">The amount of push that is desired, being a value between [-1,+1]. For non analog controllers, always -1, 0 or 1.</param>
        public void Move(float amount)
        {
            _movementAmount = amount;
        }

        public void Reset()
        {
            transform.position = _initialPosition;
        }

        /// <summary>
        /// Use this for initialization 
        /// </summary>
        void Start()
        {
            _initialPosition = transform.position;
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        void Update()
        {
            if (Math.Abs(_movementAmount) < MovementThreshold) return;

            if (_isCollided) return;

            transform.position = Vector3.Lerp(transform.position, transform.position + (_movementAmount*Vector3.right * BaseVelocity) , Time.deltaTime);
        }

        /// <summary>
        /// Called when a collision is started.
        /// </summary>
        /// <param name="collision">Data about the collision</param>
        void OnCollisionEnter(Collision collision)
        {
            if (Limits.Contains(collision.collider))
            {
                _isCollided = true;
            }
        }

        /// <summary>
        /// Called when a collision is started.
        /// </summary>
        /// <param name="collision">Data about the collision</param>
        void OnCollisionExit(Collision collision)
        {
            if (Limits.Contains(collision.collider))
            {
                _isCollided = false;
            }
        }
    }
}


