using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTrigger : MonoBehaviour
{
    public MazeGenerator mazeGenerator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Kazandin! Labirent yeniden basliyor.");

            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); --- Sahnenin basina atmak yerine gelistirmeler tamamlandigi icin finishte yeniden map generate edilir hale geitirildi.
            mazeGenerator.NextMaze();
        }
    }
}