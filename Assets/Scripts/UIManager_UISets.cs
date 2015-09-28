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
        but_Start.HitActions.Add(delegate { this.SwitchMenuStatus(emMainMenuStatus.Loading); });

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
        but_Exit.HitActions.Add(this.OutAllMenuItems);

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

    //Loading畫面
    private IEnumerator CreateLoadingMenu()
    {
        while (true)
        {
            int usingButtons = 0;

            for (int i = 0; i < mButtonList.Count; i++)
            {
                var moveState = mButtonList[i].MoveStatus;
                if (moveState == MenuItemBase.emMoveStatus.Stay || moveState == MenuItemBase.emMoveStatus.In)
                    ++usingButtons;
            }

            if (usingButtons == 0)
            {
                var but_L = CreateUIButton("L", -0.35f, -0.025f, 0.1f, 0.15f);
                but_L.SetClickAction(delegate { but_L.SetMoveStatus(MenuItemBase.emMoveStatus.Free); });
                but_L.SetClickAction(delegate { this.ThrowSnowBall(but_L); });

                var but_O = CreateUIButton("o", -0.25f, -0.01f, 0.1f, 0.15f);
                but_O.SetClickAction(delegate { but_O.SetMoveStatus(MenuItemBase.emMoveStatus.Free); });
                but_O.SetClickAction(delegate { this.ThrowSnowBall(but_O); });

                var but_A = CreateUIButton("a", -0.15f, -0.015f, 0.1f, 0.15f);
                but_A.SetClickAction(delegate { but_A.SetMoveStatus(MenuItemBase.emMoveStatus.Free); });
                but_A.SetClickAction(delegate { this.ThrowSnowBall(but_A); });

                var but_D = CreateUIButton("d", -0.05f, -0.035f, 0.1f, 0.15f);
                but_D.SetClickAction(delegate { but_D.SetMoveStatus(MenuItemBase.emMoveStatus.Free); });
                but_D.SetClickAction(delegate { this.ThrowSnowBall(but_D); });

                var but_I = CreateUIButton("i", 0.05f, -0.025f, 0.1f, 0.15f);
                but_I.SetClickAction(delegate { but_I.SetMoveStatus(MenuItemBase.emMoveStatus.Free); });
                but_I.SetClickAction(delegate { this.ThrowSnowBall(but_I); });

                var but_N = CreateUIButton("n", 0.15f, -0.015f, 0.1f, 0.15f);
                but_N.SetClickAction(delegate { but_N.SetMoveStatus(MenuItemBase.emMoveStatus.Free); });
                but_N.SetClickAction(delegate { this.ThrowSnowBall(but_N); });

                var but_G = CreateUIButton("g", 0.25f, -0.01f, 0.1f, 0.15f);
                but_G.SetClickAction(delegate { but_G.SetMoveStatus(MenuItemBase.emMoveStatus.Free); });
                but_G.SetClickAction(delegate { this.ThrowSnowBall(but_G); });

                var but_end = CreateUIButton("...", 0.35f, -0.05f, 0.1f, 0.15f);
                but_end.SetClickAction(delegate { but_end.SetMoveStatus(MenuItemBase.emMoveStatus.Free); });
                but_end.SetClickAction(delegate { this.ThrowSnowBall(but_end); });
            }

            yield return null;
        }
    }
}
