#region Header

// Developed by Onur ÖZEL

#endregion


using _GAME_.Scripts.Datas;
using _GAME_.Scripts.GlobalVariables;
using _ORANGEBEAR_.Scripts.Bears;
using _ORANGEBEAR_.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _GAME_.Scripts.Bears
{
    public class GameUIBear : UIBear
    {
        #region Serialized Fields

        [SerializeField] private TMP_Text currencyText;

        [Header("Shop Panel")] [SerializeField]
        private GameObject garagePanel;

        [SerializeField] private Button shopButton;
        [SerializeField] private Button closeShopButton;
        [SerializeField] private Button previousButton;
        [SerializeField] private Button nextCharacterButton;
        [SerializeField] private TMP_Text heroNameText;
        [SerializeField] private TMP_Text heroPriceText;
        [SerializeField] private Image heroImage;
        [SerializeField] private Button buyButton;
        [SerializeField] private GameObject lockImage;
        [SerializeField] private TMP_Text buyButtonText;

        #endregion

        #region Private Variables

        private int _index;

        #endregion

        #region MonoBehaviour Methods

        protected override void Awake()
        {
            base.Awake();

            #region Garage

            shopButton.onClick.AddListener(OnGarageButtonClicked);
            closeShopButton.onClick.AddListener(OnCloseShopButtonClicked);
            nextCharacterButton.onClick.AddListener(OnNextButtonClicked);
            previousButton.onClick.AddListener(OnPreviousButtonClicked);
            buyButton.onClick.AddListener(OnClickBuy);
            garagePanel.SetActive(false);

            #endregion
        }

        private void OnClickBuy()
        {
            if (DataManager.Instance.CharacterDatas[_index].Unlocked)
            {
                Roar(CustomEvents.SwitchCharacter);
                OnCloseShopButtonClicked();
                return;
            }

            if (DataManager.Instance.DiamondCount < DataManager.Instance.CharacterDatas[_index].Price)
            {
                return;
            }

            DataManager.Instance.SubtractDiamond(DataManager.Instance.CharacterDatas[_index].Price);

            Unlock();
        }

        private void OnPreviousButtonClicked()
        {
            if (_index <= 0)
            {
                return;
            }

            _index--;

            previousButton.gameObject.SetActive(_index > 0);
            nextCharacterButton.gameObject.SetActive(_index < 9);

            GetCharacterData();
        }

        private void OnNextButtonClicked()
        {
            if (_index > 9)
            {
                return;
            }

            _index++;

            previousButton.gameObject.SetActive(_index > 0);
            nextCharacterButton.gameObject.SetActive(_index < 9);

            GetCharacterData();
        }

        private void OnCloseShopButtonClicked()
        {
            garagePanel.SetActive(false);
            _index = 0;
        }

        private void OnGarageButtonClicked()
        {
            garagePanel.SetActive(true);

            switch (_index)
            {
                case 0:
                    previousButton.gameObject.SetActive(false);
                    nextCharacterButton.gameObject.SetActive(true);
                    break;
                case > 9:
                    previousButton.gameObject.SetActive(true);
                    nextCharacterButton.gameObject.SetActive(false);
                    break;
            }

            GetCharacterData();
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            base.CheckRoarings(status);

            if (status)
            {
                Register(CustomEvents.UpdateCurrency, UpdateCurrency);
            }

            else
            {
                UnRegister(CustomEvents.UpdateCurrency, UpdateCurrency);
            }
        }

        private void UpdateCurrency(object[] args)
        {
            currencyText.text = args[0].ToString();
        }

        #endregion

        #region Private Methods

        private void GetCharacterData()
        {
            CharacterData characterData = DataManager.Instance.GetCharacterData(_index);

            heroNameText.text = characterData.CharacterName;
            heroPriceText.text = characterData.Price.ToString();
            heroImage.sprite = characterData.heroSprite;
            heroPriceText.gameObject.SetActive(!characterData.Unlocked);
            lockImage.SetActive(!characterData.Unlocked);

            buyButtonText.text = characterData.Unlocked ? "PLAY" : "USE";
        }

        private void Unlock()
        {
            DataManager.Instance.CharacterDatas[_index].Unlocked = true;
            GetCharacterData();
            
            DataManager.Instance.SaveData();
        }

        #endregion
    }
}