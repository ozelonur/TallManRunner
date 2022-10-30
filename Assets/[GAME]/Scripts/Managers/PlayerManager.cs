#region Header
// Developed by Onur ÖZEL
#endregion

using _GAME_.Scripts.Bears.Player;
using _ORANGEBEAR_.EventSystem;

namespace _GAME_.Scripts.Managers
{
    public class PlayerManager : Bear
    {
        #region Singleton

        public static PlayerManager Instance;

        #endregion

        #region Public Variables

        public PlayerBear currentPlayer;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion

        #region Public Methods

        public void SetPlayer(PlayerBear player)
        {
            currentPlayer = player;
        }

        #endregion
    }
}