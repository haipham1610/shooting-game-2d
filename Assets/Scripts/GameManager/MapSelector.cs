using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class MapSelector : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown mapDropdown; // Dropdown chọn map
    [SerializeField] private Tilemap backgroundTilemap; // Tilemap hiển thị background
    [SerializeField] private TileBase[] backgrounds; // Danh sách các Tile background

    void Start()
    {
        mapDropdown.onValueChanged.AddListener(delegate { ChangeBackground(); });
        ChangeBackground(); // Gọi lần đầu để set background mặc định
    }

    public void ChangeBackground()
    {
        int selectedIndex = mapDropdown.value;

        Debug.Log($"🔍 Tổng số backgrounds: {backgrounds.Length}, SelectedIndex: {selectedIndex}");

        if (backgrounds == null || backgrounds.Length == 0)
        {
            Debug.LogError("❌ Danh sách backgrounds chưa được gán hoặc rỗng!");
            return;
        }

        if (selectedIndex >= 0 && selectedIndex < backgrounds.Length)
        {
            if (backgrounds[selectedIndex] == null)
            {
                Debug.LogError($"❌ Background tại index {selectedIndex} bị null!");
                return;
            }

            backgroundTilemap.ClearAllTiles();
            FillBackground(backgroundTilemap, backgrounds[selectedIndex]);
        }
        else
        {
            Debug.LogWarning("⚠️ Không tìm thấy background tương ứng!");
        }
    }


    private void FillBackground(Tilemap tilemap, TileBase tile)
    {
        // Lặp qua toàn bộ Tilemap để đặt Tile mới
        for (int x = -10; x < 10; x++)
        {
            for (int y = -5; y < 5; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
    }
}
