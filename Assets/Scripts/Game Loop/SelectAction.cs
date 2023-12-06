using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
using UnityEditor.Build.Content;

public class SelectAction : MonoBehaviour
{
    public GameLoop gameLoop;

    public GameObject characterSelectionPanel;
    public GameObject actionSelectionPanel;
    public GameObject targetSelectionPanel;
    public Button confirmButton;

    private Character selectedCharacter;
    public Abilities selectedAction;
    public Ascensions selectedAscension;
    private Character selectedTarget;

    public bool bothActionsSelected = false;



    /*
     
     
    FUCK TAPPING ON THE SCREEN TO GET THE CHARACTER TO DO SHIT, MAKE THEM CHOOSE WITH OPENED PANELS 


    */





    private void Awake()
    {
        gameLoop = FindObjectOfType<GameLoop>();
        actionSelectionPanel = GameObject.Find("AbilitySelectPanel");
        targetSelectionPanel = GameObject.Find("TargetSelectPanel");

        this.enabled = false;
    }

    private void OnEnable()
    {
        characterSelectionPanel.SetActive(true);
        actionSelectionPanel.SetActive(false);
        targetSelectionPanel.SetActive(false);
        confirmButton.interactable = false;

        //DebugText.text = "Script was set active";
    }

    void Update()
    {

    }

    public void ResetActions()
    {
        bothActionsSelected = false;
    }

    public void BothActionsSelected()
    {
        bothActionsSelected = true;
    }

    public void SelectCharacter(string name)
    {
        selectedCharacter = gameLoop.characters.Find((x) => x.characterData.characterName == name);
        characterSelectionPanel.SetActive(false);
        actionSelectionPanel.SetActive(true);
    }

    public void SelectCharacterAction(int i)
    {
        switch (i)
        {
            case 1:
                selectedAction = Abilities.BasicAttack;
                break;
            case 2:
                selectedAction = selectedCharacter.Ability1;
                break;
            case 3:
                selectedAction = selectedCharacter.Ability2;
                break;
            case 4:
                selectedAscension = selectedCharacter.Ascension;
                break;
        }
        actionSelectionPanel.SetActive(false);
        targetSelectionPanel.SetActive(true);
    }

    public void SelectActionTarget(string target)
    {
        selectedTarget = gameLoop.characters.Find((x) => x.characterData.characterName == target);
        confirmButton.enabled = true;
        confirmButton.interactable = true;
    }

    private void OnConfirmAction()
    {

    }

    public void ConfirmAction()
    {


        selectedCharacter = null;
        selectedAction = Abilities.None;
        selectedTarget = null;

        // Reset UI panels and buttons
        characterSelectionPanel.SetActive(true);
        actionSelectionPanel.SetActive(false);
        targetSelectionPanel.SetActive(false);
        confirmButton.enabled = false;
        confirmButton.interactable = false;
    }
}
