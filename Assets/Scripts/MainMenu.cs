using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Quit Confirm Panel")]
    public GameObject confirmQuitPanel; // Kéo Panel xác nhận vào đây

    [Header("SettingPanel")]
    public GameObject settingPanel; // Kéo Panel Setting vào đây

    [Header("SavePanel")]
    public GameObject savePanel; // Kéo Panel Save vào đây

    public void LoadDemo()
    {
        SceneManager.LoadScene("Demo");
    }
    public void Loadsetting()
    { 
        if (settingPanel != null)
            settingPanel.SetActive(true); // Hiện bảng Setting

    }

    public void LoadSave()
    {
        if (savePanel != null)
            savePanel.SetActive(true); // Hiện bảng Save
    }
    // Khi bấm nút Quit
    public void ExitGame()
    {
        if (confirmQuitPanel != null)
            confirmQuitPanel.SetActive(true); // Hiện bảng xác nhận
    }

    //  Khi bấm YES
    public void ConfirmQuit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    public void back()

    {
        if (settingPanel != null)
            settingPanel.SetActive(false); // Ẩn bảng Setting

        if (savePanel != null)
            savePanel.SetActive(false); // Ẩn bảng Save
    }
    //  Khi bấm NO
    public void CancelQuit()
    {
        if (confirmQuitPanel != null)
            confirmQuitPanel.SetActive(false); // Ẩn bảng xác nhận
    }

    public void LoadFromSave()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadLastSave();
        }
        else
        {
            Debug.LogWarning("Chưa có GameManager trong scene MainMenu!");
        }
    }
}
