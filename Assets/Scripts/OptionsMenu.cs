using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider mainSlider;

    // Start is called before the first frame update
    void Start()
    {
        //Adds a listener to the main slider and invokes a method when the value changes.
        mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    {
        print(mainSlider.value);
        AudioListener.volume = mainSlider.value;
    }

}
