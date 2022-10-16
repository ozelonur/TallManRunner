#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Enums;
using _GAME_.Scripts.Interfaces;
using _ORANGEBEAR_.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Bears.Gate
{
    public class GateBear : Bear, IGate
    {
        #region Properties

        [field: SerializeField] public GateType gateType { get; set; }
        [field: SerializeField] public bool Worth { get; set; }

        #endregion

        public void HitToGate(params object[] args)
        {
            
        }
    }
}