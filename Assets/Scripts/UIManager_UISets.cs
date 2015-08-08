using UnityEngine;
using System.Collections;

public partial class UIManager
{
    private Coroutine CreateUiSet_Co = null;

    //主畫面
    private IEnumerator CreateMainMenu()
    {
        var brd_Main = CreateUIBoard("雪球大戰3D", TextAnchor.UpperCenter, 0f, 0f, 1f, 1f);

        var but_Start = CreateUIButton("開始遊戲", 0f, 0f, 0.4f, 0.35f);
        but_Start.SetClickAction(this.InactiveAllButtons);
        but_Start.SetClickAction(delegate { but_Start.SetMoveStatus(MenuItemBase.emMoveStatus.Free); });
        but_Start.SetClickAction(delegate { this.ThrowSnowBall(but_Start); });
        but_Start.HitAction = this.OutAllButtons;

        var but_Option = CreateUIButton("選項", 0.35f, -0.1f, 0.25f, 0.2f);
        but_Option.SetClickAction(this.InactiveAllButtons);
        but_Option.SetClickAction(delegate { but_Option.SetMoveStatus(MenuItemBase.emMoveStatus.Free); });
        but_Option.SetClickAction(delegate { this.ThrowSnowBall(but_Option); });
        but_Option.HitAction = delegate { this.SwitchMenuStatus(emMainMenuStatus.Option); };

        var but_Credit = CreateUIButton("小組成員", -0.35f, -0.1f, 0.25f, 0.2f);
        but_Credit.SetClickAction(this.InactiveAllButtons);
        but_Credit.SetClickAction(delegate { but_Credit.SetMoveStatus(MenuItemBase.emMoveStatus.Free); });
        but_Credit.SetClickAction(delegate { this.ThrowSnowBall(but_Credit); });
        but_Credit.HitAction = delegate { this.SwitchMenuStatus(emMainMenuStatus.Credit); };

        var but_Exit = CreateUIButton("離開遊戲", 0f, -0.35f, 0.2f, 0.2f);
        but_Exit.SetClickAction(this.InactiveAllButtons);
        but_Exit.SetClickAction(delegate { but_Exit.SetMoveStatus(MenuItemBase.emMoveStatus.Free); });
        but_Exit.SetClickAction(delegate { this.ThrowSnowBall(but_Exit); });
        but_Exit.HitAction = this.OutAllButtons;

        yield break;
    }

    //選項畫面
    private IEnumerator CreateOptionMenu()
    {
        var brd_Option = CreateUIBoard("選項", TextAnchor.UpperCenter, 0f, 0f, 1f, 1f);

        var but_Back = CreateUIButton("返回", 0f, -0.35f, 0.2f, 0.15f);
        but_Back.SetClickAction(this.InactiveAllButtons);
        but_Back.SetClickAction(delegate { but_Back.SetMoveStatus(MenuItemBase.emMoveStatus.Free); });
        but_Back.SetClickAction(delegate { this.ThrowSnowBall(but_Back); });
        but_Back.HitAction = delegate { this.SwitchMenuStatus(emMainMenuStatus.MainMenu); };

        yield break;
    }

    //工作人員畫面
    private IEnumerator CreateCreditMenu()
    {
        var brd_Credit00 = CreateUIBoard("\n[介面]\n\nDean", TextAnchor.UpperCenter, 0f, 0f, 0.5f, 0.75f);

        var but_Back = CreateUIButton("返回", 0f, -0.35f, 0.2f, 0.15f);
        but_Back.SetClickAction(this.InactiveAllButtons);
        but_Back.SetClickAction(delegate { but_Back.SetMoveStatus(MenuItemBase.emMoveStatus.Free); });
        but_Back.SetClickAction(delegate { this.ThrowSnowBall(but_Back); });
        but_Back.HitAction = delegate { this.SwitchMenuStatus(emMainMenuStatus.MainMenu); };

        yield return new WaitForSeconds(2.5f);

        OutAllBoards();
        var brd_Credit01 = CreateUIBoard("\n[AI]\n\nSigma", TextAnchor.UpperCenter, 0f, 0f, 0.5f, 0.75f);

        yield return new WaitForSeconds(2.5f);

        OutAllBoards();
        var brd_Credit02 = CreateUIBoard("\n[人物控制]\n\nRoger", TextAnchor.UpperCenter, 0f, 0f, 0.5f, 0.75f);

        yield return new WaitForSeconds(2.5f);

        SwitchMenuStatus(emMainMenuStatus.MainMenu);

        yield break;
    }
}
