﻿using UnityEngine;

public class WarzonePlayerAnimation : MonoBehaviour
{
    [SerializeField] private WarzonePlayer player;
    private Animator animator;

    //CONST
    public const string IS_WALKING = "IsWalking";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}

