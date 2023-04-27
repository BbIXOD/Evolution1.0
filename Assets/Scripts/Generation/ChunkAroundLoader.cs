using UnityEngine;

public class ChunkAroundLoader : MonoBehaviour
{
    private const int ChunksAround = 1;
    private readonly int[] _position = new int[2];
    private int _dif;
    private readonly float[] _playerPos = new float[2];
    private readonly int[] _curChunk = new int[2];

    [SerializeField] private GameObject foodController;
    [SerializeField] private Material terrainMaterial;

    private void Awake()
    {
        for (var i = -ChunksAround; i <= ChunksAround; i++)
        {
            for (var j = -ChunksAround; j <= ChunksAround; j++)
            {
                VisitChunk(i, j);
            }
        }
    }

    private void FixedUpdate()
    {
        var p = transform.position;
        _playerPos[0] = p.x;
        _playerPos[1] = p.z;

        for (var i = 0; i < _position.Length; i++)
        {
            _curChunk[i] = (int)(_playerPos[i] / ChunkManager.Size.x);
            if (_playerPos[i] < 0)
            {
                _curChunk[i]--;
            }
        }

        _dif = _curChunk[0] - _position[0];
        if (_dif != 0)
        { 
            for (var delta = -ChunksAround; delta <= ChunksAround; delta++)
            {
                VisitChunk(_curChunk[0] + ChunksAround * _dif, _curChunk[1] + delta);
                LeaveChunk(_position[0] - ChunksAround * _dif, _curChunk[1] + delta);
            }
        }
        
        _dif = _curChunk[1] - _position[1];
        if (_dif != 0)
        { 
            for (var delta = -ChunksAround; delta <= ChunksAround; delta++)
            {
                VisitChunk(_curChunk[0] + delta, _curChunk[1] + ChunksAround * _dif);
                LeaveChunk(_curChunk[0] + delta, _position[1] - ChunksAround * _dif);
            }
        }
        
        _curChunk.CopyTo(_position, 0);
    }

    private void OnDestroy()
    {
        for (var i = -ChunksAround; i <= ChunksAround; i++)
        {
            for (var j = -ChunksAround; j <= ChunksAround; j++)
            {
                LeaveChunk(i, j);
            }
        }
    }

    private void VisitChunk(int x, int y)
    {
        if (ChunkManager.Loaded.ContainsKey((x, y)))
        {
            ChunkManager.Loaded[(x, y)].AddPlayer();
            return;
        }
        Chunk.MakeChunk(x, y, foodController, terrainMaterial);
    }

    private void LeaveChunk(int x, int y)
    { 
        ChunkManager.Loaded[(x, y)].SubtractPlayer();
    }
}
