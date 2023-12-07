using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class SelectAction : MonoBehaviour
{
    public GameLoop gameLoop;

    public GameObject characterSelectionPanel;
    public GameObject actionSelectionPanel;
    public GameObject targetSelectionPanel;
    public GameObject confirmButton;

    private Character selectedCharacter;
    public Abilities selectedAction;
    public Ascensions selectedAscension;
    public Character selectedTarget;

    public bool bothActionsSelected = false;

    public Character character1;
    public Abilities selectedActionCharacter1;
    public Ascensions selectedAscensionCharacter1;
    public List<Character> selectedTargetCharacter1;

    public Character character2;
    public Abilities selectedActionCharacter2;
    public Ascensions selectedAscensionsCharacter2;
    public List<Character> selectedTargetCharacter2;

    int iteration = 0;

    public Character enemy1;
    public Abilities selectedActionEnemy1;
    public Ascensions selectedAscensionEnemy1;
    public List<Character> selectedTargetEnemy1;

    public Character enemy2;
    public Abilities selectedActionEnemy2;
    public Ascensions selectedAscensionEnemy2;
    public List<Character> selectedTargetEnemy2;

    private void Awake()
    {
        gameLoop = FindObjectOfType<GameLoop>();

        this.enabled = false;
    }

    private void OnEnable()
    {
        characterSelectionPanel.SetActive(true);
        actionSelectionPanel.SetActive(false);
        targetSelectionPanel.SetActive(false);
        confirmButton.SetActive(false);

        gameLoop.InfoText.text = "Active lmao";
    }

    private void OnDisable()
    {
        characterSelectionPanel.SetActive(false);
        actionSelectionPanel.SetActive(false);
        targetSelectionPanel.SetActive(false);
        confirmButton.SetActive(false);
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
        gameLoop.InfoText.text = "Character selected: " + selectedCharacter.characterData.characterName;
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
                gameLoop.playerEnergy -= selectedCharacter.ability1Cost;
                break;
            case 3:
                selectedAction = selectedCharacter.Ability2;
                gameLoop.playerEnergy -= selectedCharacter.ability2Cost;
                break;
            case 4:
                selectedAscension = selectedCharacter.Ascension;
                break;
        }
        actionSelectionPanel.SetActive(false);
        targetSelectionPanel.SetActive(true);
        gameLoop.InfoText.text = "Action selected: " + selectedAction.ToString();
    }

    public void SelectActionTarget(string name)
    {
        selectedTarget = gameLoop.characters.Find((x) => x.characterData.characterName == name);
        targetSelectionPanel.SetActive(false);
        confirmButton.SetActive(true);
        gameLoop.InfoText.text = "Target selected: " + selectedTarget.characterData.characterName;
    }

    public void ConfirmAction()
    {
        if (iteration == 0)
        {
            character1 = selectedCharacter;
            selectedActionCharacter1 = selectedAction;
            selectedAscensionCharacter1 = selectedAscension;
            selectedTargetCharacter1.Add(selectedTarget);
        }
        if (iteration == 1)
        {
            character2 = selectedCharacter;
            selectedActionCharacter2 = selectedAction;
            selectedAscensionsCharacter2 = selectedAscension;
            selectedTargetCharacter2.Add(selectedTarget);
        }

        selectedCharacter = null;
        selectedAction = Abilities.None;
        selectedTarget = null;

        if (iteration == 1)
        {
            characterSelectionPanel.SetActive(false);
        }
        else if (iteration == 0)
        {
            gameLoop.InfoText.text = "Select second character";
            characterSelectionPanel.SetActive(true);
        }
        actionSelectionPanel.SetActive(false);
        targetSelectionPanel.SetActive(false);
        confirmButton.SetActive(false);

        if (iteration == 0)
        {
            iteration = 1;
            return;
        }
        if (iteration == 1)
        {
            iteration = 0;
            gameLoop.InfoText.text = "Both actions selected";
            RandomEnemy1Actions();
            RandomEnemy2Actions();
            BothActionsSelected();
        }
    }

    private void RandomEnemy1Actions()
    {
        enemy1 = gameLoop.characters.Find((x) => x.characterData.characterName == "Enemy1");

        EnemyAI ai = enemy1.GetComponent<EnemyAI>();

        selectedActionEnemy1 = ai.ChooseRandomAbility();

        selectedTargetEnemy1 = ai.ChooseRandomTarget();
    }

    private void RandomEnemy2Actions()
    {
        enemy2 = gameLoop.characters.Find((x) => x.characterData.characterName == "Enemy2");

        EnemyAI ai = enemy2.GetComponent<EnemyAI>();

        selectedActionEnemy2 = ai.ChooseRandomAbility();

        selectedTargetEnemy2 = ai.ChooseRandomTarget();
    }
}
