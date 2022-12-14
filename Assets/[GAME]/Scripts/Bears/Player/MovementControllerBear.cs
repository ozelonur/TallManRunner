#region Header

// Developed by Onur ÖZEL

#endregion

using System;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.ScriptableObjects;
using _ORANGEBEAR_.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Bears
{
    public class MovementControllerBear : Bear
    {
        #region Public Variables

        public MovementData movementSettings;

        #endregion

        #region Serialized Fields

        [Header("Pivot Transforms")] [SerializeField]
        private Transform positionPivot;

        [SerializeField] private Transform rotationPivot;

        #endregion

        #region Private Variables

        private float _firstClick;
        private float _delta;
        private float _newYRotation;

        private Quaternion _currentRotation;

        private Vector3 _newPosition;
        private Vector3 _newDirection;

        private bool _canMove;

        private float _deltaHorizontalValue;

        #region Movement Settings

        private float _forwardSpeed;
        private float _horizontalSpeed;
        private float _horizontalClampRange;

        private bool _canRotate;
        private float _rotationSpeed;
        private float _rotationPower;
        private float _rotationClampRange;

        #endregion

        #endregion

        #region Actions

        private Action _inputAction;
        private Action _moveAction;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _forwardSpeed = movementSettings.ForwardSpeed;
            _horizontalSpeed = movementSettings.HorizontalSpeed;
            _horizontalClampRange = movementSettings.HorizontalClampRange;

            _canRotate = movementSettings.CanRotate;
            _rotationSpeed = movementSettings.RotationSpeed;
            _rotationPower = movementSettings.RotationPower;
            _rotationClampRange = movementSettings.RotationClampRange;
        }

        private void Start()
        {
            Roar(CustomEvents.GetForwardSpeed, _forwardSpeed);

            _inputAction += MouseDown;
            _inputAction += MouseHold;
            _inputAction += MouseUp;

            _moveAction += Slide;
            _moveAction += Rotate;
            _moveAction += Move;
        }

        private void Update()
        {
            if (!_canMove)
            {
                return;
            }

            _moveAction();
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.CanMoveHorizontal, CanMoveHorizontal);
                Register(CustomEvents.OnFinishLine, OnFinishLine);
            }

            else
            {
                UnRegister(CustomEvents.CanMoveHorizontal, CanMoveHorizontal);
                UnRegister(CustomEvents.OnFinishLine, OnFinishLine);
            }
        }

        private void OnFinishLine(object[] args)
        {
            _canMove = false;
        }

        private void CanMoveHorizontal(object[] obj)
        {
            _canMove = (bool)obj[0];
        }

        #endregion

        #region Private Methods

        private void Slide()
        {
            _inputAction();
        }

        private void Rotate()
        {
            _currentRotation = rotationPivot.rotation;
            _newYRotation = 0;

            if (!_canRotate || !_canMove)
            {
                return;
            }

            _currentRotation.y = _newYRotation;
            _newYRotation = Mathf.Clamp(_currentRotation.y + _deltaHorizontalValue * _rotationPower,
                -_rotationClampRange, _rotationClampRange);
            _currentRotation.y = _newYRotation;
            rotationPivot.localRotation =
                Quaternion.Lerp(rotationPivot.localRotation, _currentRotation, Time.deltaTime * _rotationSpeed);
        }

        private void Move()
        {
            if (_canMove)
            {
                _newDirection.x = _deltaHorizontalValue;
            }

            _newDirection.y = 0;

            Vector3 localPosition = positionPivot.localPosition;

            _newPosition = localPosition + _newDirection;

            localPosition = Vector3.Lerp(
                new Vector3(
                    Mathf.Clamp(_newPosition.x, -_horizontalClampRange,
                        _horizontalClampRange), _newPosition.y, 0f), localPosition,
                30 * Time.fixedDeltaTime);

            positionPivot.localPosition = localPosition;
        }

        #endregion

        #region Inputs

        private void MouseDown()
        {
            if (!Input.GetMouseButtonDown(0)) return;

            _deltaHorizontalValue = 0;
            _firstClick = Input.mousePosition.x;
        }

        private void MouseHold()
        {
            if (!Input.GetMouseButton(0)) return;

            _delta = Input.mousePosition.x - _firstClick;
            _deltaHorizontalValue = (_delta * _horizontalSpeed) * Time.fixedDeltaTime;

            _firstClick = Input.mousePosition.x;
        }

        private void MouseUp()
        {
            if (Input.GetMouseButtonUp(0))
            {
                _deltaHorizontalValue = 0;
            }
        }

        #endregion
    }
}