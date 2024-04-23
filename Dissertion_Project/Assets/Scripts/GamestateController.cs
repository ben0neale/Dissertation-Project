using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamestateController : MonoBehaviour
{
    public enum GameState
    {
        Menu,
        Play,
        GameOver
    }

    public enum SpawnState
    {
        Obstical,
        Boalder,
        Rest
    }

    public GameState state;
    public SpawnState spawnState;

    // Start is called before the first frame update
    void Start()
    {
        state = GameState.Play;
        spawnState = SpawnState.Obstical;
    }

    public void SetGameState(GameState _state)
    {
        state = _state;
    }

    public void SetSpawnState(SpawnState _spawnState)
    {
        spawnState = _spawnState;
    }

    public GameState GetGameState()
    {
        return state;
    }

    public SpawnState GetSpawnState()
    {
        return spawnState;
    }
}
