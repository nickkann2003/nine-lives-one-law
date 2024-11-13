using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;

public class UpgradesShop : MonoBehaviour
{
    // TODO: Replace this with however Upgrades work
    public GameObject upgradesHolder;
    private UpgradeBase[] upgrades;
    public List<Vector2> shopPositions = new List<Vector2>();

    // Prefabs
    public GameObject upgradePrefab;
    
    // References
    public GameObject upgradeLargeDisplay;
    public TextMeshProUGUI largeItemName;
    public TextMeshProUGUI largeItemDescription;
    public TextMeshProUGUI largeItemPrice;
    public Image largeItemImage;
    public Button purchaseButton;
    public GameObject soldOut;
    private UpgradeBase selectedUpgrade;
    private UpgradesShopDisplay selectedUpgradeDisplay;

    private List<UpgradesShopDisplay> displays = new List<UpgradesShopDisplay>();
    private int pages;
    private int currentPage = 0;

    // Start is called before the first frame update
    void Start()
    {
        upgrades = upgradesHolder.GetComponentsInChildren<UpgradeBase>();
        pages = ((upgrades.Length-1) / shopPositions.Count) + 1;

        for (int i = 0; i < (upgrades.Length); i++)
        {
            if (i > shopPositions.Count)
            {
                currentPage += 1;
            }

            // A for positional calculations, sets position of items on other pages
            int a = i - (currentPage * shopPositions.Count);
            int index = i;

            // Create and initialize object
            GameObject up = Instantiate(upgradePrefab, transform);
            up.transform.localPosition = shopPositions[a];
            UpgradesShopDisplay uDisplay = up.GetComponent<UpgradesShopDisplay>();
            displays.Add(uDisplay);
            uDisplay.SetUpgrade(upgrades[index]);

            Button b = up.GetComponentInChildren<Button>();
            b.onClick.AddListener(() => { 
                SetLargeDisplay(upgrades[index], displays[index]);
            });

            // If its not page 1, disable it
            if(currentPage > 0)
                up.SetActive(false);
        }

        HideLargeDisplay();
    }

    private void SetLargeDisplay(UpgradeBase u, UpgradesShopDisplay uDisplay)
    {
        upgradeLargeDisplay.SetActive(true);
        largeItemName.text = u.itemName;
        largeItemDescription.text = u.itemDescription;
        largeItemPrice.text = u.itemShopPrice.ToString();
        selectedUpgrade = u;
        selectedUpgradeDisplay = uDisplay;
        soldOut.SetActive(!selectedUpgradeDisplay.soldOut);

        
        if(u.itemSprite != null)
            largeItemImage.sprite = u.itemSprite;
    }

    private void HideLargeDisplay()
    {
        upgradeLargeDisplay.SetActive(false);
    }

    public void PurchaseItem()
    {
        if (StatsManager.instance.CheckBalanceHasEnough(selectedUpgrade.itemShopPrice) && selectedUpgrade != null)
        {
            StatsManager.instance.SubtractMoney(selectedUpgrade.itemShopPrice);
            selectedUpgradeDisplay.PurchaseUpgrade();
            selectedUpgrade.PerformUpgrade();
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Canvas c = GetComponentInParent<Canvas>();
        Gizmos.matrix = c.transform.localToWorldMatrix;
        Handles.matrix = c.transform.localToWorldMatrix;
        Handles.color = Color.blue;
        Gizmos.color = Color.blue;
        int i = 0;
        foreach (Vector2 p in shopPositions)
        {
            i++;
            Gizmos.DrawWireCube(new Vector3(p.x, p.y, 1f), new Vector3(100, 100));
            Handles.Label(new Vector3(p.x-40f, p.y+40f, 1f), "" + i);
        }
    }
#endif

}
