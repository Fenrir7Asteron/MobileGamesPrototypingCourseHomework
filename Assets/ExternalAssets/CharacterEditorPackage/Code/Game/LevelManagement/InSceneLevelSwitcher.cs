using UnityEngine;
using System.Collections;
using System;

//--------------------------------------------------------------------
//InSceneLevelSwitcher keeps track of spawnpoints and respawning
//Switches camera to te one used in that level
//--------------------------------------------------------------------
public class InSceneLevelSwitcher : MonoBehaviour {
    //Level start event (for other scripts to use when the level is changed)
    public delegate void OnLevelStartEvent();
    public event OnLevelStartEvent OnLevelStart;
    [SerializeField] SliceEmAll.Networking.GameLobbyManager _lobbyManager = null;
    [SerializeField] InSceneLevel[] m_Levels = null;
    [SerializeField] int m_ButtonSize = 0;
    [SerializeField] int m_ButtonsPerRow = 0;
    [SerializeField] Transform m_Camera = null;
    int m_CurrentIndex;
    private CharacterControllerBase m_Character = null;
    private ButtonInput m_RespawnInput;

    public void Awake()
    {
        if (_lobbyManager != null)
        {
            _lobbyManager.PlayerSpawned += (GameObject x) => 
            {
                SetPlayer(x.GetComponent<CharacterControllerBase>());
                StartLevel(0);
                CorrectCamera();
            };   
        }
    }

    void OnGUI()
    {
        for (int i = 0; i < m_Levels.Length; i ++)
        {
            int xIndex = (i) % (m_ButtonsPerRow);
            int yIndex = i / m_ButtonsPerRow;
            int xPos = Screen.width - m_ButtonsPerRow * m_ButtonSize + (xIndex) * m_ButtonSize;
            int yPos = yIndex * m_ButtonSize;

            int index = i;
            if (GUI.Button(new Rect(xPos, yPos, m_ButtonSize, m_ButtonSize), (index+1).ToString()))
            {
                StartLevel(index);
                CorrectCamera();
            }
        }
    }

    void Update()
    {
        if (m_RespawnInput != null && m_RespawnInput.m_WasJustPressed)
        {
            m_RespawnInput.m_WasJustPressed = false;
            Respawn();
        }
    }

    public void SetIndex(int a_Index)
    {
        m_CurrentIndex = a_Index;
    }

    public void Respawn()
    {
        StartLevel(m_CurrentIndex);
        CorrectCamera();
    }
    void CorrectCamera()
    {
        Vector3 diff = m_Character.transform.position - m_Camera.transform.position;
        diff.z = 0;
        m_Camera.transform.position += diff;
    }
    void StartLevel(int a_Index)
    {
        if (a_Index >= m_Levels.Length)
        {
            return;
        }
        m_Character.SpawnAndResetAtPosition(m_Levels[a_Index].m_StartPoint.position);
		m_CurrentIndex = a_Index;
        if (OnLevelStart != null)
        {
            OnLevelStart();
        }    
    }

    public void SetPlayer(CharacterControllerBase playerObject)
    {
        m_Character = playerObject;

        PlayerInput playerInput = playerObject.GetPlayerInput();
        if (playerInput.GetButton("Respawn") != null)
        { 
            m_RespawnInput = playerInput.GetButton("Respawn");
        }
        else
        {
            Debug.LogError("Respawn input not set up in character input");
        }
    }
}
