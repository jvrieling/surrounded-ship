using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTutorial : MonoBehaviour
{
    public GameObject[] enableIfNoTutorial;
    // Start is called before the first frame update
    void Start()
    {
        if(OptionsHolder.instance.save.showTutorial == false)
        {
            gameObject.SetActive(false);

            foreach(GameObject i in enableIfNoTutorial)
            {
                i.SetActive(true);
            }
        }
    }

}
