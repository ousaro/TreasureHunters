using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI; 

public class DamagablesHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    [SerializeField] private GameObject parent;

    private IDamagable damageable;

    private void Start()
    {
        slider.value = 1;    
        
        damageable = parent.GetComponent<IDamagable>();

        damageable.OnHealthChanged += UpdateHealth;
        damageable.OnDeath += OnDeath;

    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0); // Keep the health bar facing a fixed direction (e.g., world space forward)
    }


    private void OnDisable()
    {
        if (damageable != null)
        {
            damageable.OnHealthChanged -= UpdateHealth;
            damageable.OnDeath -= OnDeath;
        }
    }

    private void UpdateHealth(float current, float max)
    {
       
        if (slider != null)
        {
            slider.value = current / max; 
        }
    }

    private void OnDeath()
    {
        gameObject.SetActive(false);
    }
}
