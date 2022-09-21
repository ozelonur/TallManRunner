#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Enums;
using _GAME_.Scripts.GlobalVariables;
using _ORANGEBEAR_.EventSystem;
using PathCreation;
using UnityEngine;

namespace _GAME_.Scripts.Bears
{
    public class PathFollowerBear : Bear
    {
        #region Private Variables

        private PathCreator _pathCreator;
        private EndOfPathInstruction _endOfPathInstruction;

        private float _forwardSpeed;

        private bool _canFollowPath;
        private float _pathDistance;

        #endregion

        #region MonoBehaviour Methods

        private void Update()
        {
            if (_pathCreator == null)
            {
                return;
            }

            if (!_canFollowPath)
            {
                return;
            }

            MoveForward();
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.GetPath, GetPath);
                Register(CustomEvents.GetForwardSpeed, GetForwardSpeed);
                Register(CustomEvents.CanFollowPath, CanFollowPath);
            }

            else
            {
                UnRegister(CustomEvents.GetPath, GetPath);
                UnRegister(CustomEvents.GetForwardSpeed, GetForwardSpeed);
                UnRegister(CustomEvents.CanFollowPath, CanFollowPath);
            }
        }

        private void CanFollowPath(object[] obj)
        {
            _canFollowPath = (bool)obj[0];
        }

        private void GetForwardSpeed(object[] obj)
        {
            _forwardSpeed = (float)obj[0];
        }

        private void GetPath(object[] obj)
        {
            _pathCreator = (PathCreator)obj[0];
            _endOfPathInstruction = (EndOfPathInstruction)obj[1];


            _pathCreator.pathUpdated += PathUpdated;
        }

        #endregion

        #region Private Methods

        private void PathUpdated()
        {
            _pathDistance = 0;
            transform.position = _pathCreator.path.GetPointAtDistance(_pathDistance, _endOfPathInstruction);
        }

        private void MoveForward()
        {
            if (_pathDistance >= _pathCreator.path.length)
            {
                _canFollowPath = false;
                Roar(CustomEvents.OnFinishLine);
                Roar(CustomEvents.PlayPlayerAnimation, AnimationType.Idle);
                return;
            }
            _pathDistance += _forwardSpeed * Time.deltaTime;
            transform.position = _pathCreator.path.GetPointAtDistance(_pathDistance, _endOfPathInstruction);
            transform.rotation = _pathCreator.path.GetRotationAtDistance(_pathDistance, _endOfPathInstruction);
        }

        #endregion
    }
}