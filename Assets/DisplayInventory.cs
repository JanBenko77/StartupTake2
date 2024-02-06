using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObj collectedInventory;
    public InventoryObj uncollectedInventory;
    [SerializeField] private GameObject textPrefab;
    private float SPACE_BETWEEN_ITEMS;
    private int NUMBER_OF_COLUMN; 
    private float cardHeight;
    private Vector3 start_uncol;
    Dictionary <InventorySlot, GameObject> itemsDisplayed = new Dictionary <InventorySlot, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        cardHeight = (collectedInventory.Container[0].card.prefab.GetComponent<RectTransform>().rect.height * collectedInventory.Container[0].card.prefab.GetComponent<RectTransform>().localScale.x);
        NUMBER_OF_COLUMN = (int)MathF.Floor(Screen.width / ((collectedInventory.Container[0].card.prefab.GetComponent<RectTransform>().rect.width * collectedInventory.Container[0].card.prefab.GetComponent<RectTransform>().localScale.x) + 0.0001f));
        SPACE_BETWEEN_ITEMS = (Screen.width - (collectedInventory.Container[0].card.prefab.GetComponent<RectTransform>().rect.width * collectedInventory.Container[0].card.prefab.GetComponent<RectTransform>().localScale.x * NUMBER_OF_COLUMN)) / (NUMBER_OF_COLUMN + 1);
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateDisplay();
    }
    public void CreateDisplay()
    {
        for (int i = 0; i < collectedInventory.Container.Count; ++i)
        {
            var obj = Instantiate(collectedInventory.Container[i].card.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            GetTextChild(obj, "health").text = "health: " + collectedInventory.Container[i].card.health.ToString();
            GetTextChild(obj, "characterName").text = collectedInventory.Container[i].card.characterName;
            start_uncol = new Vector3(0, obj.GetComponent<RectTransform>().localPosition.y, 0);
        }
        for (int i = 0; i < uncollectedInventory.Container.Count; ++i)
        {
            var obj = Instantiate(uncollectedInventory.Container[i].card.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i) + start_uncol - new Vector3 (0, cardHeight, 0);
            GetTextChild(obj, "health").text = "health: " + uncollectedInventory.Container[i].card.health.ToString();
            GetTextChild(obj, "characterName").text = uncollectedInventory.Container[i].card.characterName;
        }
    }

    public TextMeshProUGUI GetTextChild(GameObject obj, string Name)
    {
        foreach (var obj_ in obj.GetComponentsInChildren<TextMeshProUGUI>())
        {
            if (obj_.name == Name)
            {
                return obj_;
            }
        }
        return null;
    }
    
    public void UpdateDisplay()
    {
        for (int i = 0;i < collectedInventory.Container.Count;++i)
        {
            if (itemsDisplayed.ContainsKey(collectedInventory.Container[i]))
            {
                return;
            }
            var obj = Instantiate(collectedInventory.Container[i].card.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = collectedInventory.Container[i].card.characterName;
            itemsDisplayed.Add(collectedInventory.Container[i], obj);
        }
    }

    public Vector3 GetPosition(int i)
    {
        var xPos = SPACE_BETWEEN_ITEMS + ((Screen.width - SPACE_BETWEEN_ITEMS) / NUMBER_OF_COLUMN) * (i % NUMBER_OF_COLUMN);
        var yPos = -SPACE_BETWEEN_ITEMS - ((cardHeight + SPACE_BETWEEN_ITEMS) * Mathf.Floor(i / NUMBER_OF_COLUMN));
        return new Vector3(xPos, yPos, 0f);
    }

}
