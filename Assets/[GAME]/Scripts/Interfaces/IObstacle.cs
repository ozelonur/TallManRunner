#region Header
// Developed by Onur ÖZEL
#endregion

namespace _GAME_.Scripts.Interfaces
{
    public interface IObstacle
    {
        public int Worth { get; set; }
        void HitToObstacle(params object[] args);
    }
}