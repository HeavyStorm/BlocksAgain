namespace Assets.Other_Assets.Scripts
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// I don't like that Start and Update aren't inherited, so, I'm creating an interface. Sue me.
    /// </remarks>
    public interface IUpdatable
    {
        /// <summary>
        /// Use this for inialization
        /// </summary>
        void Start();

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        void Update();
    }

    /// <summary>
    /// Object that can be updated on a fixed amount of time.
    /// </summary>
    /// <remarks>
    /// Should be used to update physics stuff.
    /// </remarks>
    public interface IFixedUpdatable
    {
        /// <summary>
        /// Is called on a fixed amount of time (instead of being called on each frame).
        /// </summary>
        void FixedUpdate();
    }
}