using UnityEngine;

public class SaveMenuToggle : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private SaveMenuUI saveMenuUI; // kéo SaveMenuUI vào đây
    [SerializeField] private KeyCode toggleKey = KeyCode.O;

    [Header("Pause while open")]
    [SerializeField] private bool pauseGame = true; // mở menu thì pause game
    private bool isOpen;

    private void Awake()
    {
        // nếu quên kéo, tự tìm trong scene
        if (saveMenuUI == null)
            saveMenuUI = FindFirstObjectByType<SaveMenuUI>(FindObjectsInactive.Include);
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        if (saveMenuUI == null || saveMenuUI.panel == null)
        {
            Debug.LogWarning("SaveMenuToggle: Chưa gán SaveMenuUI hoặc panel!");
            return;
        }

        isOpen = !isOpen;

        if (isOpen) saveMenuUI.OpenMenu();
        else saveMenuUI.CloseMenu();

        if (pauseGame)
            Time.timeScale = isOpen ? 0f : 1f;
    }

    private void OnDisable()
    {
        // tránh kẹt pause nếu object bị disable
        if (pauseGame) Time.timeScale = 1f;
    }
}
