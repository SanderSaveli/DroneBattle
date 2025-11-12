using System;
using UnityEngine;

namespace Sander.DroneBattle
{
    public class Fabrik : MonoBehaviour
    {
        public int Score { get; private set; }
        public Action<int> OnScoreChange { get; set; }

        public void AddResource()
        {
            Score++;
            OnScoreChange?.Invoke(Score);
        }
    }
}
