using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyMobile;

public class GPGLoginButton : MonoBehaviour
{
    public Text usernameText;

    // Update is called once per frame
    void Update()
    {
        usernameText.text = (Social.localUser.authenticated) ? Social.localUser.userName : "Not logged in!";
    }

    public void OnClick()
    {
        if (GameServices.IsInitialized() && Social.localUser.authenticated)
        {
            GameServices.SignOut();
            Debug.Log("Signed out!");
            InfoPane.log += "\n user sign out";
        } else
        {
            GameServices.Init();
            Debug.Log("Signing in as per user request");
            InfoPane.log += "\n user sign in";
        }
    }
}
