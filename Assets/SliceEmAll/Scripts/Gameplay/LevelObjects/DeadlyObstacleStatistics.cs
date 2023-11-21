using GameAnalyticsSDK;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SliceEmAll.Gameplay.LevelObjects
{
    public class DeadlyObstacleStatistics : MonoBehaviour
    {
        public void RecordPlayerDeath()
        {
            Vector3 pos = transform.position;
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, SceneManager.GetActiveScene().name,
                    $"causeOfDeath({gameObject.name})", $"deathWorldLocation({pos.x}x_{pos.y}y)");
        }
    }
}