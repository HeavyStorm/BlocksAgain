using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.MainScene.Scripts.ObjectBehaviors
{
    public class BlocksManagerBehavior : MonoBehaviour
    {
        public GameObject Board;

        public GameObject BlockPrefab;

        public float Margin;

        public float PercentageOfBoardFromTop = 0.50f;

        public IEnumerable<GameObject> CreateBlocks()
        {
            var blockSize = BlockPrefab.GetComponent<Renderer>().bounds.size;
            var boardSize = Board.GetComponent<Renderer>().bounds.size;

            return CreateBlocksImpl(blockSize, boardSize, Margin);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blockSize"></param>
        /// <param name="boardSize"></param>
        /// <param name="margin">Simetrical margin between blocks.</param>
        private IEnumerable<GameObject> CreateBlocksImpl(Vector3 blockSize, Vector3 boardSize, float margin)
        {
            var howManyAcross = (int)Math.Floor(boardSize.x / (blockSize.x + margin));
            var howManyLines = (int)Math.Floor((boardSize.z * PercentageOfBoardFromTop) / (blockSize.z + margin));
            print("HowManyAccross: " + howManyAcross + " HowManyLines: " + howManyLines);

            // We set a origin to simplify the calculus below, where this position is the top-left coordinate of the board
            // Also, Y is always 1, which is the shift *from* the board.
            var origin = new Vector3(-(boardSize.x / 2) + margin, 1, +(boardSize.z / 2) - margin);

            var boardCenter = Board.transform.position;

            for (var i = 0; i < howManyLines; i++)
            {
                for (var j = 0; j < howManyAcross; j++)
                {
                    // Get the adequate position by moving further from the top-left vertex.
                    // Remember that the distance is half the size, since the position is relative to the center of the board
                    var shiftFromOrigin = new Vector3(j * (margin + blockSize.x), 0, -(i * (margin + blockSize.y)));
                    var shiftToBoxCenter = blockSize / 2;

                    var position = boardCenter + origin + shiftFromOrigin + shiftToBoxCenter;

                    var gameobject = (GameObject)Instantiate(BlockPrefab, position, Quaternion.identity);

                    yield return gameobject;
                }
            }
        }

        // Use this for initialization
        void Start()
        {
            if (PercentageOfBoardFromTop > 1 || PercentageOfBoardFromTop < 0.00001f)
            {
                // ReSharper disable once NotResolvedInText
                throw new ArgumentOutOfRangeException("PercentageOfBoardFromTop", "Must be between 0 and 1");
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
