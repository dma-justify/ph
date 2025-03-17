using System.Collections;
using UnityEngine;

namespace Utils.Timer
{
    public class TimeCounter
    {
        public int TimeElapsed { get; private set; }
        
        private readonly ITimerView _timerView;
        private readonly YieldInstruction _delay;
        private readonly ICoroutineRunner _coroutineRunner;

        private Coroutine _routine;
        
        public TimeCounter(ITimerView timerView, ICoroutineRunner coroutineRunner)
        {
            _timerView = timerView;
            _delay = new WaitForSeconds(1);
            _coroutineRunner = coroutineRunner;
        }


        public void Start()
        {
            if (_routine != null)
                return;

            TimeElapsed = 0;
            _routine = _coroutineRunner.StartCoroutine(Counter());
        }
        
        
        public int Stop()
        {
            if (_routine != null)
                _coroutineRunner.StopCoroutine(_routine);

            _routine = null;

            return TimeElapsed;
        }

        private IEnumerator Counter()
        {
            while (true)
            {
                yield return _delay;
                _timerView.SetTime(++TimeElapsed);
            }
        }
    }
}