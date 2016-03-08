using Assets.Other_Assets.Scripts;
using UnityEngine;

namespace Assets.MainScene.Scripts.ObjectBehaviors
{
    /// <summary>
    /// Class responsible for obtaining user input and transmitting it to objects
    /// </summary>
    public class InputControllerBehavior : MonoBehaviour, IUpdatable
    {
        /// <summary>
        /// The flipper this controller controls.
        /// </summary>
        public FlipperBehavior Flipper;

        /// <summary>
        /// The ball this controller launches.
        /// </summary>
        public BallBehavior Ball;

        public void Start()
        {
            if (Flipper == null) throw new UnityException("Flipper hasn't been set to the InputController");
            if (Ball == null) throw new UnityException("Ball hasn't been set on the InputController");
        }

        // Update is called once per frame
        public void Update()
        {
            var horizontal = Input.GetAxis(InputNames.HorizontalAxis);
            //Flipper.Accelerate(horizontal);
            Flipper.Push(horizontal);

            if (Input.GetButton(InputNames.BallKicker))
            {
                Ball.Launch();
            }
        }
    }
}
