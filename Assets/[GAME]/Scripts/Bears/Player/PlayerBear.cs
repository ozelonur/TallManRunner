#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Enums;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Interfaces;
using _ORANGEBEAR_.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Bears.Player
{
    public class PlayerBear : Bear
    {

        #region MonoBehaviour Methods

        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<ICollectable>()?.Collect();
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
    }
}