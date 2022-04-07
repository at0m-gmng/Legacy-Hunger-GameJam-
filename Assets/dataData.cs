using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "dataData", menuName = "Gameplay/New dataData")]
public class dataData : ScriptableObject
{
    [SerializeField] private int _maxLevel; // ���� �������
    [SerializeField] private float _timeLeftSpeed; // �������� ������������ � Time.deltaTime ��� �������� ���������� timeLeft
    [SerializeField] private float _addedTime; // ����������� �������� �������
    [SerializeField] private float _basicAddedTime; // ��������� �������� ������������ �������
    [SerializeField] private List<Vector3> _fruitPositions = new List<Vector3>(); // ����� ��������� �������
    public int maxLevel => this._maxLevel;
    public float timeLeftSpeed => this._timeLeftSpeed;
    public float addedTime => this._addedTime;
    public float basicAddedTime => this._basicAddedTime;
    public List<Vector3> fruitPositions => this._fruitPositions;
}
