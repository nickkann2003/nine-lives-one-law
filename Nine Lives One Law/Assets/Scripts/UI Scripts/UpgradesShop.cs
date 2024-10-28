using System.Collections;
using System.Collections.Generic;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;

public class UpgradesShop : MonoBehaviour
{
    // TODO: Replace this with however Upgrades work
    public List<string> upgrades = new List<string>();
    public List<Vector2> shopPositions = new List<Vector2>();

    // Prefabs
    public GameObject upgradePrefab;
    
    // References
    public GameObject upgradeLargeDisplay;
    public TextMeshProUGUI largeItemName;

    private int pages;
    private int currentPage = 0;

    // Start is called before the first frame update
    void Start()
    {
        pages = (upgrades.Count / shopPositions.Count) + 1;

        for (int i = 0; i < upgrades.Count; i++)
        {
            if (i > shopPositions.Count)
            {
                currentPage += 1;
            }

            // A for positional calculations, sets position of items on other pages
            int a = i - (currentPage * shopPositions.Count);

            // Create and initialize object
            GameObject up = Instantiate(upgradePrefab, transform);
            up.transform.localPosition = shopPositions[a];

            Button b = up.GetComponentInChildren<Button>();
            int index = i;
            b.onClick.AddListener(() => { SetLargeDisplay(upgrades[index]); });

            // If its not page 1, disable it
            if(currentPage > 0)
                up.SetActive(false);
        }

        HideLargeDisplay();
    }

    private void SetLargeDisplay(string text)
    {
        upgradeLargeDisplay.SetActive(true);
        largeItemName.text = text;
    }

    private void HideLargeDisplay()
    {
        upgradeLargeDisplay.SetActive(false);
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
