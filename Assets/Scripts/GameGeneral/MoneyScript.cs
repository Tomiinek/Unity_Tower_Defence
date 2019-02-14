using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class MoneyScript : MonoBehaviour
{
    public int startingWealth;
    protected int currentWealth;

    public float goldProbability;
    public float silverProbability;
    public float cooperProbability;

    public GameObject goldPrefab;
    public GameObject silverPrefab;
    public GameObject cooperPrefab;
    public AudioClip coinAudio;

    public Text moneyText;

    private AudioSource _aus;
    
    public void Awake()
    {
        _aus = gameObject.GetComponent<AudioSource>();
        currentWealth = startingWealth;
        moneyText.text = currentWealth.ToString();
    }

    public void CreateCoin(Vector3 position)
    {
        var rnd = Random.Range(0.0f, 1.0f);

        GameObject coinType;
        if (rnd < goldProbability)
        {
            coinType = goldPrefab;
        }
        else if (rnd < goldProbability + silverProbability)
        {
            coinType = silverPrefab;
        }
        else if (rnd < goldProbability + silverProbability + cooperProbability)
        {
            coinType = cooperPrefab;
        }
        else return;

        var coin = coinType.GetComponent<CoinScript>();
        Debug.Assert(coin != null);
        int coinPrice = coin.price;

        System.Action<BaseEventData> callback = (c) => AddBudgetWithSound(coinPrice);
        System.Action<EventTrigger> coinSetup = (c) =>
        {
            var entry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerClick,
                callback = new EventTrigger.TriggerEvent()
            };
            entry.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData>(callback));

            c.triggers.Add(entry);
        };

        ObjectPooler.InstantiatePooled(coinType, position + new Vector3(Random.Range(-25.0f, 25.0f), Random.Range(-25.0f, 25.0f), 0), Quaternion.identity, coinSetup);
    }

    public void AddBudgetWithSound(int amount)
    {
        AddBudget(amount);
        _aus.PlayOneShot(coinAudio);
    }

    public void AddBudget(int amount)
    {
        currentWealth += amount;
        moneyText.text = currentWealth.ToString();
    }

    public void RemoveBudget(int amount)
    {
        currentWealth -= amount;
        moneyText.text = currentWealth.ToString();
    }

    public int GetWealth()
    {
        return currentWealth;
    }
}
