using TMPro;
using UnityEngine;

public class GunController : MonoBehaviour
{
	public GunData gunData;
	private float rotateOffset = 180f;
	[SerializeField] private Transform firePosition;
	[SerializeField] private GameObject bulletPrefab;
	[SerializeField] private GameObject muzzle;
	private float nextShot;
	public int currentAmmo;
	private GameObject currentGun;
	[SerializeField] private TextMeshProUGUI ammoText;

	void Start()
	{
		currentAmmo = gunData.maxAmmo;
	}

	void Update()
	{
		RotateGun();
		Shoot();
		Reload();
	}

	// Rotate 
	void RotateGun()
	{
		if (Input.mousePosition.x < 0
			|| Input.mousePosition.x > Screen.width
			|| Input.mousePosition.y < 0
			|| Input.mousePosition.y > Screen.height)
		{
			return;
		}

		Vector3 displacement = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, 0, angle + rotateOffset);

		if (angle < -90 || angle > 90)
		{
			transform.localScale = new Vector3(1, 1, 1);
		}
		else
		{
			transform.localScale = new Vector3(1, -1, 1);
		}
	}

	//Shoot
	void Shoot()
	{
		if (Input.GetMouseButtonDown(0) && currentAmmo > 0 && Time.time > nextShot)
		{
			//Rifle & Pistol
			if (gunData.bulletsPerShot == 1)
			{
				nextShot = Time.time + gunData.shotDelay;
				Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
				Instantiate(muzzle, firePosition.position, transform.rotation, transform);
				currentAmmo--;
			}
			//Shotgun
			else
			{
				for (int i = 0; i < gunData.bulletsPerShot; i++)
				{
					float angle = Random.Range(-gunData.spreadAngle, gunData.spreadAngle);
					Quaternion bulletRotation = Quaternion.Euler(0, 0, firePosition.eulerAngles.z + angle);
					Instantiate(gunData.bulletPrefab, firePosition.position, bulletRotation);
					Instantiate(muzzle, firePosition.position, transform.rotation, transform);
					
				}
				currentAmmo--;
			}
			UpdateAmmoText();
		}
	}

	//Reload
	void Reload()
	{
		if (Input.GetMouseButtonDown(1) && currentAmmo < gunData.maxAmmo)
		{
			currentAmmo = gunData.maxAmmo;
			UpdateAmmoText();
		}
	}

	private void UpdateAmmoText()
	{
		if (ammoText != null)
		{
			if (currentAmmo > 0)
			{
				ammoText.text = currentAmmo.ToString();
			}
			else
			{
				ammoText.text = "0";
			}
		}
	}
}
