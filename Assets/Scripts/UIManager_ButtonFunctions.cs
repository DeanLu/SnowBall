using UnityEngine;
using System.Collections;

public partial class UIManager
{
    private void InactiveAllButtons()
    {
        foreach (var button in mButtonList)
            button.ButtonInactive();
    }

    private void ThrowSnowBall(MenuItemBase _aimUI)
    {
        CreateUISnowBall(_aimUI);
    }

    private void SwitchMenuStatus(emMainMenuStatus _status)
    {
        mMenuStatus = _status;

        OutAllButtons();

        switch (mMenuStatus)
        { 
            case emMainMenuStatus.MainMenu:
                CreateMainMenu();
                break;
            case emMainMenuStatus.Option:
                CreateOptionMenu();
                break;
            case emMainMenuStatus.Credit:
                CreateCreditMenu();
                break;
            default:
                break;
        }
    }

    private void OutAllButtons()
    {
        foreach (var button in mButtonList)
        {
            if (button.MoveStatus == MenuItemBase.emMoveStatus.Stay || button.MoveStatus == MenuItemBase.emMoveStatus.In)
                button.SetMoveStatus(MenuItemBase.emMoveStatus.Out);
        }
    }
}
