#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.GlobalVariables;
using _ORANGEBEAR_.EventSystem;
using UnityEngine;

namespace _ORANGEBEAR_.Scripts.Managers
{
    public class DataManager : Bear
    {
        #region Singleton

        public static DataManager Instance;

        #endregion

        #region Datas

        public int DiamondCount
        {
            get => PlayerPrefs.GetInt("DiamondCount", 0);
            set => PlayerPrefs.SetInt("DiamondCount", value);
        }

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

        public void AddDiamond(int amount)
        {
            DiamondCount += amount;
            print("Diamond Count: " + DiamondCount);
            Roar(CustomEvents.UpdateCurrency, DiamondCount);
        }
        
        public void SubtractDiamond(int amount)
        {
            DiamondCount -= amount;
            print("Diamond Count: " + DiamondCount);
            Roar(CustomEvents.UpdateCurrency, DiamondCount);
        }

        #endregion
    }
}