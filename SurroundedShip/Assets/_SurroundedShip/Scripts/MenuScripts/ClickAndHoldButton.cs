using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickAndHoldButton : MonoBehaviour
{
    public float holdTime = 0.75f;
    private bool pressed;
    private float timer;
    private int frameDelay;

    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
    }
    public void Down()
    {
        pressed = true;
    }
    public void Up()
    {
        pressed = false;
        timer = 0;
        frameDelay = 0;
    }

    void Update()
    {
        if (pressed) {
            timer += Time.deltaTime;

            if(timer >= holdTime)
            {
                if (frameDelay >= 3)
                {
                    btn.onClick.Invoke();
                    frameDelay = 0;
                } else
                {
                    frameDelay++;
                }
            }
        }
    }
}
