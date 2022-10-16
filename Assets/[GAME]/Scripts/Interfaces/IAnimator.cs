#region Header
// Developed by Onur ÖZEL
#endregion

using _GAME_.Scripts.Enums;
using _GAME_.Scripts.GlobalVariables;
using UnityEngine;

namespace _GAME_.Scripts.Interfaces
{
    public interface IAnimator
    {
        void PlayAnimation(AnimationType animationType);

        virtual void SetAnimation(Animator animator, AnimationType animationState)
        {
            int animState = (int) animationState;

            if (!animator.GetCurrentAnimatorStateInfo(0).IsName(animationState.ToString()))
            {
                animator.SetInteger(GlobalStrings.Player, animState);
            }
        }
    }
}