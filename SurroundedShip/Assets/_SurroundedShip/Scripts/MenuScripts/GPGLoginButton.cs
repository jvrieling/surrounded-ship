using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyMobile;

public class GPGLoginButton : MonoBehaviour
{
    public Text usernameText;

    public GameObject signInPanel;
    public GameObject signInPrompt;
    public Text statusText;

    public Image iconImage;
    public Color notSignedInColour = Color.red;

    // Update is called once per frame
    void Update()
    {
        if (Social.localUser.authenticated)
        {
            usernameText.text = Social.localUser.userName;
            iconImage.color = Color.white;
            signInPrompt.SetActive(false);
        }
        else
        {
            usernameText.text = "Not logged in!";
            iconImage.color = notSignedInColour;
            signInPrompt.SetActive(true);
        }
    }

    public void OnClick()
    {
        signInPanel.SetActive(true);
        Debug.Log("Clicked GPG button");
        if (!(GameServices.IsInitialized() && Social.localUser.authenticated))
        {
            GameServices.Init();
            Debug.Log("Signing in as per user request");
            InfoPane.log += "\n user manual sign in";

            OptionsHolder.Reload(statusText, () =>
            {
                signInPanel.SetActive(false);
                OptionsHolder.GPGEnabled = true;
            });
        } else
        {
            signInPanel.SetActive(false);
            Debug.Log("User is already signed in!");
        }
    }
}
