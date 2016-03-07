using Assets.Other_Assets.Scripts;
using UnityEngine;

namespace Assets.Main_Scene.Scripts
{
    /// <summary>
    /// Main behavior that controls a ball in game
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class BallBehavior : MonoBehaviour, IUpdatable
    {
        /// <summary>
        /// The flipper related to the ball
        /// </summary>
        public GameObject Flipper;

        /// <summary>
        /// This is the object that, once touched, kills the ball.
        /// </summary>
        public GameObject DeathBarrier;

        /// <summary>
        /// Sound to be played on collision.
        /// </summary>
        public AudioClip CrashSound;

        /// <summary>
        /// Sound to be played on death.
        /// </summary>
        public AudioClip DeathSound;

        /// <summary>
        /// Sound to be played on launch.
        /// </summary>
        public AudioClip LaunchSound;

        /// <summary>
        /// The speed that is applied to the ball when it begins moving and whenever it lose any stats.
        /// </summary>
        public float LaunchForceMagnitude = 2000.0f;

        /// <summary>
        /// The angle at which the ball will be hurled.
        /// </summary>
        public float LaunchAngleAmplitude = 60.0f;

        /// <summary>
        /// The rigidbody component of this object.
        /// </summary>
        private Rigidbody _rigidbody;

        /// <summary>
        /// The current state of the ball.
        /// </summary>
        private State _currentState = State.Still;

        /// <summary>
        /// AudioSource to be played on the death of the ball.
        /// </summary>
        private AudioSource _deathSoundAudioSource;

        /// <summary>
        /// Sound to be played on collision.
        /// </summary>
        private AudioSource _crashSoundAudioSource;

        /// <summary>
        /// Sound to be played on launch.
        /// </summary>
        private AudioSource _launchSoundAudioSource;

        // Use this for initialization
        public void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();

            if (Flipper == null) throw new UnityException("Flipper must be set.");

            _deathSoundAudioSource = AddAudioSource(DeathSound);
            _launchSoundAudioSource = AddAudioSource(LaunchSound);
            _crashSoundAudioSource = AddAudioSource(CrashSound);

        }

        // Update is called once per frame
        public void Update()
        {
        }

        public void FixedUpdate()
        {
            if (_currentState != State.WaitingToMove) return;

            // Activate physics managed movement
            transform.parent = null;
            _rigidbody.isKinematic = false;

            var angle = Random.Range(-LaunchAngleAmplitude / 2, +LaunchAngleAmplitude / 2);
            var force = Quaternion.Euler(0, angle, 0) * (Vector3.forward * LaunchForceMagnitude);

            _rigidbody.AddRelativeForce(force);
            _currentState = State.Moving;
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (_currentState != State.Moving) return;

            if (collision.gameObject == Flipper)
            {
                _crashSoundAudioSource.Play();
                _rigidbody.AddForce(collision.impulse.normalized * LaunchForceMagnitude, ForceMode.Force);
            }
            else if (collision.gameObject == DeathBarrier)
            {
                _deathSoundAudioSource.Play();
                _currentState = State.Dying;
                _rigidbody.velocity = Vector3.zero;

            }
            else
            {
                _crashSoundAudioSource.Play();
            }
        }

        /// <summary>
        /// Should be called once, in order to make the ball start movement.
        /// </summary>
        public void Launch()
        {
            if (_currentState != State.Still) return;
            _currentState = State.WaitingToMove;

            _launchSoundAudioSource.Play();
        }

        private AudioSource AddAudioSource(AudioClip clip)
        {
            var source = gameObject.AddComponent<AudioSource>();
            source.clip = clip;
            return source;
        }

        /// <summary>
        /// All possible states of this object.
        /// </summary>
        private enum State
        {
            Still,
            WaitingToMove,
            Moving,
            Dying,
            Dead
        }
    }
}
