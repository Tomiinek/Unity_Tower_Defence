using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSpotScript : MonoBehaviour
{
    public GameManager manager;
    public MoneyScript money;

    private GameObject _build;

    private void Start()
    {
        _build = null;
    }

    public void OnBuildSpotClicked()
    {
        /*if (build != null)
        {
            var tower = build.GetComponent<TowerScript>();
            var cost = tower.cost;
            tower.Destroy();
            build = null;
            money.AddBudget(cost / 2);
        }*/

        if (_build != null) return;

        var towerPrefab = manager.GetTowerToBuild();
        if (towerPrefab == null) return;

        var cost = towerPrefab.GetComponent<TowerScript>().cost;
        if (cost > money.GetWealth()) return;

        money.RemoveBudget(cost);
        _build = ObjectPooler.InstantiatePooled(towerPrefab, transform.position, transform.rotation);
    }
}
