using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class LogoFlashes : MonoBehaviour
{
    public UnityEvent onLoadEvent;
    // Start is called before the first frame update
    void Start()
    {
        onLoadEvent.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
