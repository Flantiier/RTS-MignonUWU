using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Variables
    #endregion

    #region Properties
    public static PlayerManager player;
    #endregion

    #region Builts_In
    private void Awake()
    {
        SingletonPattern();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Hanlde the singleton pattern
    /// </summary>
    private void SingletonPattern()
    {
        if(player != null)
        {
            //Same instance
            if(player != this)
                Destroy(gameObject);

            return;
        }

        player = this;
    }
    #endregion
}
