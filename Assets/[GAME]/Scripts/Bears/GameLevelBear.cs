#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.GlobalVariables;
using _ORANGEBEAR_.EventSystem;
using _ORANGEBEAR_.Scripts.Bears;
using PathCreation;
using UnityEngine;

namespace _GAME_.Scripts.Bears
{
    public class GameLevelBear : LevelBear
    {
        #region Serialize Fields

        [Header("Path References")] [SerializeField]
        private PathCreator pathCreator;

        [Header("Follow Target")] [SerializeField]
        private Transform followTarget;

        [SerializeField] private EndOfPathInstruction endOfPathInstruction;

        [Header("Level Settings")] [SerializeField]
        private int maximumStackLimit;

        #endregion

        #region MonoBehaviour Methods

        private void Start()
        {
            Roar(CustomEvents.GetPath, pathCreator, endOfPathInstruction);
            Roar(CustomEvents.GetFollowTarget, followTarget);
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(GameEvents.InitLevel, InitLevel);
                Register(GameEvents.OnGameStart, OnGameStart);
            }

            else
            {
                Register(GameEvents.InitLevel, InitLevel);
                UnRegister(GameEvents.OnGameStart, OnGameStart);
            }
        }

        private void InitLevel(object[] args)
        {
            Roar(CustomEvents.GetMaximumStackCount, maximumStackLimit);
        }

        private void OnGameStart(object[] args)
        {
        }
        
        

        #endregion
    }
}