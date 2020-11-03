using UnityEngine;
using GameManager;

public class HealthManager : SingletonMb<HealthManager>
{
    [SerializeField] [Header("Health 4/4")]
    private GameObject healthBar4;
    [SerializeField] [Header("Health 3/4")]
    private GameObject healthBar3;
    [SerializeField] [Header("Health 2/4")]
    private GameObject healthBar2;
    [SerializeField] [Header("Health 1/4")]
    private GameObject healthBar1;

    private GameObject ActiveBar { get; set; }

    public void InitializationHealthBar()
    {
        ActiveBar = healthBar4;
        ActiveBar.SetActive(true);
    }

    public void ModifyHealth(int value)
    {
        ActiveBar.SetActive(false);
        switch (value)
        {
            case 0:
                return;
            case 1:
                ActiveBar = healthBar1;
                break;
            case 2:
                ActiveBar = healthBar2;
                break;
            case 3:
                ActiveBar = healthBar3;
                break;
            case 4:
                ActiveBar = healthBar4;
                break;
        }
        ActiveBar.SetActive(true);
    }

    protected override void Cleanup()
    {
    }

    protected override void Initialize()
    {
    }
}
