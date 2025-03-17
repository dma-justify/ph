using System;
using System.Collections;
using UnityEngine;

namespace Utils.Timer
{
    public class Timer
    {
        public event Action Ended;
        
        public int RemainingTime { get; private set; }
        public int TotalTime { get; private set; }
        
        private readonly ITimerView _timerView;
        private readonly YieldInstruction _delay;
        private readonly ICoroutineRunner _coroutineRunner;
        
        private Coroutine _coroutine;
        
        public Timer(ICoroutineRunner coroutineRunner, ITimerView timerView)
        {
            _timerView = timerView;
            _delay = new WaitForSeconds(1f);
            _coroutineRunner = coroutineRunner;
        }

        public void Start(int seconds)
        {
            Stop();
            
            RemainingTime = TotalTime = seconds;
            
            _timerView.SetTime(seconds);
            _coroutine = _coroutineRunner.StartCoroutine(TimerRoutine());
        }
        
        public void Stop()
        {
            if (_coroutine != null)
                _coroutineRunner.StopCoroutine(_coroutine);
        }

        private IEnumerator TimerRoutine()
        {
            while (RemainingTime > 0)
            {
                yield return _delay;
                _timerView.SetTime(--RemainingTime);
            }
            
            Ended?.Invoke();
        }
    }
}