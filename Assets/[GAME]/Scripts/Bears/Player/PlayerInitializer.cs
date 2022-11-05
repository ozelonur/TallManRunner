#region Header
// Developed by Onur ÖZEL
#endregion

using _GAME_.Scripts.GlobalVariables;
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
        private GameObject _tempCharacter;

        #endregion

        #region MonoBehaviour Methods

        private void Start()
        {
            Initialize();
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.SwitchCharacter, SwitchCharacter);
            }

            else
            {
                UnRegister(CustomEvents.SwitchCamera, SwitchCharacter);
            }
        }

        private void SwitchCharacter(object[] args)
        {
            Initialize();
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            _playerBear = GetComponent<PlayerBear>();
            _playerAnimateBear = GetComponent<PlayerAnimateBear>();

            if (_tempCharacter != null)
            {
                Destroy(_tempCharacter);
            }
            
            _tempCharacter = Instantiate(DataManager.Instance.GetCurrentCharacter().Model, modelParent);
            Animator animator = _tempCharacter.AddComponent<Animator>();
            animator.runtimeAnimatorController = _playerBear.CharacterData.Animator;
            
            _playerAnimateBear.animator = animator;
        }

        #endregion
    }
}