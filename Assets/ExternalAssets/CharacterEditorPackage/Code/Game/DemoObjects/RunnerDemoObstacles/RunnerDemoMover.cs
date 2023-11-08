using UnityEngine;
using System.Collections;
//--------------------------------------------------------------------
//Small class to move an object back and forth along a path. Used for moving platforms in levels
//This child class adds reset capabilities when a respawn happens
//--------------------------------------------------------------------
public class RunnerDemoMover: Mover
{
    [SerializeField] InSceneLevelSwitcher m_LevelSwitcher;
    [SerializeField] float m_StartTime = 0.0f;

    void OnEnable()
    {
        m_Time = m_StartTime;
        m_StartPosition = transform.position;
        m_LevelSwitcher.OnLevelStart += ResetStart;
    }

    void OnDisable()
    {
        m_LevelSwitcher.OnLevelStart -= ResetStart;
    }

    void ResetStart()
    {
        m_Time = m_StartTime;
    }

}
