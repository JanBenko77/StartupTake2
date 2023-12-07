using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image healthbar;
    public Character character;

    public TMP_Text debugText;

    float healthPercentage;

    void Start()
    {
        character = GetComponent<Character>();
        debugText = GameObject.Find("SecondTest").GetComponent<TMP_Text>();
    }

    void Update()
    {
        healthPercentage = (float)character.currentHealth / (float)character.maxHealth;

        healthbar.fillAmount = healthPercentage;
    }
}
