using BepInEx;
using GorillaGameModes;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

[BepInPlugin("kyx.lms.music", "LMS Music", "1.0.0")]
public class LastManStandingMusic : BaseUnityPlugin
{
    private AudioSource source;
    private AudioClip clip;
    private bool isPlaying;
    private Coroutine fadeRoutine;

    private const string SongUrl =
        "https://raw.githubusercontent.com/kyxfr/secdn/main/RandomLMSIfound.mp3";

    private void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.loop = true;
        source.volume = 0f;
        StartCoroutine(LoadAudio());
    }

    private IEnumerator LoadAudio()
    {
        using (UnityWebRequest req =
               UnityWebRequestMultimedia.GetAudioClip(SongUrl, AudioType.MPEG))
        {
            yield return req.SendWebRequest();
            clip = DownloadHandlerAudioClip.GetContent(req);
            source.clip = clip;
        }
    }

    private void Update()
    {
        if (!PhotonNetwork.InRoom || GorillaGameManager.instance == null || clip == null)
            return;

        bool localInfected = IsInfected();
        bool lastSurvivor = IsLastSurviver();

        if (lastSurvivor && !localInfected && !isPlaying)
            FadeIn();

        if (localInfected && isPlaying)
            FadeOut();
    }

    private bool IsLastSurviver()
    {
        List<NetPlayer> infected = GetInfectedPlayers();
        int totalPlayers = PhotonNetwork.PlayerList.Length;

        return infected.Count == totalPlayers - 1 &&
               !infected.Contains(NetworkSystem.Instance.LocalPlayer);
    }

    private bool IsInfected()
    {
        return GetInfectedPlayers()
            .Contains(NetworkSystem.Instance.LocalPlayer);
    }

    private List<NetPlayer> GetInfectedPlayers()
    {
        List<NetPlayer> infected = new List<NetPlayer>();

        if (!(GorillaGameManager.instance is GorillaTagManager tag))
            return infected;

        if (tag.isCurrentlyTag)
            infected.Add(tag.currentIt);
        else
            infected.AddRange(tag.currentInfected);

        return infected;
    }

    private void FadeIn()
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        source.Play();
        fadeRoutine = StartCoroutine(Fade(1f));
        isPlaying = true;
    }

    private void FadeOut()
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(Fade(0f));
        isPlaying = false;
    }

    private IEnumerator Fade(float target)
    {
        float start = source.volume;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / 3f;
            source.volume = Mathf.Lerp(start, target, t);
            yield return null;
        }

        source.volume = target;

        if (target == 0f)
            source.Stop();
    }
}
