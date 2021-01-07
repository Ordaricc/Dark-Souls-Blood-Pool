using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerSoulsSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentSoulsText;
    [SerializeField] private GameObject bloodPool;

    [SerializeField] private float checkPositionEvery = 0.2f;
    [SerializeField] private float raycastDistance = 2;
    [SerializeField] private LayerMask groundLayer;

    private Vector3 lastSafeSpot;

    [SerializeField] private int currentSouls;
    [SerializeField] private int soulsInBloodPool;

    private void Awake()
    {
        StartCoroutine(CheckIfInSafeSpot());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == bloodPool)
        {
            bloodPool.SetActive(false);
            currentSouls += soulsInBloodPool;
            soulsInBloodPool = 0;
            UpdateCurrentSoulsText();
        }
    }

    private IEnumerator CheckIfInSafeSpot()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkPositionEvery);
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, raycastDistance, groundLayer))
            {
                lastSafeSpot = hitInfo.point;
            }
        }
    }

    public void OnPlayerDeath()
    {
        bloodPool.SetActive(true);
        bloodPool.transform.position = lastSafeSpot;

        soulsInBloodPool = currentSouls;
        currentSouls = 0;
        UpdateCurrentSoulsText();
    }

    private void UpdateCurrentSoulsText()
    {
        currentSoulsText.text = "Souls: " + currentSouls;
    }

    public void AddSouls(int amount)
    {
        currentSouls += amount;
        UpdateCurrentSoulsText();
    }
}