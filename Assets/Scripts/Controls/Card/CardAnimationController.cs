using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();        
    }




}
