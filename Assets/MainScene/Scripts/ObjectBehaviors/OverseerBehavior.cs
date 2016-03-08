using System;
using Assets.MainScene.Prefabs.Block;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.MainScene.Scripts.ObjectBehaviors
{
    public class OverseerBehavior : MonoBehaviour
    {
        public BlocksManagerBehavior BlocksManager;

        public FlipperBehavior Flipper;

        public BallBehavior Ball;

        public Text Text;

        private int _score;
        private int _lives = 3;

        // Use this for initialization
        private void Start()
        {
            // Set up blocks
            var blocks = BlocksManager.CreateBlocks();
            foreach (var block in blocks)
            {
                block.GetComponent<BlockBehavior>().BlockDestroyed += BlockDestroyed;
            }

            // Set up ball
            Ball.BallDestroyed += BallOnBallDestroyed;
        }

        private void BallOnBallDestroyed(object sender, EventArgs eventArgs)
        {
            _lives--;
            Flipper.Reset();
            Ball.Reset();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateScoreLabel();
        }

        public void BlockDestroyed(object source, EventArgs e)
        {
            _score += 100;
            UpdateScoreLabel();
            ((BlockBehavior) source).BlockDestroyed -= BlockDestroyed;
        }

        private void UpdateScoreLabel()
        {
            Text.text = string.Format("Lives: {0} Score: {1}", _lives, _score);
        }
    }
}
