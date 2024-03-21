using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scaffold.Screens.Core
{
    public class ScreenTransitionController : MonoBehaviour
    {
        private Queue<IEnumerator> pendingSequences = new Queue<IEnumerator>();
        private Coroutine sequenceCO;

        public void QueueSequence(IEnumerator sequence)
        {
            pendingSequences.Enqueue(sequence);
            if (sequenceCO == null)
            {
                sequenceCO = StartCoroutine(Sequence());
            }
        }

        private IEnumerator Sequence()
        {
            while (pendingSequences.TryDequeue(out var next))
            {
                name = $"Screen Queue - Running - {pendingSequences.Count} pending";
                yield return next;
            }
            name = $"Screen Queue - Idle";
            sequenceCO = null;
        }

        public void StartSequenceWithCallback(IEnumerator sequence, Action onComplete)
        {
            StartCoroutine(RunWithCallback(sequence, onComplete));
        }

        private IEnumerator RunWithCallback(IEnumerator sequence, Action onComplete)
        {
            yield return sequence;
            onComplete?.Invoke();
        }

    }
}