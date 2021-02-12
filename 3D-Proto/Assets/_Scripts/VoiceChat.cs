using UnityEngine;
using Mirror;
using System;
using System.Collections;

public class VoiceChat : NetworkBehaviour
{
    public AudioSource audioSource;

    [Range(0.1f, 1.99f)]
    public float extractSampleTime;
    public int micFrequency = 44100;

    public override void OnStartLocalPlayer()
    {
        //audioSource.enabled = false;
        audioSource.clip = Microphone.Start(null, true, Mathf.FloorToInt(extractSampleTime) + 1, micFrequency);
        audioSource.loop = true;
        StartCoroutine(RealtimeVoiceChat());
    }

    private void Update()
    {
        if (!isLocalPlayer) return;
        //MicroTest();
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (AudioListener.volume == 0f)
            {
                AudioListener.volume = 1;
            }
            else
               AudioListener.volume = 0;
        }
    }

    private IEnumerator RealtimeVoiceChat()
    {
        while (true)
        {
            int startPos = Microphone.GetPosition(null);
            //Debug.LogWarning("Mic startPos = " + startPos);

            yield return new WaitForSecondsRealtime(extractSampleTime);

            int endPos = Microphone.GetPosition(null);
            //Debug.LogWarning("Mic endPos = " + endPos);

            float[] samples = new float[audioSource.clip.samples * audioSource.clip.channels];
            audioSource.clip.GetData(samples, 0);
            float[] extractSamples = GetExtractArrayFromSamples(samples, startPos, endPos);

            //Debug.LogWarning("----- extract Byte size = " + Buffer.ByteLength(extractSamples));
            CmdPlayExtract(extractSamples, audioSource.clip.channels, audioSource.clip.frequency, false); // true ?
        }
    }

    private float[] GetExtractArrayFromSamples(float[] samples, int startPos, int endPos)
    {
        if (startPos < endPos)
        {
            float[] res = new float[endPos - startPos];
            Array.Copy(samples, startPos, res, 0, endPos - startPos);
            return res;
        }
        else
        {
            int size = (samples.Length - startPos) + endPos;
            float[] res = new float[size];
            for (int i = 0; i < size; i++)
            {
                res[i] = samples[(startPos + i) % samples.Length];
            }
            return res;
        }
    }

    [Command]
    private void CmdPlayExtract(float[] samples, int channels, int frequency, bool stream)
    {
        RpcPlayExtract(samples, channels, frequency, stream);
    }

    [ClientRpc]
    private void RpcPlayExtract(float[] samples, int channels, int frequency, bool stream)
    {
        AudioClip clip = AudioClip.Create("test", samples.Length, channels, frequency, stream);
        clip.SetData(samples, 0);
        if (!isLocalPlayer)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    //private bool recording = false;
    //private int startPos = 0;
    //private int endPos;

    //private void MicroTest()
    //{
    //    if (Input.GetKeyDown(KeyCode.Mouse1))
    //    {
    //        if (!recording)
    //        {
    //            recording = true;
    //            startPos = Microphone.GetPosition(null);
    //        }
    //        else if (recording)
    //        {
    //            endPos = Microphone.GetPosition(null);
    //            Debug.LogWarning("Mic startPos = " + startPos);
    //            Debug.LogWarning("Mic endPos = " + endPos);
    //            recording = false;

    //            float[] samples = new float[audioSource.clip.samples * audioSource.clip.channels];
    //            audioSource.clip.GetData(samples, 0);
    //            float[] extractSamples = GetExtractArrayFromSamples(samples, startPos, endPos);

    //            Debug.LogWarning("----- extract Byte size = " + Buffer.ByteLength(extractSamples));
    //            CmdPlayExtract(extractSamples, audioSource.clip.channels, audioSource.clip.frequency, false); // true ?
    //        }
    //    }
    //}

}
