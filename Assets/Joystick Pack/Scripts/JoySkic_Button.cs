using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoySkic_Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    [Header("ÿһ��Ԥ��ֵ")]
    public bool isAttack_Sword;
    public bool isDash;
    private void Start()
    {
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (isAttack_Sword)
        {
            PlayerController.instance.anim.SetTrigger("Attack");
        }
        else if (isDash)
        {
            PlayerController.instance.anim.SetTrigger("Dash");
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
       
    }
}

