using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player References")]
    public Transform player;
    public PlayerHealth playerHealth;
    public PlayerInventory playerInventory;

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    /* ================= SAVE ================= */

    public void SaveCurrentGame()
    {
        if (player == null || playerHealth == null || playerInventory == null)
        {
            Debug.LogWarning("[SAVE] Missing player reference!");
            return;
        }

        SaveData data = new SaveData
        {
            sceneName = SceneManager.GetActiveScene().name,

            playerPosX = player.position.x,
            playerPosY = player.position.y,
            playerPosZ = player.position.z,

            playerHP = playerHealth.currentHealth,
            keyCount = playerInventory.keyCount
        };

        SaveSystem.SaveGame(data);
        Debug.Log("[SAVE] Game saved");
    }

    /* ================= LOAD ================= */

    public void LoadLastSave()
    {
        SaveData data = SaveSystem.LoadGame();
        if (data == null)
        {
            Debug.LogWarning("[LOAD] No save data found");
            return;
        }

        StartCoroutine(LoadSceneAndApply(data));
    }

    private IEnumerator LoadSceneAndApply(SaveData data)
    {
        // Load scene async
        AsyncOperation op = SceneManager.LoadSceneAsync(data.sceneName);
        while (!op.isDone)
            yield return null;

        // Chờ 1 frame để scene setup xong
        yield return null;

        // 🔍 Tìm lại Player mới trong scene
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null)
        {
            Debug.LogError("[LOAD] Player not found!");
            yield break;
        }

        player = playerObj.transform;
        playerHealth = playerObj.GetComponent<PlayerHealth>();
        playerInventory = playerObj.GetComponent<PlayerInventory>();

        // Apply position
        player.position = new Vector3(
            data.playerPosX,
            data.playerPosY,
            data.playerPosZ
        );

        // Apply health
        playerHealth.currentHealth = data.playerHP;
        playerHealth.RefreshUI();

        // Apply inventory
        playerInventory.keyCount = data.keyCount;

        Debug.Log("[LOAD] Game loaded successfully");
    }
}
