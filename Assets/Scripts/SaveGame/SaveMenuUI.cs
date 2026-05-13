using UnityEngine;

public class SaveMenuUI : MonoBehaviour
{
    public GameObject panel;

    private void Start()
    {
        if (panel != null)
            panel.SetActive(false);
    }

    public void OpenMenu()
    {
        if (panel != null) panel.SetActive(true);
    }

    public void CloseMenu()
    {
        if (panel != null) panel.SetActive(false);
    }

    public void OnSaveButton()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.SaveCurrentGame();
    }

    public void OnLoadButton()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.LoadLastSave();
    }
}
