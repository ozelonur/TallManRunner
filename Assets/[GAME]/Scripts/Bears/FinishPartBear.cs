#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Bears.Player;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Interfaces;
using _GAME_.Scripts.Managers;
using _ORANGEBEAR_.EventSystem;
using DG.Tweening;
using UnityEngine;

namespace _GAME_.Scripts.Bears
{
    public class FinishPartBear : Bear, IFinishPart
    {
        #region Serialized Fields

        [SerializeField] private Transform barrier;
        [SerializeField] private Renderer[] multipliers;
        [SerializeField] private ParticleSystem[] particles;
        [SerializeField] private ParticleSystem explosionParticle;
        [SerializeField] private Color color;

        #endregion

        #region Private Variables
        
        private PlayerBear _playerBear;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            foreach (var multiplier in multipliers)
            {
                multiplier.material.color = Color.white;
            }
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.PlayerOnFinish, PlayerOnFinish);
            }

            else
            {
                UnRegister(CustomEvents.PlayerOnFinish, PlayerOnFinish);
            }
        }

        private void PlayerOnFinish(object[] args)
        {
           _playerBear = PlayerManager.Instance.currentPlayer;
        }

        #endregion

        public void HitToFinishPart(params object[] args)
        {
            if (_playerBear.transform.localScale.x < .3f || _playerBear.transform.localScale.y < .3f)
            {
               Roar(CustomEvents.PlayerFinishMovement, false);
                _playerBear.transform.DOScale(Vector3.zero, .3f)
                    .OnComplete(() => Roar(GameEvents.OnGameComplete, true))
                    .SetEase(Ease.InBack)
                    .SetLink(_playerBear.gameObject);
                return;
            }

            barrier.DOScale(Vector3.zero, .15f).SetEase(Ease.InBack).SetLink(barrier.gameObject);
            _playerBear.Scale(new Vector3(-.05f, -.05f, -.05f), true);
            explosionParticle.Play();
            foreach (var multiplier in multipliers)
            {
                multiplier.material.DOColor(color, .3f).SetEase(Ease.Linear).SetLink(multiplier.gameObject);
            }

            foreach (var particle in particles)
            {
                particle.Play();
            }
        }
    }
}