using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Nếu cần thao tác với Dropdown

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMP_Dropdown mapDropdown; // Dropdown chọn map
    private bool isMapSelected = false; // Biến kiểm tra đã chọn map chưa
    public GameObject ground;  // Background của Map 1
    public GameObject backgroundTileMap;  // Background của Map 2
    public GameObject thirdBackground;


    void Start()
    {
        mapDropdown.onValueChanged.AddListener(delegate { OnMapSelected(); });
    }

    public void OnMapSelected()
    {
        int selectedIndex = mapDropdown.value;

        if (selectedIndex < 0)
        {
            Debug.LogWarning("⚠️ Chưa chọn map!");
            isMapSelected = false;
            return;
        }

        // Tắt tất cả background trước
        ground.SetActive(false);
        backgroundTileMap.SetActive(false);
        thirdBackground.SetActive(false);

        // Bật đúng map được chọn
        if (selectedIndex == 0) // Map 1
        {
            ground.SetActive(true);
        }
        else if (selectedIndex == 1) // Map 2
        {
            backgroundTileMap.SetActive(true);
        }
        else if (selectedIndex == 2) // Map 3
        {
            thirdBackground.SetActive(true);
        }
        else
        {
            Debug.LogError("Lỗi: Map không hợp lệ!");
            return;
        }

        Debug.Log($"✅ Đã chọn map: {mapDropdown.options[selectedIndex].text}");
        isMapSelected = true;
    }

    public void StartGame()
    {

        Debug.Log("Trạng thái chọn map: " + isMapSelected);

        if (!isMapSelected)
        {
            Debug.LogWarning("⚠️ Hãy chọn map trước khi chơi!");
            return;
        }

        if (gameManager == null)
        {
            Debug.LogError("❌ GameManager chưa được gán!");
            return;
        }

        gameManager.StartGame();
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        gameManager.ResumeGame();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
