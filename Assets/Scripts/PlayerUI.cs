using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

	[SerializeField]
	RectTransform thrusterFuelFill;

    [SerializeField]
    RectTransform healthbarFill;

	[SerializeField]
	GameObject pauseMenu;

    [SerializeField]
    GameObject scoreboard;

    [SerializeField]
    Text ammoText;

    private Player player;

	private PlayerController controller;
    private WeaponManager weaponManager;

    //Options UI elements
    [Header("Options Menu")]
    [SerializeField]
    Slider mouseSense;
    [SerializeField]
    Text amountText;

    public void SetPlayer (Player _player)
	{
		player = _player;
        controller = player.GetComponent<PlayerController>();
        weaponManager = player.GetComponent<WeaponManager>();
	}

	void Start ()
	{
		PauseMenu.IsOn = false;
	}

	void Update ()
	{
		SetFuelAmount (controller.GetThrusterFuelAmount());

        SetHealthAmount(player.GetHealthPct());

        SetAmmAmount(weaponManager.GetCurrentWeapon().bullets);

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			TogglePauseMenu();
		}

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            scoreboard.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            scoreboard.SetActive(false);
        }

        if (mouseSense != null)
        {
            controller.lookSensitivity = mouseSense.value;
            amountText.text = mouseSense.value.ToString();
        }

    }

    private void SetAmmAmount(int _amount)
    {
        ammoText.text = _amount.ToString();
    }

    private void SetHealthAmount(float _amount)
    {
        print(_amount);
        healthbarFill.localScale = new Vector3(1f, _amount, 1f);
    }

    public void TogglePauseMenu ()
	{
		pauseMenu.SetActive(!pauseMenu.activeSelf);
		PauseMenu.IsOn = pauseMenu.activeSelf;
    }

	void SetFuelAmount (float _amount)
	{
        print(_amount);
        thrusterFuelFill.localScale = new Vector3(1f, _amount, 1f);

	}

}
