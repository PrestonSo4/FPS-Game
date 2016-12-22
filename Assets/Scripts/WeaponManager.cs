using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class WeaponManager : NetworkBehaviour {
    [Header("Variables")]
	[SerializeField]
	private string weaponLayerName = "Weapon";

	[SerializeField]
	private Transform weaponHolder;

    public bool isReloading = false;
    [Header("Weapons")]
	[SerializeField]
	private PlayerWeapon primaryWeapon;
    [SerializeField]
    private PlayerWeapon secondaryWeapon;

    [SerializeField]
	private PlayerWeapon currentWeapon;
	private WeaponGraphics currentGraphics;


	void Start ()
	{
		EquipWeapon(primaryWeapon);
	}

	public PlayerWeapon GetCurrentWeapon ()
	{
		return currentWeapon;
	}

	public WeaponGraphics GetCurrentGraphics()
	{
		return currentGraphics;
	}

     
	public void EquipWeapon (PlayerWeapon _weapon)
	{
		currentWeapon = _weapon;

		GameObject _weaponIns = (GameObject)Instantiate(_weapon.graphics, weaponHolder.position, weaponHolder.rotation);
		_weaponIns.transform.SetParent(weaponHolder);

		currentGraphics = _weaponIns.GetComponent<WeaponGraphics>();
		if (currentGraphics == null)
			Debug.LogError("No WeaponGraphics component on the weapon object: " + _weaponIns.name);

		if (isLocalPlayer)
			Util.SetLayerRecursively(_weaponIns, LayerMask.NameToLayer(weaponLayerName));

	}


    public void Reload()
    {
        if (isReloading)
            return;
        Debug.Log("Reloading..");

        StartCoroutine(Reload_Coroutine());
    }

    private IEnumerator Reload_Coroutine()
    {
        isReloading = true;

        CmdOnReload();

        yield return new WaitForSeconds(currentWeapon.reloadTime);

        currentWeapon.bullets = currentWeapon.maxBullets;

        isReloading = false;
    }

    [Command]
    void CmdOnReload()
    {
        RpcOnReload();
    }

    [ClientRpc]
    void RpcOnReload()
    {
        Animator anim = currentGraphics.GetComponent<Animator>();
        if(anim != null)
        {
            anim.SetFloat("reloadTime", currentWeapon.reloadTime);
            anim.SetTrigger("Reload");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentWeapon != primaryWeapon)
        {
            Debug.Log(weaponHolder.transform.GetChild(0).name);
            Destroy(weaponHolder.transform.GetChild(0).gameObject);
            EquipWeapon(primaryWeapon);
            Debug.Log("equiped primary weapon");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && currentWeapon != secondaryWeapon)
        {
            Destroy(weaponHolder.transform.GetChild(0).gameObject);
            EquipWeapon(secondaryWeapon);
            Debug.Log("equiped secondary weapon");
        }


    }

}
