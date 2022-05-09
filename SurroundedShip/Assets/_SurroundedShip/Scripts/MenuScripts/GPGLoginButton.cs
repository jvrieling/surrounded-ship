using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyMobile;

public class GPGLoginButton : MonoBehaviour
{
    public Text usernameText;

    public GameObject signInPanel;
    public Text statusText;

    // Update is called once per frame
    void Update()
    {
        usernameText.text = (Social.localUser.authenticated) ? Social.localUser.userName : "Not logged in!";
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
            });
        }
    }
}
