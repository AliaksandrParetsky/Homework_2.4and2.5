using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class CinemasineCam : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook cum;
    [SerializeField] private CinemachineSmoothPath smoothPath;
    [SerializeField] private Transform cameraFocusPlayer;
    [SerializeField] private Transform camDeathFocusPlayer;
    [SerializeField] private Transform camDeathLookAt;
    [SerializeField] private PlayableDirector playableDirector;

    public static event Action<bool> DisableMovementEvent;

    private bool first = true;

    private void Start()
    {
        StartCoroutine(SetCum(true));

        playableDirector.Stop();

        DisableMovementEvent?.Invoke(true);
    }

    private IEnumerator SetCum(bool start)
    {
        if (start)
        {
            yield return new WaitForSeconds(8.0f);

            cum.Follow = cameraFocusPlayer;

            DisableMovementEvent?.Invoke(false);
        }
        else
        {
            yield return new WaitForSeconds(1.0f);

            cum.Follow = camDeathFocusPlayer;
            cum.LookAt = camDeathLookAt;

            playableDirector.Play();

            first = false;
        }
        
    }

    public void SetCamDeath()
    {
        if (first)
        {
            StartCoroutine(SetCum(false));
        }
        
    }
}
