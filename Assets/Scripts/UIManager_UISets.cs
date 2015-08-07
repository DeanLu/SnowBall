using UnityEngine;
using System.Collections;

public partial class UIManager
{
    //主畫面
    private void CreateMainMenu()
    {
        var but01 = CreateButton("開始遊戲", 0f, 0f, 0.4f, 0.35f);
        but01.SetButtonAction(this.InactiveAllButtons);
        but01.SetButtonAction(delegate { but01.SetMoveStatus(MenuItemBase.emMoveStatus.Free); });
        but01.SetButtonAction(delegate { this.ThrowSnowBall(but01); });
        but01.HitAction = this.OutAllButtons;

        var but02 = CreateButton("選項", 0.35f, -0.1f, 0.25f, 0.25f);
        but02.SetButtonAction(this.InactiveAllButtons);
        but02.SetButtonAction(delegate { but02.SetMoveStatus(MenuItemBase.emMoveStatus.Free); });
        but02.SetButtonAction(delegate { this.ThrowSnowBall(but02); });
        but02.HitAction = delegate { this.SwitchMenuStatus(emMainMenuStatus.Option); };

        var but03 = CreateButton("工作人員", -0.35f, -0.1f, 0.25f, 0.25f);
        but03.SetButtonAction(this.InactiveAllButtons);
        but03.SetButtonAction(delegate { but03.SetMoveStatus(MenuItemBase.emMoveStatus.Free); });
        but03.SetButtonAction(delegate { this.ThrowSnowBall(but03); });
        but03.HitAction = delegate { this.SwitchMenuStatus(emMainMenuStatus.Credit); };

        var but04 = CreateButton("離開遊戲", 0f, -0.35f, 0.2f, 0.2f);
        but04.SetButtonAction(this.InactiveAllButtons);
        but04.SetButtonAction(delegate { but04.SetMoveStatus(MenuItemBase.emMoveStatus.Free); });
        but04.SetButtonAction(delegate { this.ThrowSnowBall(but04); });
        but04.HitAction = this.OutAllButtons;
    }

    //選項畫面
    private void CreateOptionMenu()
    {
        var but_back = CreateButton("返回", 0f, -0.35f, 0.2f, 0.2f);
        but_back.SetButtonAction(this.InactiveAllButtons);
        but_back.SetButtonAction(delegate { but_back.SetMoveStatus(MenuItemBase.emMoveStatus.Free); });
        but_back.SetButtonAction(delegate { this.ThrowSnowBall(but_back); });
        but_back.HitAction = delegate { this.SwitchMenuStatus(emMainMenuStatus.MainMenu); };
    }

    //工作人員畫面
    private void CreateCreditMenu()
    { 
        
    }
}
