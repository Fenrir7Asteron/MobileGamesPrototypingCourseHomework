using UnityEngine;
using UnityEngine.SceneManagement;

namespace SliceEmAll.Architecture
{
    public class SceneValidator : MonoBehaviour
    {
        public void Awake()
        {
            // Load startup scene if network manager is not setup.
            if (Networking.ServerConnector.Instance == null)
            {
                LoadStartupScene();
            }
        }

        private static void LoadStartupScene()
        {
            SceneManager.LoadScene(0);
        }
    }
}
