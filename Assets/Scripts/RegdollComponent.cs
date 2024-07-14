using System;
using System.Collections.Generic;
using UnityEngine;

public class RegdollComponent : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] Animator m_Animator;
    [SerializeField] List<Rigidbody> elements;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] CinemasineCam cum;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Damage>(out var damage))
        {
            m_Animator.enabled = false;
            characterController.enabled = false;
            playerMovement.enabled = false;


            foreach (var element in elements)
            {
                element.isKinematic = false;
            }

            cum.SetCamDeath();
        }
    }
}
