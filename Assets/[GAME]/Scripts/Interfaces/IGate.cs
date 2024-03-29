﻿#region Header
// Developed by Onur ÖZEL
#endregion

using _GAME_.Scripts.Enums;

namespace _GAME_.Scripts.Interfaces
{
    public interface IGate
    {
        #region Properties

        public GateType gateType { get; set; }
        public DirectionType DirectionType { get; set; }
        public float Worth { get; set; }

        #endregion

        #region Methods

        void HitToGate(params object[] args);

        #endregion
    }
}