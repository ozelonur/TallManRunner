#region Header
// Developed by Onur ÖZEL
#endregion

using _GAME_.Scripts.Enums;
using _GAME_.Scripts.Interfaces;
using _ORANGEBEAR_.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Bears.Player
{
    public class PlayerAnimateBear : Bear, IAnimator
    {
        #region Private Variables
        
        public Animator animator;

        #endregion

        #region Interface Methods

        public void PlayAnimation(AnimationType animationType)
        {
            ((IAnimator) this).SetAnimation(animator, animationType);
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

        private void OnGameStart(object[] args)
        {
            PlayAnimation(AnimationType.Run);
        }

        #endregion
    }
}