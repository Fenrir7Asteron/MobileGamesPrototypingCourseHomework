using UnityEngine;
using UnityEngine.SceneManagement;

namespace SliceEmAll.Architecture
{
    public class GameStart : MonoBehaviour
    {
        private Networking.ServerConnector _connector;

        public void Start()
        {
            GameObject networkManager = new GameObject("NetworkManager");
            _connector = networkManager.AddComponent<Networking.ServerConnector>();
            _connector.Init();
            _connector.ConnectToServer();
            DontDestroyOnLoad(networkManager);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } 
    }
}
