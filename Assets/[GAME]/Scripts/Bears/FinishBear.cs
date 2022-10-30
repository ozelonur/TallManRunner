#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Bears.Player;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Managers;
using _ORANGEBEAR_.EventSystem;
using DG.Tweening;
using UnityEngine;
using CameraType = _GAME_.Scripts.Enums.CameraType;

namespace _GAME_.Scripts.Bears
{
    public class FinishBear : Bear
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private Transform targetJumpTransform;

        #endregion

        #region Private Variables

        private bool _canPlayerMove;
        private Transform _moveTransform;

        #endregion

        #region MonoBehaviour Methods

        private void Update()
        {
            if (!_canPlayerMove)
            {
                return;
            }

            _moveTransform.Translate(Vector3.forward * (Time.deltaTime * 5f));
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.OnFinishLine, OnFinishLine);
                Register(CustomEvents.PlayerFinishMovement, OnPlayerFinishMovement);
            }

            else
            {
                UnRegister(CustomEvents.OnFinishLine, OnFinishLine);
                UnRegister(CustomEvents.PlayerFinishMovement, OnPlayerFinishMovement);
            }
        }

        private void OnPlayerFinishMovement(object[] args)
        {
            bool status = (bool)args[0];

            _canPlayerMove = status;
        }

        private void OnFinishLine(object[] args)
        {
            PlayerBear player = PlayerManager.Instance.currentPlayer;
            Roar(CustomEvents.SwitchCamera, CameraType.Finish);

            var parent = player.transform.parent;
            _moveTransform = parent;

            player.transform.DOMoveX(0, .3f).SetEase(Ease.Linear).SetLink(player.gameObject);

            parent.DOJump(targetJumpTransform.position, 1, 1, 1f).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    _canPlayerMove = true;
                    Roar(CustomEvents.PlayerOnFinish);
                })
                .SetLink(gameObject);
        }

        #endregion
    }
}