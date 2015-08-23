using UnityEngine;
using System.Collections;

public partial class UIManager
{
    private Coroutine CreateUiSet_Co = null;

    //主畫面
    private IEnumerator CreateMainMenu()
    {
        var brd_Main = CreateUIBoard("Snow Ball 3D", TextAnchor.UpperCenter, 0f, 0f, 1f, 1f);

        var but_Start = CreateUIButton("START", 0f, 0.05f, 0.3f, 0.125f);
        but_Start.SetClickAction(this.InactiveAllButtons);
        but_Start.SetClickAction(delegate { but_Start.SetMoveStatus(MenuItemBase.emMoveStatus.Free); });
        but_Start.SetClickAction(delegate { this.ThrowSnowBall(but_Start); });
        but_Start.HitActions.Add(this.OutAllButtons);

        var but_Option = CreateUIButton("OPTION", 0f, -0.1f, 0.3f, 0.125f);
        but_Option.SetClickAction(this.InactiveAllButtons);
        but_Option.SetClickAction(delegate { but_Option.SetMoveStatus(MenuItemBase.emMoveStatus.Free); });
        but_Option.SetClickAction(delegate { this.ThrowSnowBall(but_Option); });
        but_Option.HitActions.Add(delegate { this.SwitchMenuStatus(emMainMenuStatus.Option); });

        var but_Credit = CreateUIButton("CREDIT", 0f, -0.25f, 0.3f, 0.125f);
        but_Credit.SetClickAction(this.InactiveAllButtons);
        but_Credit.SetClickAction(delegate { but_Credit.SetMoveStatus(MenuItemBase.emMoveStatus.Free); });
        but_Credit.SetClickAction(delegate { this.ThrowSnowBall(but_Credit); });
        but_Credit.HitActions.Add(delegate { this.SwitchMenuStatus(emMainMenuStatus.Credit); });

        var but_Exit = CreateUIButton("EXIT", 0f, -0.4f, 0.3f, 0.125f);
        but_Exit.SetClickAction(this.InactiveAllButtons);
        but_Exit.SetClickAction(delegate { but_Exit.SetMoveStatus(MenuItemBase.emMoveStatus.Free); });
        but_Exit.SetClickAction(delegate { this.ThrowSnowBall(but_Exit); });
        but_Exit.HitActions.Add(this.OutAllButtons);

        yield break;
    }

    //選項畫面
    private IEnumerator CreateOptionMenu()
    {
        var brd_Option = CreateUIBoard("OPTION", TextAnchor.UpperCenter, 0f, 0f, 1f, 1f);

        var but_Sound = CreateUIButton(string.Format("SOUND     {0}", Option_Sound ? "ON" : "OFF"), 0f, 0.05f, 0.3f, 0.125f);
        but_Sound.SetClickAction(delegate { Option_Sound = !Option_Sound; });
        but_Sound.SetClickAction(delegate { but_Sound.SetButtonString(string.Format("SOUND     {0}", Option_Sound ? "ON" : "OFF")); });

        var but_Music = CreateUIButton(string.Format("MUSIC     {0}", Option_Music ? "ON" : "OFF"), 0f, -0.1f, 0.3f, 0.125f);
        but_Music.SetClickAction(delegate { Option_Music = !Option_Music; });
        but_Music.SetClickAction(delegate { but_Music.SetButtonString(string.Format("MUSIC     {0}", Option_Music ? "ON" : "OFF")); });

        var but_Back = CreateUIButton("BACK", 0f, -0.35f, 0.2f, 0.15f);
        but_Back.SetClickAction(this.InactiveAllButtons);
        but_Back.SetClickAction(delegate { but_Back.SetMoveStatus(MenuItemBase.emMoveStatus.Free); });
        but_Back.SetClickAction(delegate { this.ThrowSnowBall(but_Back); });
        but_Back.HitActions.Add(delegate { this.SwitchMenuStatus(emMainMenuStatus.MainMenu); });

        yield break;
    }

    //工作人員畫面
    private IEnumerator CreateCreditMenu()
    {
        var brd_Credit00 = CreateUIBoard("\n[ UI ]\n\nDean", TextAnchor.UpperCenter, 0f, 0f, 0.5f, 0.75f);

        var but_Back = CreateUIButton("BACK", 0f, -0.35f, 0.2f, 0.15f);
        but_Back.SetClickAction(this.InactiveAllButtons);
        but_Back.SetClickAction(delegate { but_Back.SetMoveStatus(MenuItemBase.emMoveStatus.Free); });
        but_Back.SetClickAction(delegate { this.ThrowSnowBall(but_Back); });
        but_Back.HitActions.Add(delegate { this.SwitchMenuStatus(emMainMenuStatus.MainMenu); });

        yield return new WaitForSeconds(2.5f);

        OutAllBoards();
        var brd_Credit01 = CreateUIBoard("\n[ AI ]\n\nSigma", TextAnchor.UpperCenter, 0f, 0f, 0.5f, 0.75f);

        yield return new WaitForSeconds(2.5f);

        OutAllBoards();
        var brd_Credit02 = CreateUIBoard("\n[ Control ]\n\nRoger", TextAnchor.UpperCenter, 0f, 0f, 0.5f, 0.75f);

        yield return new WaitForSeconds(2.5f);

        SwitchMenuStatus(emMainMenuStatus.MainMenu);

        yield break;
    }
}
