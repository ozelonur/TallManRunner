#region Header
// Developed by Onur ÖZEL
#endregion

using System;
using _ORANGEBEAR_.EventSystem;
using _ORANGEBEAR_.Scripts.Managers;
using UnityEngine;

namespace _GAME_.Scripts.Bears.Player
{
    public class PlayerInitializer : Bear
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private Transform modelParent;

        #endregion

        #region Private Variables

        private PlayerBear _playerBear;
        private PlayerAnimateBear _playerAnimateBear;

        #endregion

        #region MonoBehaviour Methods

        private void Start()
        {
            Initialize();
        }

        #endregion

        #region Public Methods

        public void Initialize()
        {
            _playerBear = GetComponent<PlayerBear>();
            _playerAnimateBear = GetComponent<PlayerAnimateBear>();
            
            GameObject character = Instantiate(DataManager.Instance.GetCurrentCharacter().Model, modelParent);
            Animator animator = character.AddComponent<Animator>();
            animator.runtimeAnimatorController = _playerBear.CharacterData.Animator;
            
            _playerAnimateBear.animator = animator;
        }

        #endregion
    }
}