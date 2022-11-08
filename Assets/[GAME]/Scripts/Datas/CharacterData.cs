#region Header
// Developed by Onur ÖZEL
#endregion

using UnityEngine;

namespace _GAME_.Scripts.Datas
{
    [CreateAssetMenu(fileName = "Data", menuName = "Datas/CharacterData", order = 1)]
    public class CharacterData : ScriptableObject
    {
        public int Price;
        public bool Unlocked;
        public GameObject Model;
        public RuntimeAnimatorController Animator;
        public string CharacterName;
        public Sprite heroSprite;
    }
}