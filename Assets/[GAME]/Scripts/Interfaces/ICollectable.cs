#region Header
// Developed by Onur ÖZEL
#endregion

namespace _GAME_.Scripts.Interfaces
{
    public interface ICollectable
    {
        public int Worth { get; set; }
        void Collect(params object[] args);
    }
}