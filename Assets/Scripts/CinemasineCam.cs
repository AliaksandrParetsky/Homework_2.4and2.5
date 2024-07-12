using Cinemachine;
using System;
using System.Collections;
using UnityEngine;

public class CinemasineCam : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook cum;
    [SerializeField] private CinemachineSmoothPath smoothPath;
    [SerializeField] private Transform cameraFocusPlayer;

    private void Start()
    {
        StartCoroutine(SetCum());
    }

    private IEnumerator SetCum()
    {
        yield return new WaitForSeconds(8.0f);

        cum.Follow = cameraFocusPlayer;

        this.enabled = false;
    }
}
