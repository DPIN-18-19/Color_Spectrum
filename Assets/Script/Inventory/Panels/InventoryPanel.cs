using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField]
    private ChipList i_chips;
    [SerializeField]
    public IWeaponChipList i_weapons;
    public AbilityList i_abilities;

    Transform i_panel;
    Transform wei_panel;
    public GameObject chip_mould;
    public GameObject weapon_chip_mould;
    public GameObject ability_chip_mould;

    private void Awake()
    {
        i_panel = transform.Find("InventoryPanel");
        wei_panel = GameObject.Find("Weight").transform;
        LoadChips();
    }

    // Use this for initialization
    void Start ()
    {

	}

    void LoadChips()
    {
        // Introducir chips normales
        for (int i = 0; i < i_chips.chips.Count; ++i)
        {
            GameObject n_chip = Instantiate(chip_mould);
            n_chip.transform.SetParent(i_panel);
            n_chip.GetComponent<IChipData>().data = i_chips.chips[i];
            //n_chip.GetComponent<RectTransform>().localPosition = Vector3.zero;

            // Comprobar el estado "equipado" y crear copia
            if(i_chips.chips[i].equipped)
            {
                Darken d = n_chip.AddComponent<Darken>();
                d.DarkenColor(0.5f);
                Destroy(n_chip.GetComponent<IChipDrag>());
                n_chip.transform.Find("Equipped").gameObject.SetActive(true);
            }

            // Comprobar si es un chip de habilidad
            //if(i_chips.chips[i].ability != "")
            //{
            //    n_chip.GetComponent<IChipData>().chip_type = IChipData.ChipType.Ability;
            //}
        }

        // Introducir chips de armas
        for (int i = 0; i < i_weapons.i_weapon_chips.Count; ++i)
        {
            GameObject n_w_chip = Instantiate(weapon_chip_mould);
            n_w_chip.transform.SetParent(i_panel);
            n_w_chip.GetComponent<IWeaponChipDrag>().w_data = i_weapons.i_weapon_chips[i];
            n_w_chip.GetComponent<IChipData>().data.id = i_weapons.i_weapon_chips[i].id;        // El id de chip es el mismo que el id de chip de arma

            // Insertar imagen
            //Debug.Log(i_weapons.i_weapon_chips[i].base_gun.display_icon);
            if (i_weapons.i_weapon_chips[i].base_gun.display_icon != null)
            {
                n_w_chip.transform.Find("ChipForm").Find("W_Picture").GetComponent<Image>().sprite = i_weapons.i_weapon_chips[i].base_gun.display_icon;
                n_w_chip.transform.Find("WeaponForm").Find("W_Picture").GetComponent<Image>().sprite = i_weapons.i_weapon_chips[i].base_gun.display_icon;
            }

            // Comprobar el estado "equipado" y crear copia
            if (i_weapons.i_weapon_chips[i].equipped)
            {
                Darken d = n_w_chip.AddComponent<Darken>();
                d.DarkenColor(0.5f);
                Destroy(n_w_chip.GetComponent<IWeaponChipDrag>());
                n_w_chip.transform.Find("Equipped").gameObject.SetActive(true);
            }
        }

        //Introducir chips de habilidades
        for (int i = 0; i < i_abilities.abi_list.Count; ++i)
        {
            GameObject n_w_chip = Instantiate(ability_chip_mould);
            n_w_chip.transform.SetParent(i_panel);
            n_w_chip.GetComponent<IAbiChipData>().abi_data = i_abilities.abi_list[i];        // El id de chip es el mismo que el id de chip de arma
            n_w_chip.GetComponent<IAbiChipData>().chip_type = IChipData.ChipType.Ability;
            n_w_chip.GetComponent<IAbiChipData>().data.id = i_abilities.abi_list[i].id;

            if (i_abilities.abi_list[i].display_icon != null)
            {
                n_w_chip.transform.Find("A_Picture").GetComponent<Image>().sprite = i_abilities.abi_list[i].display_icon;
            }

            // Comprobar el estado "equipado" y crear copia
            if (i_abilities.abi_list[i].equipped)
            {
                Darken d = n_w_chip.AddComponent<Darken>();
                d.DarkenColor(0.5f);
                Destroy(n_w_chip.GetComponent<IChipDrag>());
                n_w_chip.transform.Find("Equipped").gameObject.SetActive(true);
            }
        }
    }

    public GameObject SearchChip(ChipData chip)
    {
        for(int i = 0; i < i_panel.childCount; ++i)
        {
            if (i_panel.GetChild(i).GetComponent<IChipData>().data.id == chip.id)
                return i_panel.GetChild(i).gameObject;
        }

        Debug.Log("Not found");
        return null;
    }

    public void EquipChip(IChipData chip)
    {
        if (chip.chip_type == IChipData.ChipType.Upgrade)
        {
            // Buscar chip con id
            int index = i_chips.chips.FindIndex(x => x.id == chip.data.id);
            i_chips.chips[index].equipped = true;
        }
        else if(chip.chip_type == IChipData.ChipType.Weapon)
        {
            int index = i_weapons.i_weapon_chips.FindIndex(x => x.id == chip.data.id);
            i_weapons.i_weapon_chips[index].equipped = true;
        }
        else if(chip.chip_type == IChipData.ChipType.Ability)
        {
            int index = i_abilities.abi_list.FindIndex(x => x.id == chip.data.id);
            i_abilities.abi_list[index].equipped = true;
        }

        wei_panel.GetComponent<WeightPanel>().CalculateWeight();
    }

    public void UnequipChip(IChipData chip)
    {
        if (chip.chip_type == IChipData.ChipType.Upgrade)
        {
            // Buscar chip con id
            int index = i_chips.chips.FindIndex(x => x.id == chip.data.id);
            i_chips.chips[index].equipped = false;
        }
        else if(chip.chip_type == IChipData.ChipType.Weapon)
        {
            int index = i_weapons.i_weapon_chips.FindIndex(x => x.id == chip.data.id);
            i_weapons.i_weapon_chips[index].equipped = false;
        }
        else if (chip.chip_type == IChipData.ChipType.Ability)
        {
            int index = i_abilities.abi_list.FindIndex(x => x.id == chip.data.id);
            i_abilities.abi_list[index].equipped = false;
        }

        wei_panel.GetComponent<WeightPanel>().CalculateWeight();
    }
}
