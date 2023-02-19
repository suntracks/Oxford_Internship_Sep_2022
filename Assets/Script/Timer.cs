using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update

   // int countDown = 20;
    public int _currentSeconds=0;
//float startingTime = 20f;
public bool stop = true;
Game Game;
    [SerializeField] GameObject Multiplayer;

[SerializeField] Text timerText;
    /*void Start()
    {
        currentTime = startingTime;
    }*/

    // Update is called once per frame
           public void StartTimer(int seconds)
        {
            _currentSeconds = seconds;
            StartCoroutine(nameof(TimerCoroutine));
        }

        private IEnumerator TimerCoroutine()
        {
            UpdateTimerText();
            
            while (_currentSeconds > 0)
            {
                yield return new WaitForSeconds(1);
                _currentSeconds--;
                UpdateTimerText();
            }
            

            _currentSeconds = 0;
            UpdateTimerText();
            OnTimerOver();
        }

        private void UpdateTimerText()
        {
            
                timerText.text = _currentSeconds.ToString();
               
        }

         public void StopTimer()
        {
            StopCoroutine(nameof(TimerCoroutine));
            SetTimerNull();
            // _currentSeconds;
        }

         private void SetTimerNull()
        {
            
                timerText.text = "";
               
        }

        public void OnTimerOver(){
            Game = Multiplayer.GetComponent<Game>();
            Game.OnTimeOverSet();
        }

}
