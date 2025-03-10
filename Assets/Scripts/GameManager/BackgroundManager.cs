using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown mapDropdown; // Dropdown chọn background
    [SerializeField] private Tilemap[] backgroundTilemaps; // Mảng chứa các Tilemap

    void Start()
    {
        // Đặt tất cả background ẩn đi, chỉ hiển thị background mặc định
        ShowBackground(0);

        // Thêm sự kiện vào Dropdown
        mapDropdown.onValueChanged.AddListener(delegate { OnBackgroundSelected(); });
    }

    public void OnBackgroundSelected()
    {
        int selectedIndex = mapDropdown.value;
        ShowBackground(selectedIndex);
    }

    private void ShowBackground(int index)
    {
        for (int i = 0; i < backgroundTilemaps.Length; i++)
        {
            backgroundTilemaps[i].gameObject.SetActive(i == index);
        }
    }
}
