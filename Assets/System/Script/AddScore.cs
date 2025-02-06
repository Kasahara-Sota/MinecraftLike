using System;
using UnityEngine;

public class AddScore : MonoBehaviour
{
    [SerializeField] ulong _score;
    public event Action<ulong> _onAddScore;
    private void OnDisable()
    {
        _onAddScore?.Invoke(_score);
    }
}
