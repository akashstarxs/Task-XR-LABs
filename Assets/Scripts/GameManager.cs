using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Animator PlayerAnimator;
    [SerializeField]
    private Animator NPCAnimator;


    [SerializeField]
    private GameObject TextOverview;
    [SerializeField]
    private TMP_Text CCText;

    [SerializeField]
    private GameObject Pickupbutton;
    [SerializeField]
    private GameObject Dropdownbutton;
    [SerializeField]
    private GameObject Endgame;

    private Coroutine StoryCOT;
    public GameState gameState { get;private set; }

    public void ChangeGameState(GameState gameState)
    {
        this.gameState = gameState;
    }   

    public void TriggerStoryEvent(StoryTrigger storyevent)
    {
        storyevent.Character.SetTrigger(storyevent.AnimatorParametertoTrigger);
        storyevent.Character.ResetTrigger(storyevent.AnimatorParametertoDisable);
        TextOverview.SetActive(storyevent.ShouldshowCC);
        if (storyevent.ShouldshowCC)
            CCText.text = storyevent.Dailougestring;

    }
    public void TriggerStoryEvent(StoryTrigger storyevent,GameObject collectable)
    {
        StoryCOT = StartCoroutine(StoryCO(storyevent, collectable));
    }
    IEnumerator StoryCO(StoryTrigger storyevent,GameObject object1)
    {
        yield return new WaitForEndOfFrame();
        storyevent.Character.SetTrigger(storyevent.AnimatorParametertoTrigger);
        yield return new WaitForSeconds(3f);
        storyevent.Character.ResetTrigger(storyevent.AnimatorParametertoDisable);
        object1.SetActive(storyevent.ShouldshowCC);
        yield return new WaitForEndOfFrame();
        TextOverview.SetActive(storyevent.ShouldshowCC);
       // ChangeGameState(gameState + 1);
        if (storyevent.ShouldshowCC)
            CCText.text = storyevent.Dailougestring;
        yield return new WaitForEndOfFrame();

        StopCoroutine(StoryCOT);
    }

    public void StoryCase(GameState gameState, bool enable = false)
    {
        switch(this.gameState)
        {
            case GameState.Started:break;
            case GameState.Objective1:
                Pickupbutton.SetActive(enable);
                break;
            case GameState.Objective2:
                Dropdownbutton.SetActive(enable);
                
                break;
            case GameState.Finished:
                Endgame.SetActive(true);
                break;


        }
    }


    public void Reloadlevel()
    {
        SceneManager.LoadScene("Level1");
    }

}

[Serializable]
public class StoryTrigger
{
    public Animator Character;
    public string AnimatorParametertoTrigger;
    public string AnimatorParametertoDisable;
    public string Dailougestring;
    public bool ShouldshowCC;
}


public enum GameState
{
    Started,
    Objective1,
    Objective2,
    Finished
}
