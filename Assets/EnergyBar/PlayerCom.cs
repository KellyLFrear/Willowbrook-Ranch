using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Energy Settings")]
    public int maxEnergy = 100;
    public int currentEnergy;

    [Header("References")]
    public EnergyBar energybar;

    void Start()
    {
        if (energybar == null)
            energybar = FindObjectOfType<EnergyBar>();

        currentEnergy = maxEnergy;

        if (energybar != null)
            energybar.SetMaxEnergy(maxEnergy);
    }

    public bool HasEnoughEnergy(int amount)
    {
        return currentEnergy >= amount;
    }

    public void UseEnergy(int amount)
    {
        currentEnergy -= amount;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);

        if (energybar != null)
            energybar.SetEnergy(currentEnergy);
    }

    public void RefillEnergy()
    {
        currentEnergy = maxEnergy;

        if (energybar != null)
            energybar.SetEnergy(currentEnergy);
    }
}
