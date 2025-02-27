using UnityEngine;

[CreateAssetMenu(fileName = "NewGun", menuName = "Gun Data")]
public class GunData : ScriptableObject
{
	public string gunName;
	public float shotDelay;       // Tốc độ bắn (delay giữa các phát bắn)
	public int damage;           // Sát thương
	public GameObject bulletPrefab; // Viên đạn tương ứng
	public int bulletsPerShot = 1;  // Số viên đạn bắn ra mỗi lần (Shotgun bắn nhiều viên)
	public float spreadAngle = 0f;  // Độ lan của đạn khi bắn nhiều viên
	public int maxAmmo;
}
