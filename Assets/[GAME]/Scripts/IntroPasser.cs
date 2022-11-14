#region Header
// Developed by Onur ÖZEL
#endregion

using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _GAME_.Scripts
{
    public class IntroPasser : MonoBehaviour
    {
        private void Start()
        {
            DOVirtual.DelayedCall(2f, () =>
            {
                SceneManager.LoadScene(1);
            });
        }
    }
}