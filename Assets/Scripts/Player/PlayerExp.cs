using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    public HealthBarUI ExpBar;

    private int currentExp = 0;
    private int currentLevel = 1;
    private int requireExp = 30;

    [SerializeField] private LevelUpPanelManager levelUpPanelManager; // Tham chiếu đến LevelUpPanelManager
    // Level + exp
    public void UpdateExperience(int addExp)
    {
        currentExp += addExp;
        if (currentExp >= requireExp)
        {
            currentLevel++;
            currentExp -= requireExp;
            requireExp = (int)(requireExp * 1.2);

            levelUpPanelManager.ShowPanel();
        }

        // Cập nhật thanh kinh nghiệm
        ExpBar.UpdateBar(currentExp, requireExp, "Level " + currentLevel.ToString());
    }
}
