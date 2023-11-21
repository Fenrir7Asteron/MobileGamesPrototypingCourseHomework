using GameAnalyticsSDK;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SliceEmAll.Gameplay.LevelObjects
{
    public class WinStone : MonoBehaviour
    {
        public void Win()
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, SceneManager.GetActiveScene().name);
        }
    }
}