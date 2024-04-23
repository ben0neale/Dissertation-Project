using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ItemCollection : PlayerAttributes
{
    [SerializeField] GameObject Shield;
    [SerializeField] TextMeshProUGUI CoinText;

    public int multiplier = 0;
    [SerializeField] float multiplierTime;
    float _multiplierTime;
    int CoinNum = 0;

    public void Start()
    {
        _multiplierTime = multiplierTime;
        CoinText.text = CoinNum.ToString();
    }

    public void Update()
    {
        if (StateControllerRef.GetComponent<GamestateController>().GetGameState() == GamestateController.GameState.Play)
        {
            if (multiplier > 0)
            {
                if (_multiplierTime <= 0)
                {
                    multiplier = 0;
                    _multiplierTime = multiplierTime;
                }
                else
                    _multiplierTime -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            GetComponent<audioController>().PlayCoin();
            Destroy(other.gameObject);
            CoinNum++;
            CoinText.text = CoinNum.ToString();
        }

        if (other.gameObject.CompareTag("Multiplier"))
        {
            GetComponent<audioController>().PlayPowerup();
            Destroy(other.gameObject);
            if (multiplier == 0)
                multiplier = 2;
            else
                multiplier *= 2;
            _multiplierTime = multiplierTime;
        }
        if (other.gameObject.CompareTag("Shield"))
        {
            GetComponent<audioController>().PlayPowerup();
            Destroy(other.gameObject);
            if (Shield.activeSelf)
                Shield.GetComponent<ShieldController>().ResetTime();
            else
                Shield.SetActive(true);
        }
    }
}
