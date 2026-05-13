using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager: MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject loadPanel;
    public GameObject settingPanel;
    public GameObject quitPanel;

    // --- Nút PLAY ---
    public void OnPlayButton()
    {
        SceneManager.LoadScene("GameDemo"); // Đổi tên scene của bạn
    }

    // --- Nút LOAD ---
    public void OnLoadButton()
    {
        loadPanel.SetActive(true);
        settingPanel.SetActive(false);
        quitPanel.SetActive(false);
    }

    // --- Nút SETTING ---
    public void OnSettingButton()
    {
        settingPanel.SetActive(true);
        loadPanel.SetActive(false);
        quitPanel.SetActive(false);
    }

    // --- Nút QUIT ---
    public void OnQuitButton()
    {
        quitPanel.SetActive(true);
        loadPanel.SetActive(false);
        settingPanel.SetActive(false);
    }

    // --- Nút BACK chung cho các panel ---
    public void OnBackButton()
    {
        loadPanel.SetActive(false);
        settingPanel.SetActive(false);
        quitPanel.SetActive(false);
    }
}
