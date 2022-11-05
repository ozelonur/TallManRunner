#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Datas;
using _GAME_.Scripts.Enums;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Interfaces;
using _GAME_.Scripts.Managers;
using _ORANGEBEAR_.EventSystem;
using DG.Tweening;
using UnityEngine;

namespace _GAME_.Scripts.Bears.Player
{
    public class PlayerBear : Bear
    {
        #region Public Variables

        public CharacterData CharacterData;
        public Transform playerModel;
        public PlayerInitializer playerInitializer;

        #endregion

        #region MonoBehaviour Methods

        private void Start()
        {
            PlayerManager.Instance.currentPlayer = this;
        }

        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<ICollectable>()?.Collect();
            other.GetComponent<IGate>()?.HitToGate(this);
            other.GetComponent<IFinishPart>()?.HitToFinishPart(this);
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(GameEvents.OnGameStart, OnGameStart);
            }

            else
            {
                UnRegister(GameEvents.OnGameStart, OnGameStart);
            }
        }

        private void OnGameStart(object[] obj)
        {
            Roar(CustomEvents.CanFollowPath, true);
            Roar(CustomEvents.CanMoveHorizontal, true);
            Roar(CustomEvents.PlayPlayerAnimation, AnimationType.Run);
        }

        #endregion

        #region Public Methods

        public void Scale(Vector3 scale, bool finishStatus = false)
        {
            if (!finishStatus)
            {
                if ((playerModel.localScale.x < .3f && scale.x > 0) || (playerModel.localScale.y < .3f && scale.y > 0))
                {
                    playerModel.DOScale(Vector3.zero, .3f).SetEase(Ease.InBack)
                        .OnComplete(() => Roar(GameEvents.OnGameComplete, false))
                        .SetLink(gameObject);
                    return;
                }
            }


            playerModel.DOScale(playerModel.localScale + scale, 0.5f).SetEase(Ease.OutBack).SetLink(gameObject);
        }

        #endregion
    }
}