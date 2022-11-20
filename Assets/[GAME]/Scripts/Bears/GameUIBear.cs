#region Header

// Developed by Onur ÖZEL

#endregion


using System.Collections;
using _GAME_.Scripts.Datas;
using _GAME_.Scripts.Extensions;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Managers;
using _ORANGEBEAR_.EventSystem;
using _ORANGEBEAR_.Scripts.Bears;
using _ORANGEBEAR_.Scripts.Managers;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _GAME_.Scripts.Bears
{
    public class GameUIBear : UIBear
    {
        #region Serialized Fields

        [SerializeField] private TMP_Text currencyText;
        [SerializeField] private TMP_Text earnedDiamondText;

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

        [Header("Main Menu")] [SerializeField] private Image currentCharacterImage;
        
        [Header("Settings Menu")]
        
        [SerializeField] private Button settingsButton;
        
        [Header("Info Text")]
        [SerializeField] private TMP_Text infoText;

        [SerializeField] private Color trueColor;
        [SerializeField] private Color falseColor;
        
        #endregion

        #region Ads

        [Header("Ads")] [SerializeField]
        private Button watchAdsButton;

        [SerializeField] private Button claimButton;

        #endregion

        #region Private Variables

        private int _index;
        private int _earnedDiamonds;

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

            #region Settings

            settingsButton.onClick.AddListener(OnSettingsButtonClicked);

            #endregion

            #region Ads

            watchAdsButton.onClick.AddListener(OnWatchAdsButtonClicked);
            claimButton.onClick.AddListener(OnClaimButtonClicked);

            #endregion
            
            infoText.transform.localScale = Vector3.zero;
        }

        private void OnClaimButtonClicked()
        {
            Advertisements.Instance.ShowRewardedVideo(DiamondClaimed);
        }

        private void DiamondClaimed(bool arg0)
        {
            DataManager.Instance.AddDiamond((_earnedDiamonds * 5) - _earnedDiamonds);
        }

        private void OnWatchAdsButtonClicked()
        {
            Advertisements.Instance.ShowRewardedVideo(DiamondRewarded);
        }

        private void DiamondRewarded(bool arg0)
        {
            watchAdsButton.gameObject.SetActive(false);
            DataManager.Instance.AddDiamond(50);
        }

        private void OnSettingsButtonClicked()
        {
            AnimationManager.Instance.PlaySettingsButtonAnimation();
        }

        private void Start()
        {
            StartCoroutine(WaitOneFrame());

            IEnumerator WaitOneFrame()
            {
                yield return null;
                currentCharacterImage.sprite = DataManager.Instance.GetActiveModel().heroSprite;
            }
        }

        private void OnClickBuy()
        {
            if (DataManager.Instance.characterDatas[_index].Unlocked)
            {
                DataManager.Instance.SetActiveCharacterIndex(_index);
                Roar(CustomEvents.SwitchCharacter);
                return;
            }

            if (DataManager.DiamondCount < DataManager.Instance.characterDatas[_index].Price)
            {
                return;
            }

            DataManager.Instance.SubtractDiamond(DataManager.Instance.characterDatas[_index].Price);

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
            nextCharacterButton.gameObject.SetActive(_index < 4);

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
                case > 4:
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
                Register(CustomEvents.SwitchCharacter, SwitchCharacter);
                Register(GameEvents.OnGameComplete, OnGameComplete);
                Register(CustomEvents.GiveInfo, GiveInfo);
                Register(GameEvents.OnGameStart, OnGameStart);
            }

            else
            {
                UnRegister(CustomEvents.UpdateCurrency, UpdateCurrency);
                UnRegister(CustomEvents.SwitchCharacter, SwitchCharacter);
                UnRegister(GameEvents.OnGameComplete, OnGameComplete);
                UnRegister(CustomEvents.GiveInfo, GiveInfo);
                UnRegister(GameEvents.OnGameStart, OnGameStart);
            }
        }

        private void OnGameStart(object[] args)
        {
            watchAdsButton.gameObject.SetActive(true);
        }

        private void GiveInfo(object[] args)
        {
            bool status = (bool) args[1];
            infoText.text = (string) args[0];
            infoText.color = status ? trueColor : falseColor;
            
            infoText.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                infoText.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack).SetDelay(.2f);
            });
        }

        private void OnGameComplete(object[] args)
        {
            bool status = (bool) args[0];
            Advertisements.Instance.ShowInterstitial();
            if (!status)
            {
                return;
            }
            _earnedDiamonds = DataManager.Instance.levelDiamondCount;
            
            int currentCount = 0;

            DOTween.To(() => currentCount, count1 => currentCount = count1, _earnedDiamonds, 1f).OnUpdate(() =>
            {
                earnedDiamondText.text = "+" + currentCount;
                if (Time.frameCount % 10 == 0)
                {
                    AudioManager.Instance.PlayCoinCollectSound();
                }
            });
        }

        private void SwitchCharacter(object[] args)
        {
            currentCharacterImage.sprite = DataManager.Instance.GetCurrentCharacter().heroSprite;
        }

        private void UpdateCurrency(object[] args)
        {
            currencyText.text = "<sprite=0>" + ((int)args[0]).WithSuffix();
        }

        #endregion

        #region Private Methods

        private void GetCharacterData()
        {
            CharacterData characterData = DataManager.Instance.GetCharacterData(_index);

            heroNameText.text = characterData.CharacterName;
            heroImage.sprite = characterData.heroSprite;
            
            if (characterData.Unlocked)
            {
                heroPriceText.text = "UNLOCKED";
            }
            else
            {
                heroPriceText.text = "<sprite=0>" + characterData.Price.WithSuffix();
            }

            lockImage.SetActive(!characterData.Unlocked);

            buyButtonText.text = characterData.Unlocked ? "USE" : "BUY";
        }

        private void Unlock()
        {
            DataManager.Instance.characterDatas[_index].Unlocked = true;
            GetCharacterData();

            DataManager.Instance.SaveData();
        }

        #endregion
    }
}