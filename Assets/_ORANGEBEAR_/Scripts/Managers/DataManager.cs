#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Datas;
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
        
        public int CurrentCharacterIndex
        {
            get => PlayerPrefs.GetInt("CurrentCharacter", 0);
            set => PlayerPrefs.SetInt("CurrentCharacter", value);
        }

        #endregion
        
        #region Public Variables

        public CharacterData[] CharacterDatas;

        #endregion

        #region Private Variables

        private int _currentIndex;

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

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(GameEvents.InitLevel, InitLevel);
            }

            else
            {
                UnRegister(GameEvents.InitLevel, InitLevel);
            }
        }

        private void InitLevel(object[] args)
        {
            Roar(CustomEvents.UpdateCurrency, DiamondCount);
        }

        #endregion

        #region Public Methods

        public void AddDiamond(int amount)
        {
            DiamondCount += amount;
            Roar(CustomEvents.UpdateCurrency, DiamondCount);
        }
        
        public void SubtractDiamond(int amount)
        {
            DiamondCount -= amount;
            Roar(CustomEvents.UpdateCurrency, DiamondCount);
        }
        
        public CharacterData GetCharacterData(int index)
        {
            _currentIndex = index;
            CurrentCharacterIndex = index;
            return CharacterDatas[index];
        }
        
        public void SaveData()
        {
            foreach (var characterData in CharacterDatas)
            {
                PlayerPrefs.SetInt(characterData.CharacterName, characterData.Unlocked ? 1 : 0);
            }
        }

        public CharacterData GetCurrentCharacter()
        {
            return CharacterDatas[CurrentCharacterIndex];
        }

        #endregion

        #region Private Methods

       

        private void LoadData()
        {
            foreach (var characterData in CharacterDatas)
            {
                characterData.Unlocked = PlayerPrefs.GetInt(characterData.CharacterName, 0) == 1;
            }
        }

        #endregion
    }
}