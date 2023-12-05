using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

public class SelectAction : MonoBehaviour
{
    public ARRaycastManager arRaycastManager;
    public ARPlaneManager arPlaneManager;
    public GameObject characterSelectionPanel;
    public GameObject actionSelectionPanel;
    public GameObject targetSelectionPanel;
    public Button confirmButton;

    private GameObject selectedCharacter;
    private string selectedAction;
    private GameObject selectedTarget;

    void Start()
    {
        characterSelectionPanel.SetActive(true);
        actionSelectionPanel.SetActive(false);
        targetSelectionPanel.SetActive(false);
        confirmButton.interactable = false;
    }

    void Update()
    {
        // Handle touch input for character selection
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId)) // Ensure UI elements are not being clicked
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Character")) // Replace "Character" with your character tag
                    {
                        SelectCharacter(hit.collider.gameObject);
                    }
                }
            }
        }
    }

    public void SelectCharacter(GameObject character)
    {
        selectedCharacter = character;
        characterSelectionPanel.SetActive(false);
        actionSelectionPanel.SetActive(true);
    }

    public void SelectCharacterAction(string action)
    {
        selectedAction = action;
        actionSelectionPanel.SetActive(false);
        targetSelectionPanel.SetActive(true);
    }

    public void SelectActionTarget(GameObject target)
    {
        selectedTarget = target;
        confirmButton.interactable = true;
    }

    public void ConfirmAction()
    {
        // Send the selected data to another script or process it as needed
        Debug.Log($"Character: {selectedCharacter.name}, Action: {selectedAction}, Target: {selectedTarget.name}");

        // Reset for the next action
        selectedCharacter = null;
        selectedAction = null;
        selectedTarget = null;

        // Reset UI panels and buttons
        characterSelectionPanel.SetActive(true);
        actionSelectionPanel.SetActive(false);
        targetSelectionPanel.SetActive(false);
        confirmButton.interactable = false;
    }
}
