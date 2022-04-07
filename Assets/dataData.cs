using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "dataData", menuName = "Gameplay/New dataData")]
public class dataData : ScriptableObject
{
    [SerializeField] private int _maxLevel; // макс уровень
    [SerializeField] private float _timeLeftSpeed; // значение прибавляемое к Time.deltaTime для быстрого уменьшения timeLeft
    [SerializeField] private float _addedTime; // добавляемое значение времени
    [SerializeField] private float _basicAddedTime; // стартовое значение добавляемого времени
    [SerializeField] private List<Vector3> _fruitPositions = new List<Vector3>(); // набор стартовых позиций
    public int maxLevel => this._maxLevel;
    public float timeLeftSpeed => this._timeLeftSpeed;
    public float addedTime => this._addedTime;
    public float basicAddedTime => this._basicAddedTime;
    public List<Vector3> fruitPositions => this._fruitPositions;
}
