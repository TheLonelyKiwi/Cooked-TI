using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    private ParticleSystem particleSys;

    public float FillSpeed = 0.5f;
    private float targetProgress = 0;

    private void Awake(){
        slider = gameObject.GetComponent<Slider>();
        particleSys = GameObject.Find("Progress Bar Particles").GetComponent<ParticleSystem>();
    }
    
    void Start()
    {
        IncrementProgress(1f);
    }

    void Update()
    {
        if (slider.value < targetProgress){
            slider.value += FillSpeed * Time.deltaTime;
            if (!particleSys.isPlaying)
                particleSys.Play();
        }
        else{
            particleSys.Stop();
        }
    }

    public void IncrementProgress(float newProgress){
        targetProgress = slider.value + newProgress;
    }
}
