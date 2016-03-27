using UnityEngine;

namespace Assets.MainScene.Scripts.ObjectBehaviors
{
    /// <summary>
    /// Class responsible for obtaining user input and transmitting it to objects
    /// </summary>
    public class InputControllerBehavior : MonoBehaviour
    {
        /// <summary>
        /// The flipper this controller controls.
        /// </summary>
        public FlipperBehavior Flipper;

        /// <summary>
        /// The ball this controller launches.
        /// </summary>
        public BallBehavior Ball;

        /// <summary>
        /// The cameras available to the user.
        /// </summary>
        public Camera[] Cameras;

        int _cameraIndex;

        void Start()
        {
            if (Flipper == null) throw new UnityException("Flipper hasn't been set to the InputController");
            if (Ball == null) throw new UnityException("Ball hasn't been set on the InputController");

            foreach (var c in Cameras)
            {
                c.enabled = false;
            }

            Cameras[_cameraIndex].enabled = true;
        }

        // Update is called once per frame
        void Update()
        {
            var horizontal = Input.GetAxis(InputNames.HorizontalAxis);
            Flipper.Move(horizontal);

            if (Input.GetButtonDown(InputNames.BallKicker))
            {
                Ball.Launch();
            }

            if (Input.GetButtonDown(InputNames.SwitchCamera))
            {
                // Disable current camera
                Cameras[_cameraIndex].enabled = false;

                // Enable next camera
                _cameraIndex = (_cameraIndex + 1) % Cameras.Length;
                Cameras[_cameraIndex].enabled = true;
            }
        }
    }
}
