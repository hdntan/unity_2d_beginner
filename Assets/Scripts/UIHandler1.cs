using UnityEngine;
using UnityEngine.UIElements;
public class UIHandler1 : MonoBehaviour
{
    public static UIHandler1 instance { get; private set; }
    private VisualElement m_Healthbar;

       private void Awake()
   {
      instance = this;
   }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        m_Healthbar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        SetHealthValue(1);
       
    }

   public void SetHealthValue(float percentage)
{
    m_Healthbar.style.width = Length.Percent(100 * percentage);
}
}
