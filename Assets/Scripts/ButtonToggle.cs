using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonToggle : MonoBehaviour
{
    Button myButton;
    Animator myAnimator;
    bool clicked;
    [SerializeField] GameObject objectToToggle;
    private void Awake()
    {
        myButton = GetComponent<Button>();
        myAnimator = GetComponent<Animator>();
        myButton.onClick.AddListener(OnClick);
    }
    public void OnClick()
    {
        if (!clicked)
        {
            clicked = true;
            EventSystem.current.SetSelectedGameObject(objectToToggle);
            myAnimator.SetBool("ButtonAppear", true);
        }
    }
    public void OnButtonDeselect()
    {
        if (clicked)
        {
            myAnimator.SetBool("ButtonAppear", false);
            clicked = false;
        }
    }
}
