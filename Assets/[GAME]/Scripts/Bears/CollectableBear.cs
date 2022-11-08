#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Interfaces;
using _GAME_.Scripts.Managers;
using _ORANGEBEAR_.EventSystem;
using _ORANGEBEAR_.Scripts.Managers;
using UnityEngine;

namespace _GAME_.Scripts.Bears
{
    public class CollectableBear : Bear, ICollectable
    {
        [field: SerializeField] public int Worth { get; set; }

        public void Collect(params object[] args)
        {
            DataManager.Instance.levelDiamondCount += Worth;
            AudioManager.Instance.PlayCoinCollectSound();
            DataManager.Instance.AddDiamond(Worth);
            Destroy(gameObject);
        }
    }
}