using System;
using Assets.Other_Assets.Scripts;
using UnityEngine;

namespace Assets.Main_Scene.Scripts
{
    /// <summary>
    /// Defines the default behavior for a flipper GameObject.
    /// </summary>
    public class FlipperBehavior : MonoBehaviour, IUpdatable, IFixedUpdatable
    {
        ///// <summary>
        ///// The "attrition" between the flipper and the ground
        ///// </summary>
        //public Vector3 DecreaseAccelaration = new Vector3(-10, 0);

        ///// <summary>
        ///// The acceleration that the player inputs to the flipper every frame.
        ///// </summary>
        //public Vector3 IncreaseAccelaration = new Vector3(10, 0);


        public float BaseForce = 50.0f;

        ///// <summary>
        ///// Maximum speed that can be attained by this object.
        ///// </summary>
        //public Vector3 MaxSpeed = new Vector3(50, 0);

        ///// <summary>
        ///// Speeds less than this are considered zero
        ///// </summary>
        //public Vector3 DeadZone = new Vector3(1, 0);
        /// <summary>
        /// Movement amount must be higher than this.
        /// </summary>
        public float MovementThreshold = 0.1f;

        ///// <summary>
        ///// The current speed of the flipper.
        ///// </summary>
        //private Vector3 _currentSpeed = Vector3.zero;

        /// <summary>
        /// The <see cref="Rigidbody"/> component of this <see cref="GameObject"/>.
        /// </summary>
        private Rigidbody _rigidbody;

        /// <summary>
        /// Amount of movement that is being applied to the object.
        /// </summary>
        private float _movementAmount;

        /// <summary>
        /// Use this for initialization 
        /// </summary>
        public void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            if (_rigidbody == null) throw new UnityException("GameObject must have a Rigidbody component.");
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        public void Update()
        {
        }

        public void FixedUpdate()
        {
            if (!(Math.Abs(_movementAmount) > MovementThreshold)) return;

            var force = Vector3.right * _movementAmount * BaseForce;
            _rigidbody.AddRelativeForce(force);
        }

        /// <summary>
        /// Push the flipper in the horizontal (x) axis.
        /// </summary>
        /// <param name="amount">The amount of push that is desired, being a value between [-1,+1].</param>
        public void Push(float amount)
        {
            _movementAmount = amount;

        }
    }
}


