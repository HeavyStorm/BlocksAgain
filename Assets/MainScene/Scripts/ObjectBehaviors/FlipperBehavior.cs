using System;
using Assets.Other_Assets.Scripts;
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
        /// BaseForce applied on move
        /// </summary>
        public float BaseForce = 50.0f;

        /// <summary>
        /// Movement amount must be higher than this.
        /// </summary>
        public float MovementThreshold = 0.1f;

        /// <summary>
        /// The <see cref="Rigidbody"/> component of this <see cref="GameObject"/>.
        /// </summary>
        private Rigidbody _rigidbody;

        /// <summary>
        /// Amount of movement that is being applied to the object.
        /// </summary>
        private float _movementAmount;

        /// <summary>
        /// The initial position of the flipper
        /// </summary>
        private Vector3 _initialPosition;

      

        /// <summary>
        /// Push the flipper in the horizontal (x) axis.
        /// </summary>
        /// <param name="amount">The amount of push that is desired, being a value between [-1,+1].</param>
        public void Push(float amount)
        {
            _movementAmount = amount;
        }

        public void Reset()
        {
            // TODO: Do I really need to set kinematic?
            _rigidbody.isKinematic = true;
            _rigidbody.velocity = Vector3.zero;
            transform.position = _initialPosition;
            _rigidbody.isKinematic = false;
        }

        /// <summary>
        /// Use this for initialization 
        /// </summary>
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _initialPosition = transform.position;
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        void Update()
        {
        }

        void FixedUpdate()
        {
            if (!(Math.Abs(_movementAmount) > MovementThreshold)) return;

            var force = Vector3.right * _movementAmount * BaseForce;
            _rigidbody.AddRelativeForce(force);
        }
    }
}


