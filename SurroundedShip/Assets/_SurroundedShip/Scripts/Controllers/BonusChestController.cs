using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusChestController : MonoBehaviour
{
    public Transform chestModel;
    public ParticleSystem coinParticle;
    public Float floater;
    public Text coinText;
    public Transform popupCanvas;
    public float popupSpeed = 1;

    private bool clicked;

    private void Awake()
    {
        chestModel.Rotate(new Vector3(Random.Range(-18,18), Random.Range(-18, 18), Random.Range(-18, 18)));
    }

    private void OnMouseDown()
    {
        if (clicked) return;

        clicked = true;

        coinParticle.Play();
        floater.floatPower = 0;

        Destroy(gameObject, 5);

        popupCanvas.gameObject.SetActive(true);

        int coinsEarned = 10;
        if (ManagerManager.scoreManager != null)
        {
            coinsEarned = Mathf.FloorToInt(ManagerManager.scoreManager.difficulty * 0.6f);
            coinsEarned = Mathf.Max(coinsEarned, 10);

            ManagerManager.scoreManager.AddGold(coinsEarned);
        }

        coinText.text = "+" + coinsEarned.ToString();
    }

    private void Update()
    {
        if (clicked)
        {
            popupCanvas.Translate(0, popupSpeed * Time.deltaTime, 0);
        }
    }
}
