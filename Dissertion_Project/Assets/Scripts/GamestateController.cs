using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        PreObstical,
        Obstical,
        PreBoulder,
        Boalder,
        Rest
    }

    public GameState state;
    public SpawnState spawnState;

    bool warning = false;
    [SerializeField] GameObject warningText;

    // Start is called before the first frame update
    void Start()
    {
        state = GameState.Play;
        spawnState = SpawnState.Obstical;
    }

    private void Update()
    {
        if (spawnState == SpawnState.PreBoulder && !warning)
        {
            warning = true;
            StartCoroutine(BoulderWarning());
        }
        else if(spawnState != SpawnState.PreBoulder)
            warning = false;
    }

    IEnumerator BoulderWarning()
    {
        yield return new WaitForSeconds(3f);
        warningText.SetActive(true);
        yield return new WaitForSeconds(1f);
        warningText.SetActive(false);
        yield return new WaitForSeconds(1f);
        warningText.SetActive(true);
        yield return new WaitForSeconds(1f);
        warningText.SetActive(false);
        yield return new WaitForSeconds(1f);
        warningText.SetActive(true);
        yield return new WaitForSeconds(1f);
        warningText.SetActive(false);
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
