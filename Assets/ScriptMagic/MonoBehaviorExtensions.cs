using System.Collections;
using UnityEngine;

namespace Assets.ScriptMagic
{
    /// <summary>
    /// Extensions to the <see cref="MonoBehaviour"/> type.
    /// </summary>
    public static class MonoBehaviorExtensions
    {
        /// <summary>
        /// Adds an AudioSource to the <see cref="MonoBehaviour"/> script.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="clip"></param>
        /// <returns></returns>
        public static AudioSource AddAudioSource(this MonoBehaviour @this, AudioClip clip)
        {
            var source = @this.gameObject.AddComponent<AudioSource>();
            source.clip = clip;
            return source;
        }
    }
}