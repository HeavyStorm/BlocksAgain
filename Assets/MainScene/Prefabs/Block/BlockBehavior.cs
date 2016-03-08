using System;
using Assets.MainScene.Scripts;
using Assets.ScriptMagic;
using UnityEngine;

namespace Assets.MainScene.Prefabs.Block
{
    /// <summary>
    /// Main behavior for a Block object.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Collider))]
    public class BlockBehavior : MonoBehaviour
    {
        /// <summary>
        /// Audio to play when the block is destroyed.
        /// </summary>
        public AudioClip DestroyedAudioClip;

        public event EventHandler BlockDestroyed;

        private AudioSource _destroyedAudioSource;
        private Animator _animator;
        private Collider _collider;

        // Use this for initialization
        private void Start()
        {
            _destroyedAudioSource = this.AddAudioSource(DestroyedAudioClip);
            _animator = GetComponent<Animator>();
            _collider = GetComponent<Collider>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            _animator.SetTrigger("Destroy");
            OnBlockDestroyed();
            _collider.enabled = false;
            Physics.IgnoreCollision(collision.collider, _collider);
            _destroyedAudioSource.Play();
            Destroy(gameObject, 1.0f);
        }


        protected virtual void OnBlockDestroyed()
        {
            var handler = BlockDestroyed;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
