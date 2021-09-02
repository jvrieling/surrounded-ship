using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile;

public class GetAdConsent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Advertising.GrantDataPrivacyConsent();   
    }
}
