using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinAnimationEffects : MonoBehaviour
{
    public AudioSource coinAudio;
    public int parameter;
    public UnityEvent<int> useInt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayCoinGetSound()
    {
        coinAudio.PlayOneShot(coinAudio.clip);
    }

    public void TriggerIntEvent()
    {
        useInt.Invoke(parameter);
    }
}
