using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffStateController : StateController
{
    public PowerupType currentPowerupType = PowerupType.Default;
    public BuffState nextState = BuffState.Default;
    public SpriteRenderer spriteRenderer;
    public Color32[] colors;
    public Coroutine cycleCoroutine;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        GameRestart();
        spriteRenderer = transform.gameObject.GetComponent<SpriteRenderer>();
        colors = new Color32[4]
        {
            new Color32(255, 165, 0, 255), //orange
            new Color32(255, 255, 0, 255), //yellow
            new Color32(0, 255, 0, 255), //green
            new Color32(0, 0, 255, 255), //blue
        };
    }
    
    public void SetPowerup(PowerupType i)
    {
        currentPowerupType = i;
    }

    public void Rainbow()
    {
        cycleCoroutine = StartCoroutine(Cycle());
    }

    public void StopRainbow()
    {
        StopCoroutine(cycleCoroutine);
        spriteRenderer.color = new Color32(255,255,255,255);
    }

    public void GameRestart()
    {
        currentPowerupType = PowerupType.Default;   // Clear Powerup
        TransitionToState(startState);
    }


    public IEnumerator Cycle()
    {
        int i = 0;
        while(true)
        {
            for(float interpolant = 0f; interpolant < 1f; interpolant+= 0.9f)
            {
                spriteRenderer.color = Color.Lerp(colors[i%4], colors[(i+1)%4], interpolant);
                yield return null;
            }
            i++;
        }
    }
}
