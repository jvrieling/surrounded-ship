using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyMobile;

public class GPGLoginButton : MonoBehaviour
{
    public Text usernameText;

    public GameObject signInPanel, conflictPanel;
    public GameObject signInPrompt;
    public Text statusText, localSaveSummary, remoteSaveSummary;

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
            Debug.Log("Signing in as per user request");
            InfoPane.log += "\n user manual sign in";

            StartCoroutine(OptionsHolder.Reload(conflictPanel, statusText, localSaveSummary, remoteSaveSummary, () =>
            {
                signInPanel.SetActive(false);
            }));
        } else
        {
            signInPanel.SetActive(false);
            Debug.Log("User is already signed in!");
        }
    }

    public void OverwriteCloud()
    {
        Debug.Log("User chose to overwrite cloud!");
        OptionsHolder.resolution = OptionsHolder.ConflicResolution.useLocal;
    }
    public void OverwriteLocal()
    {
        Debug.Log("User chose to overwrite local!");
        OptionsHolder.resolution = OptionsHolder.ConflicResolution.useRemote;
    }
}
