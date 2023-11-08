using UnityEngine;
using System.Collections;
//--------------------------------------------------------------------
//Small class to rotate an object back and forth a certain angle. Used for moving platforms in levels
//This child class adds reset capabilities when a respawn happens
//--------------------------------------------------------------------
public class RunnerDemoBalancer: Balancer
{
    [SerializeField] InSceneLevelSwitcher m_LevelSwitcher;
    [SerializeField] float m_StartTime = 0.0f;

    void OnEnable()
    {
        m_Time = m_StartTime;
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
