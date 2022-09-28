using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowFireRate : MonoBehaviour
{
    public Text txt;
    public Button btn;

    Text btnTxt;

    int max = 13;
    string originalText;

    private void Start()
    {
        btnTxt = btn.GetComponentInChildren<Text>();
        originalText = btnTxt.text;
    }

    // Update is called once per frame
    void Update()
    {
        btn.interactable = true;
        btnTxt.text = originalText;
        float acc, accdiff;
        switch (UpgradeManager.selectedGun)
        {
            case 1:
                acc = OptionsHolder.instance.save.gun1.timeBetweenShots -0.1f;
                accdiff = Mathf.RoundToInt((0.65f - acc) / 0.05f);
                txt.text = accdiff + "/" + max;

                if (accdiff == 13)
                {
                    btn.interactable = false;
                    btn.GetComponentInChildren<Text>().text = "MAXED";
                }
                break;
            case 2:
                acc = OptionsHolder.instance.save.gun2.timeBetweenShots - 0.1f;
                accdiff = Mathf.RoundToInt((0.65f - acc) / 0.05f);
                txt.text = accdiff + "/" + max;
                if (accdiff == 13)
                {
                    btn.interactable = false;
                    btn.GetComponentInChildren<Text>().text = "MAXED";
                }
                break;
            case 3:
                acc = OptionsHolder.instance.save.gun3.timeBetweenShots - 0.1f;
                accdiff = Mathf.RoundToInt((0.65f - acc) / 0.05f);
                txt.text = accdiff + "/" + max;
                if (accdiff == 13)
                {
                    btn.interactable = false;
                    btn.GetComponentInChildren<Text>().text = "MAXED";
                }
                break;
            case 4:
                acc = OptionsHolder.instance.save.gun4.timeBetweenShots - 0.1f;
                accdiff = Mathf.RoundToInt((0.65f - acc) / 0.05f);
                txt.text = accdiff + "/" + max;
                if (accdiff == 13)
                {
                    btn.interactable = false;
                    btn.GetComponentInChildren<Text>().text = "MAXED";
                }
                break;

        }
    }
}
