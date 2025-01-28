using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class RogeLikeMenu : MonoBehaviour
{

    [Header("General Refreance Via Code")]
    [SerializeField] UIDocument uIDocument;
    [SerializeField] VisualTreeAsset mainMenu;
    [SerializeField] VisualTreeAsset gameSetting;
    [SerializeField] Button playBotton;
    [SerializeField] Button settingBotton;
    [SerializeField] Button quitBotton;
    [SerializeField] Button backBotton;
    [Header("Mute botton")]
    [SerializeField] Button muteBotton;
    [SerializeField] Sprite mute;
    [SerializeField] Sprite unMute;
    [SerializeField] bool muted;

    void Awake()
    {
        HandleMainMenu();
    }

    private void HandleMainMenu()
    {
        uIDocument = GetComponent<UIDocument>();
        muted = true;
        // Play Botton
        playBotton = uIDocument.rootVisualElement.Q<Button>("Play");
        playBotton.clicked += PlayBottonOnClick;

        // Setting Botton
        settingBotton = uIDocument.rootVisualElement.Q<Button>("Settings");
        settingBotton.clicked += OpenSettingsUIOnClick;

        // Mute Botton
        muteBotton = uIDocument.rootVisualElement.Q<Button>("MuteBotton");
        muteBotton.clicked += HandleMuteBotton;

        // Quit Botton
        quitBotton = uIDocument.rootVisualElement.Q<Button>("Quit");
        quitBotton.clicked += Quiting;
    }

    private void backToMainMenu()
    {
        uIDocument.visualTreeAsset = mainMenu;
        HandleMainMenu();

    }

    private void PlayBottonOnClick()
    {
        SceneManager.LoadScene(1);
    }

    // Back Botton
    private void OpenSettingsUIOnClick()
    {
        Debug.Log("Opening UI");
        uIDocument.visualTreeAsset = gameSetting;
        backBotton = uIDocument.rootVisualElement.Q<Button>("Back");
        backBotton.clicked += backToMainMenu;
    }

    private void Quiting()
    {
        Application.Quit();
    }

    private void HandleMuteBotton()
    {
        muted = !muted;

        var backGroundIcon = muteBotton.style.backgroundImage;
        backGroundIcon.value = Background.FromSprite(muted ? mute : unMute);
        muteBotton.style.backgroundImage = backGroundIcon;

        AudioListener.volume = muted ? 1 : 0;
    }
}
