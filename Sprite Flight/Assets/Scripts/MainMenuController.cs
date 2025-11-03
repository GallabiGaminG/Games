using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;  // sahne yüklemek için

public class MainMenuController : MonoBehaviour
{
    private Button startButton;
    private Button settingsButton;
    private Button exitButton;
    private VisualElement settingsPanel;
    private Slider musicSlider;
    private Slider sfxSlider;
    private UIDocument uiDocument;

    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;

        startButton = root.Q<Button>("StartButton");
        settingsButton = root.Q<Button>("SettingsButton");
        exitButton = root.Q<Button>("ExitButton");
        settingsPanel = root.Q<VisualElement>("SettingsPanel");
        musicSlider = root.Q<Slider>("MusicSlider");
        sfxSlider = root.Q<Slider>("SFXSlider");

        // Başlangıçta ayarlar kapalı
        settingsPanel.style.display = DisplayStyle.None;

        // Olaylar
        startButton.clicked += OnStartClicked;
        settingsButton.clicked += OnSettingsClicked;
        exitButton.clicked += OnExitClicked;
        musicSlider.RegisterValueChangedCallback(evt => OnMusicVolumeChange(evt.newValue));
        sfxSlider.RegisterValueChangedCallback(evt => OnSFXVolumeChange(evt.newValue));
    }

    void OnStartClicked()
    {
        // Ana sahneyi yükle (örnek: “Game”)
        SceneManager.LoadScene("Game");
    }

    void OnSettingsClicked()
    {
        // Ayar panelini aç/kapat
        settingsPanel.style.display =
            settingsPanel.style.display == DisplayStyle.None ? DisplayStyle.Flex : DisplayStyle.None;
    }

    void OnExitClicked()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    void OnMusicVolumeChange(float value)
    {
        AudioListener.volume = value; // global ses
    }

    void OnSFXVolumeChange(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
        PlayerPrefs.Save();
    }
}