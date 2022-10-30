#region Header

// Developed by Onur ÖZEL

#endregion

using _GAME_.Scripts.Bears.Player;
using _GAME_.Scripts.Enums;
using _GAME_.Scripts.Interfaces;
using _GAME_.Scripts.Managers;
using _ORANGEBEAR_.EventSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _GAME_.Scripts.Bears.Gate
{
    public class GateBear : Bear, IGate
    {
        #region Properties

        [field: SerializeField] public GateType gateType { get; set; }
        [field: SerializeField] public DirectionType DirectionType { get; set; }
        [field: SerializeField] public float Worth { get; set; }

        #endregion

        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private TMP_Text worthText;

        [SerializeField] private MeshRenderer[] meshRenderers;
        [SerializeField] private Image gateImage;
        [SerializeField] private Image DirectionImage;
        [SerializeField] private Sprite verticalSprite;
        [SerializeField] private Sprite horizontalSprite;
        [SerializeField] private Color positiveColor;
        [SerializeField] private Color negativeColor;

        #endregion

        #region MonoBehaviour Methods

        private void Start()
        {
            switch (gateType)
            {
                case GateType.Positive:
                    SetColor(MaterialManager.Instance.positiveGateMaterial);
                    worthText.text = $"+{Worth}";
                    break;
                case GateType.Negative:
                    SetColor(MaterialManager.Instance.negativeGateMaterial);
                    worthText.text = $"-{Worth}";
                    Worth *= -1;
                    break;
                default:
                    Debug.LogError("Gate Type is not defined");
                    break;
            }

            switch (DirectionType)
            {
                case DirectionType.Vertical:
                    DirectionImage.sprite = verticalSprite;
                    break;
                case DirectionType.Horizontal:
                    DirectionImage.sprite = horizontalSprite;
                    break;
                default:
                    Debug.LogError("Direction Type is not defined");
                    break;
            }
        }

        #endregion

        public void HitToGate(params object[] args)
        {
            PlayerBear playerBear = (PlayerBear)args[0];

            Vector3 scaleAmount = Vector3.zero;

            switch (DirectionType)
            {
                case DirectionType.Vertical:
                    scaleAmount = Vector3.up * (Worth / 50f);
                    break;
                case DirectionType.Horizontal:
                    scaleAmount = Vector3.right * (Worth / 50f);
                    break;
                default:
                    Debug.LogWarning("Direction Type is not defined");
                    break;
            }
            
            playerBear.Scale(scaleAmount);
        }

        #region Private Methods

        private void SetColor(Material material)
        {
            foreach (var meshRenderer in meshRenderers)
            {
                meshRenderer.material = material;
            }
            
            gateImage.color = gateType == GateType.Positive ? positiveColor : negativeColor;
        }

        #endregion
    }
}