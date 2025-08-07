using UnityEngine;
using UnityEngine.UIElements;
public class UIHandler1 : MonoBehaviour
{
    public static UIHandler1 instance { get; private set; }
    private VisualElement m_Healthbar;
    public float displayTime = 4.0f;
private VisualElement m_NonPlayerDialogue;
private float m_TimerDisplay;

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
       m_NonPlayerDialogue = uiDocument.rootVisualElement.Q<VisualElement>("NPCDialogue");
m_NonPlayerDialogue.style.display = DisplayStyle.None;
m_TimerDisplay = -1.0f;
    }


    private void Update()
    {
        if (m_TimerDisplay > 0)
        {
            m_TimerDisplay -= Time.deltaTime;
            if (m_TimerDisplay < 0)
            {
                m_NonPlayerDialogue.style.display = DisplayStyle.None;
            }
        }
    }

public void DisplayDialogue()
{
    m_NonPlayerDialogue.style.display = DisplayStyle.Flex;
    m_TimerDisplay = displayTime;
}
   public void SetHealthValue(float percentage)
    {
        m_Healthbar.style.width = Length.Percent(100 * percentage);
    }
}
