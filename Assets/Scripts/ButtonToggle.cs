using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonToggle : MonoBehaviour
{
    Button myButton;
    [SerializeField] Animator myAnimator;
    bool clicked;
    bool deselected;
    [SerializeField] GameObject objectToToggle;
    private void Awake()
    {
        if (!myAnimator)
        {
            myAnimator = GetComponent<Animator>();
        }
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(OnClick);
    }
    public void OnClick()
    {
        if (!clicked)
        {
            if (deselected)
            {
                deselected = false;
            }
            else
            {
                myButton.interactable = false;
                clicked = true;
                EventSystem.current.SetSelectedGameObject(objectToToggle);
                myAnimator.SetBool("ButtonAppear", true);
            }
        }
    }
    public void OnButtonDeselect()
    {
        if (clicked)
        {
            myAnimator.SetBool("ButtonAppear", false);
            clicked = false;
            myButton.interactable = true;
            deselected = true;
        }
    }

}
