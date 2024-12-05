using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponChange : MonoBehaviour
{
    [SerializeField] private GameObject Gun; 
    [SerializeField] private GameObject Shovel;

    private int currentWeaponIndex = 1;

    void Start()
    {
        ActivateWeapon(1);
    }

    public void OnSwitchWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentWeaponIndex = (currentWeaponIndex == 1) ? 2 : 1;
            ActivateWeapon(currentWeaponIndex);
        }
    }

    private void ActivateWeapon(int weaponIndex)
    {
        if (weaponIndex == 1)
        {
            Gun.SetActive(true);
            Shovel.SetActive(false);
        }
        else if (weaponIndex == 2)
        {
            Gun.SetActive(false);
            Shovel.SetActive(true);
        }
    }
}
