using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadTestScene()
    {
        SceneManager.LoadScene("TestScene");
    }
}