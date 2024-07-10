using UnityEngine.UIElements;
using UnityEngine;
using System.Collections.Generic;
public class HealthBar : VisualElement, INotifyValueChanged<float>
{

    public int Width { get; set; }

    public int Height { get; set; }

    private float m_value;
    public float value { get
        {
            m_value = Mathf.Clamp(m_value, 0f, 1f);
            return m_value;
        }
        set
        {
            if (EqualityComparer<float>.Default.Equals(m_value, value))
                return;
            if (this.panel != null)
            {
                using (ChangeEvent<float> pooled = ChangeEvent<float>.GetPooled(m_value, value))
                {
                    pooled.target = (IEventHandler)this;
                    this.SetValueWithoutNotify(value);
                    this.SendEvent((EventBase)pooled);
                }
            }
            else
            {
                SetValueWithoutNotify(value);
            }
        }
    }

    public enum FillType
    {
        Horizontal,
        Vertical,
    }

    public FillType fillType;

    private VisualElement _hpParent;
    private VisualElement _hpBackground;
    private VisualElement _hpForeGround;

    public new class UxmlFactory : UxmlFactory<HealthBar, UxmlTraits> { }

    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlIntAttributeDescription m_width = new UxmlIntAttributeDescription() { name = "Width", defaultValue = 400 };
        UxmlIntAttributeDescription m_height = new UxmlIntAttributeDescription() { name = "Height", defaultValue = 50 };
        UxmlFloatAttributeDescription m_value = new UxmlFloatAttributeDescription() { name = "Value", defaultValue = 1 };
        UxmlEnumAttributeDescription<HealthBar.FillType> m_fillType = new UxmlEnumAttributeDescription<FillType>() { name = "fill-type", defaultValue = 0 };

        public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
        {
            get { yield break; }
        }

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as HealthBar;
            ate.Width = m_width.GetValueFromBag(bag, cc);
            ate.Height = m_height.GetValueFromBag(bag, cc);
            ate.value = m_value.GetValueFromBag(bag, cc);
            ate.fillType = m_fillType.GetValueFromBag(bag, cc);

            ate.Clear();
            VisualTreeAsset vt = Resources.Load<VisualTreeAsset>("UI Documents/HealthBar");
            VisualElement healthbar = vt.Instantiate();
            ate._hpParent = healthbar.Q<VisualElement>("healthBar");
            ate._hpBackground = healthbar.Q<VisualElement>("background");
            ate._hpForeGround = healthbar.Q<VisualElement>("foreground");
            ate.Add(healthbar);

            ate._hpParent.style.width = ate.Width;
            ate._hpParent.style.height = ate.Height;
            ate.style.width = ate.Width;
            ate.style.height = ate.Height;
            ate.RegisterValueChangedCallback(ate.UpdateHealth);
            ate.FillHealth();

            if (ate.fillType == FillType.Horizontal)
            {
                ate._hpForeGround.style.scale = new Scale(new Vector3(ate.value, 1, 0));
            }
            else
            {
                ate._hpForeGround.style.scale = new Scale(new Vector3(1, ate.value, 0));
            }

        }
    }

    public void UpdateHealth(ChangeEvent<float> evt)
    {
        FillHealth();
    }


    private void FillHealth()
    {
        if (fillType == FillType.Horizontal)
        {
            _hpForeGround.style.scale = new Scale(new Vector3(value, 1, 0));
        }
        else
        {
            _hpForeGround.style.scale = new Scale(new Vector3(1, value, 0));
        }

    }

    public void SetValueWithoutNotify(float newValue)
    {
        m_value = newValue;
    }
}
