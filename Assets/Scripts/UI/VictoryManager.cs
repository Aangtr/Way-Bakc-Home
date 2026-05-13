using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
    public static VictoryManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private GameObject victoryPanel;   // Panel Victory (Canvas)
    [SerializeField] private GameObject needKeysPanel;  // (Tuỳ chọn) panel báo thiếu key

    [Header("Pause")]
    [SerializeField] private bool pauseGameOnWin = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // Nếu bạn muốn manager này sống qua scene thì bật dòng dưới
        // DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (victoryPanel != null) victoryPanel.SetActive(false);
        if (needKeysPanel != null) needKeysPanel.SetActive(false);
    }

    public void ShowVictory()
    {
        if (victoryPanel != null) victoryPanel.SetActive(true);

        if (pauseGameOnWin)
            Time.timeScale = 0f;
    }

    public void HideVictory()
    {
        if (victoryPanel != null) victoryPanel.SetActive(false);
        if (needKeysPanel != null) needKeysPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    // Button: Restart
    public void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Button: Menu (nhớ add scene MainMenu vào Build Settings)
    public void GoToMenu(string menuSceneName = "MainMenu")
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    // (Tuỳ chọn) Nếu thiếu key thì bật panel cảnh báo
    public void ShowNeedKeys()
    {
        if (needKeysPanel != null)
        {
            needKeysPanel.SetActive(true);
            CancelInvoke(nameof(HideNeedKeys));
            Invoke(nameof(HideNeedKeys), 1.2f);
        }
    }

    private void HideNeedKeys()
    {
        if (needKeysPanel != null) needKeysPanel.SetActive(false);
    }
}
