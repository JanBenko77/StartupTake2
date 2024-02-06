using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchToGame : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(switchAfter10sec());
    }

    public IEnumerator switchAfter10sec()
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(1);
    }
}
