using UnityEngine;

public class QuickSave : MonoBehaviour
{
    void Update()
    {
        if (GameManager.Instance == null) return;

        if (Input.GetKeyDown(KeyCode.F5))
            GameManager.Instance.SaveCurrentGame();
        

        if (Input.GetKeyDown(KeyCode.F9))
            GameManager.Instance.LoadLastSave();
        
    }
}
