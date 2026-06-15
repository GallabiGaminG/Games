using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Takip Edilecek Oyuncu")]
    public Transform player;

    [Header("Oyuncuya Gore Kamera Mesafesi")]
public Vector3 offset = new Vector3(0f, 25f, -20f);

    [Header("Kameranin Sabit Acisi")]
    public Vector3 fixedRotation = new Vector3(55f, 0f, 0f);

    void LateUpdate()
    {
        if (player == null) return;

        // Kamera oyuncunun arkasinda/yukarisinda sabit mesafeyle durur.
        transform.position = player.position + offset;

        // Kamera oyuncuya LookAt ile takip etme hatasi duzeldi; hep bu sabit aciyla bakar.
        transform.rotation = Quaternion.Euler(fixedRotation);
    }
}