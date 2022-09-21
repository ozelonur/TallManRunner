#region Header
// Developed by Onur ÖZEL
#endregion


using _GAME_.Scripts.GlobalVariables;
using _ORANGEBEAR_.Scripts.Bears;
using TMPro;
using UnityEngine;

namespace _GAME_.Scripts.Bears
{
    public class GameUIBear : UIBear
    {
        #region Serialized Fields

        [SerializeField] private TMP_Text currencyText;

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            base.CheckRoarings(status);
            
            if (status)
            {
                Register(CustomEvents.UpdateCurrency, UpdateCurrency);
            }

            else
            {
                UnRegister(CustomEvents.UpdateCurrency, UpdateCurrency);
            }
        }

        private void UpdateCurrency(object[] args)
        {
            currencyText.text = args[0].ToString();
        }

        #endregion
    }
}