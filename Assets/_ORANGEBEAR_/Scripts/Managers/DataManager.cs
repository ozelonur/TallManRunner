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

        #region Serialized Fields

        [SerializeField] private int initialDiamondsCount = 100;

        #endregion

        #region Private Variables

        private static int ActiveCharacterIndex
        {
            get => PlayerPrefs.GetInt("ActiveCharacterIndex", 0);
            set => PlayerPrefs.SetInt("ActiveCharacterIndex", value);
        }

        #endregion

        #region Datas

        public static int DiamondCount
        {
            get => PlayerPrefs.GetInt("DiamondCount", 0);
            private set => PlayerPrefs.SetInt("DiamondCount", value);
        }

        public int levelDiamondCount;

        private static int CurrentCharacterIndex
        {
            get => PlayerPrefs.GetInt("CurrentCharacter", 0);
            set => PlayerPrefs.SetInt("CurrentCharacter", value);
        }

        #endregion

        #region Public Variables

        public CharacterData[] characterDatas;

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

            SaveData();
            LoadData();

            AddDiamond(initialDiamondsCount);
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
            levelDiamondCount = 0;
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
            CurrentCharacterIndex = index;
            return characterDatas[index];
        }

        public void SaveData()
        {
            foreach (var characterData in characterDatas)
            {
                PlayerPrefs.SetInt(characterData.CharacterName, characterData.Unlocked ? 1 : 0);
            }
        }

        public CharacterData GetCurrentCharacter()
        {
            return characterDatas[CurrentCharacterIndex];
        }

        public void SetActiveCharacterIndex(int index)
        {
            ActiveCharacterIndex = index;
        }
        
        public CharacterData GetActiveModel()
        {
           return characterDatas[ActiveCharacterIndex];
        }

        #endregion

        #region Private Methods

        private void LoadData()
        {
            foreach (var characterData in characterDatas)
            {
                characterData.Unlocked = PlayerPrefs.GetInt(characterData.CharacterName, 0) == 1;
            }
        }

        #endregion
    }
}