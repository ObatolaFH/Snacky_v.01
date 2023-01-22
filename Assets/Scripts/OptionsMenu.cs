using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider mainSlider;
    public Dropdown templates;

    // Start is called before the first frame update
    void Start()
    {
        //Adds a listener to the main slider and invokes a method when the value changes.
        mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        templates.onValueChanged.AddListener(delegate { TemplateChangeCheck(); });

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    {
        AudioListener.volume = mainSlider.value;
    }

    public void TemplateChangeCheck()
    {
        print(templates.value);
        MainMenu.template = templates.value;
    }

}
