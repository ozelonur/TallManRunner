﻿#region Header

// Developed by Onur ÖZEL

#endregion

namespace _GAME_.Scripts.GlobalVariables
{
    /// <summary>
    /// This class is used to store Custom Events that you create.
    /// </summary>
    public class CustomEvents
    {
        public const string GetPath = nameof(GetPath);
        public const string GetForwardSpeed = nameof(GetForwardSpeed);
        public const string CanFollowPath = nameof(CanFollowPath);
        public const string OnFinishLine = nameof(OnFinishLine);
        public const string PlayPlayerAnimation = nameof(PlayPlayerAnimation);
        public const string CanMoveHorizontal = nameof(CanMoveHorizontal);
        public const string SwitchCamera = nameof(SwitchCamera);
        public const string GetFollowTarget = nameof(GetFollowTarget);
        public const string GetMaximumStackCount = nameof(GetMaximumStackCount);
    }
}