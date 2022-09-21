#region Header

// Developed by Onur ÖZEL

#endregion

using System.Collections;
using System.Collections.Generic;
using _GAME_.Scripts.GlobalVariables;
using _ORANGEBEAR_.EventSystem;
using Cinemachine;
using UnityEngine;
using CameraType = _GAME_.Scripts.Enums.CameraType;

namespace _GAME_.Scripts.Bears
{
    public class CameraBear : Bear
    {
        #region SerilizeFields
        
        [Header("Camera Shake Settings")] [SerializeField] [Range(0, 5)]
        private float intensity = 5f;

        [Header("Camera Shake Settings")] [SerializeField] [Range(0, 1)]
        private float time = .1f;

        [Header("Virtual Cameras")] [SerializeField]
        private CinemachineVirtualCamera mainVirtualCamera;

        [SerializeField] private CinemachineVirtualCamera finishVirtualCamera;

        #endregion

        #region Private Variables

        private Transform _followTarget;

        private Dictionary<CameraType, CinemachineVirtualCamera> _virtualCameras;
        
        private CinemachineBasicMultiChannelPerlin _perlin;
        
        private float _shakeTimer;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _virtualCameras = new Dictionary<CameraType, CinemachineVirtualCamera>
            {
                { CameraType.Main, mainVirtualCamera },
                { CameraType.Finish, finishVirtualCamera }
            };
            
            
            _perlin = mainVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.GetFollowTarget, GetFollowTarget);
                Register(CustomEvents.ShakeCamera, ShakeCamera);
                Register(CustomEvents.SwitchCamera, SwitchCamera);
            }

            else
            {
                UnRegister(CustomEvents.GetFollowTarget, GetFollowTarget);
                UnRegister(CustomEvents.ShakeCamera, ShakeCamera);
                UnRegister(CustomEvents.SwitchCamera, SwitchCamera);
            }
        }

        private void SwitchCamera(object[] args)
        {
            CameraType cameraType = (CameraType) args[0];
            
            SetCamera(cameraType);
        }

        private void ShakeCamera(object[] args)
        {
            StartCoroutine(Shake(time));
        }

        private void GetFollowTarget(object[] obj)
        {
            _followTarget = (Transform)obj[0];

            SetTarget(mainVirtualCamera, _followTarget);
            SetTarget(finishVirtualCamera, _followTarget);

            SetCamera(CameraType.Main);
        }

        #endregion

        #region Private Methods

        private void SetCamera(CameraType cameraType)
        {
            foreach (KeyValuePair<CameraType, CinemachineVirtualCamera> cameraValue in _virtualCameras)
            {
                cameraValue.Value.Priority = cameraValue.Key == cameraType ? 11 : 0;
            }
        }

        private void SetTarget(CinemachineVirtualCamera virtualCamera, Transform target)
        {
            virtualCamera.m_Follow = target;
        }
        
        private IEnumerator Shake(float timer)
        {
            _perlin.m_AmplitudeGain = intensity;
            _shakeTimer = timer;

            while (_shakeTimer > 0)
            {
                _shakeTimer -= Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }
            
            _perlin.m_AmplitudeGain = Mathf.Lerp(intensity, 0f, 1 - (_shakeTimer / timer));
        }

        #endregion
    }
}