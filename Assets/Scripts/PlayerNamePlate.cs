using UnityEngine;
using UnityEngine.UI;

public class PlayerNamePlate : MonoBehaviour {

    [SerializeField]
    private Text usernameText;

    [SerializeField]
    private Player player;

    [SerializeField]
    private RectTransform healthBarFill;

	void Update()
    {
        usernameText.text = player.username;
        healthBarFill.localScale = new Vector3(player.GetHealthPct(), 1f, 1f);

    }


}
