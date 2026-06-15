using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTrigger : MonoBehaviour
{
    public MazeGenerator mazeGenerator;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Finish trigger entered by: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player reached finish.");

            if (mazeGenerator != null)
            {
                mazeGenerator.NextMaze();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); --- Sahnenin basina atmak yerine gelistirmeler tamamlandigi icin finishte yeniden map generate edilir hale geitirildi.
            }
            else
            {
                Debug.LogError("MazeGenerator reference is missing.");
            }
        }
        else
        {
            Debug.Log("Entered object is not Player. Tag: " + other.tag);
        }
    }
}