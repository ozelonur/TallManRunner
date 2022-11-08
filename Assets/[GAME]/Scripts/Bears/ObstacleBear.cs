#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Bears.Player;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Interfaces;
using _ORANGEBEAR_.EventSystem;
using DG.Tweening;
using UnityEngine;

namespace _GAME_.Scripts.Bears
{
    public class ObstacleBear : Bear, IObstacle
    {
        [field: SerializeField] public int Worth { get; set; }

        public void HitToObstacle(params object[] args)
        {
            Worth = -Worth;

            PlayerBear playerBear = (PlayerBear)args[0];

            Vector3 scaleAmount = Vector3.one * (Worth / 100f);

            playerBear.Scale(scaleAmount);

            Roar(CustomEvents.GiveInfo, $"{Worth} Shrink", false);

            transform.DOScale(Vector3.zero, .15f).SetEase(Ease.InBack).SetLink(gameObject)
                .OnComplete(() => { Destroy(gameObject); });
        }
    }
}