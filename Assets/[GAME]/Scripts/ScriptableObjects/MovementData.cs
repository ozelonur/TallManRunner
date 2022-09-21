#region Header

// Developed by Onur ÖZEL

#endregion

using UnityEngine;

namespace _GAME_.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Settings/Movement Settings")]
    public class MovementData : ScriptableObject
    {
        #region Serialize Fields

        [Header("Movement Settings")] [SerializeField]
        private float forwardSpeed = 10f;

        [SerializeField] private float horizontalSpeed = 10f;


        [SerializeField] private float horizontalClampRange = 4f;

        [Header("Rotation Settings")] [SerializeField]
        private float rotationSpeed = 1f;

        [SerializeField] private bool canRotate;
        [SerializeField] private float rotationPower = 1f;
        [SerializeField] private float rotationClampRange = 4f;

        #endregion

        #region Properties

        public float ForwardSpeed => forwardSpeed;
        public float HorizontalSpeed => horizontalSpeed;

        public float HorizontalClampRange => horizontalClampRange;

        public float RotationSpeed => rotationSpeed;

        public bool CanRotate => canRotate;

        public float RotationPower => rotationPower;

        public float RotationClampRange => rotationClampRange;

        #endregion
    }
}