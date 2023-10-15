using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CoinPowerupWeek5 : BasePowerupWeek5
{

    private AudioSource coinAudio;
    private Animator coinAnimator;

    public UnityEvent incrementScore;

    // Start is called before the first frame update
    protected override void Start()
    {
        coinAudio = this.GetComponent<AudioSource>();
        coinAnimator = this.GetComponent<Animator>();
        this.type = PowerupType.Coin;
    }

    public override void SpawnPowerup()
    {
        base.SpawnPowerup();
        PlaySpawnAnimation();
        IncreaseScore();
    }

    public override void ApplyPowerup(MonoBehaviour i)
    {
        // TODO: do something with the object
    }

    public override void GameRestart()
    {
        base.GameRestart();
        ResetAnimations();
    }



    /// Helper Functions
    void PlaySpawnAnimation()
    {
        coinAnimator.SetBool("collected", true);
    }

    void ResetAnimations()
    {
        coinAnimator.SetTrigger("gameRestart");
        coinAnimator.SetBool("collected", false);
    }

    public void PlayCoinGetSound()
    {
        coinAudio.PlayOneShot(coinAudio.clip);
    }

    void IncreaseScore()
    {
        incrementScore.Invoke();
    }
}
