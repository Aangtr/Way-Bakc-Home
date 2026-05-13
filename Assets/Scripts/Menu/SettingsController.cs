using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SettingsController : MonoBehaviour
{
    [Header("--- Âm Thanh ---")]
    public Slider volumeSlider;

    [Header("--- Ngôn Ngữ ---")]
    public TMP_Dropdown languageDropdown;

    [Header("--- Độ Phân Giải (Để trống nếu chưa dùng) ---")]
    public TMP_Dropdown resolutionDropdown; // Cái này tùy chọn

    Resolution[] resolutions;

    void Start()
    {
        // 1. SETUP VOLUME
        volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // 2. SETUP LANGUAGE
        // Đọc ngôn ngữ đã lưu (0: Tiếng Việt, 1: English...), mặc định là 0
        int currentLangID = PlayerPrefs.GetInt("LanguageID", 0);
        languageDropdown.value = currentLangID;
        languageDropdown.onValueChanged.AddListener(SetLanguage);

        // 3. SETUP RESOLUTION (Chỉ chạy nếu bạn có gán Dropdown này)
        if (resolutionDropdown != null)
        {
            SetupResolution();
        }
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetLanguage(int langIndex)
    {
        // langIndex sẽ là thứ tự bạn sắp xếp trong Unity Editor
        // Ví dụ: 0 là Tiếng Việt, 1 là English, 2 là Japanese
        PlayerPrefs.SetInt("LanguageID", langIndex);
        PlayerPrefs.Save();

        Debug.Log("Đã chọn ngôn ngữ số: " + langIndex);

        // LƯU Ý: Ở đây bạn sẽ cần gọi thêm hàm để đổi text trong game
        // Ví dụ: LocalizationManager.Instance.ChangeLanguage(langIndex);
    }

    // Hàm phụ để xử lý độ phân giải (Tách ra cho gọn)
    void SetupResolution()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.onValueChanged.AddListener((index) =>
        {
            Resolution res = resolutions[index];
            Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        });
    }
}