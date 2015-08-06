using UnityEngine;
using System.Collections;

public partial class UIManager
{
    //主畫面
    private void CreateMainMenu()
    {
        var but01 = CreateButton("開始遊戲", 0f, 0f, 0.4f, 0.35f);

        var but02 = CreateButton("選項", 0.35f, -0.1f, 0.25f, 0.25f);

        var but03 = CreateButton("工作人員", -0.35f, -0.1f, 0.25f, 0.25f);

        var but04 = CreateButton("離開", 0f, -0.35f, 0.2f, 0.2f);
    }
}
