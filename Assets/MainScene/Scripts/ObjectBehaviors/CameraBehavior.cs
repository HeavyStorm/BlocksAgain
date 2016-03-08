using System;
using UnityEngine;

namespace Assets.MainScene.Scripts.ObjectBehaviors
{
    public class CameraBehavior : MonoBehaviour
    {
        public GameObject Flipper;

        public float MaxAngle = float.MaxValue;

        // Use this for initialization
        public void Start()
        {
            if (Flipper == null) throw new UnityException("No flipper assigned to this camera.");
        }

        // Update is called once per frame
        public void Update()
        {
            var euler = transform.rotation.eulerAngles;
            var yRotation = Flipper.transform.position.x * Flipper.transform.lossyScale.x / 2;

            if (Math.Abs(yRotation) > MaxAngle)
            {
                yRotation = MaxAngle * Math.Sign(yRotation);
            }

            transform.rotation = Quaternion.Euler(euler.x, yRotation, euler.z);
        }
    }
}
