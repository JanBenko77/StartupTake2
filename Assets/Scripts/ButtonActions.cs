using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour
{
    void Start()
    { }
    void Update()
    { }

    public void LoadPlayScene()
    {
        SceneManager.LoadScene(1);
    }


}
