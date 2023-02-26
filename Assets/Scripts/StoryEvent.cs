using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEvent : MonoBehaviour
{
     StoryEvent m_StoryEvent;
    [SerializeField] StoryTrigger m_StoryEntryTrigger;
    [SerializeField] StoryTrigger m_StoryExitTrigger;
    [SerializeField] GameManager m_GameManager;
    [SerializeField] GameState m_GameState;
    [SerializeField] bool hasstateevents;
    [SerializeField] bool ShouldUpdatestate;

    [SerializeField] GameObject m_nextpos;
    [SerializeField] GameObject m_curremtpos;


    [SerializeField]
    private GameObject collectable;
    private void Start()
    {
        m_StoryEvent = this;
        if(m_GameManager==null)
            m_GameManager = GameObject.FindObjectOfType<GameManager>();
    }
    public void OnUserInteraction()
    {
        if (m_GameManager.gameState == m_GameState)
        {
            m_GameManager.TriggerStoryEvent(m_StoryEntryTrigger, collectable);

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(m_GameManager.gameState == m_GameState)
        {
            m_GameManager.TriggerStoryEvent(m_StoryEntryTrigger);
            
            m_GameManager.StoryCase(m_GameState,hasstateevents);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (m_GameManager.gameState == m_GameState)
        {
            m_GameManager.TriggerStoryEvent(m_StoryExitTrigger);
            m_GameManager.StoryCase(m_GameState);
            m_nextpos.SetActive(true);
            m_curremtpos.SetActive(false);
            if (ShouldUpdatestate)
                m_GameManager.ChangeGameState(m_GameState+1);

        }
    }
}

