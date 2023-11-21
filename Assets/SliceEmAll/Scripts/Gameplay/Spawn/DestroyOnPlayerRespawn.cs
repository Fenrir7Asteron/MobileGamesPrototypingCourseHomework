using UnityEngine;

namespace SliceEmAll.Gameplay.Spawn
{
    public class DestroyOnPlayerRespawn : MonoBehaviour
    {
        private InSceneLevelSwitcher _levelSwitcher;

        public void OnEnable()
        {
            _levelSwitcher = FindObjectOfType<InSceneLevelSwitcher>();
            _levelSwitcher.OnLevelStart += DestroySelf;
        }

        public void OnDisable()
        {
            _levelSwitcher.OnLevelStart -= DestroySelf;
        }

        private void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}