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

        if (CreateUiSet_Co != null)
            StopCoroutine(CreateUiSet_Co);

        OutAllMenuItems();

        switch (mMenuStatus)
        { 
            case emMainMenuStatus.MainMenu:
                CreateUiSet_Co = StartCoroutine(CreateMainMenu());
                break;
            case emMainMenuStatus.Option:
                CreateUiSet_Co = StartCoroutine(CreateOptionMenu());
                break;
            case emMainMenuStatus.Credit:
                CreateUiSet_Co = StartCoroutine(CreateCreditMenu());
                break;
            case emMainMenuStatus.Loading:
                CreateUiSet_Co = StartCoroutine(CreateLoadingMenu());
                break;
            default:
                break;
        }
    }

    private void OutAllMenuItems()
    {
        OutAllButtons();
        OutAllBoards();
    }

    private void OutAllButtons()
    {
        foreach (var button in mButtonList)
        {
            if (button.MoveStatus == MenuItemBase.emMoveStatus.Stay || button.MoveStatus == MenuItemBase.emMoveStatus.In)
                button.SetMoveStatus(MenuItemBase.emMoveStatus.Out);
        }
    }

    private void OutAllBoards()
    {
        foreach (var board in mBoardList)
        {
            if (board.MoveStatus == MenuItemBase.emMoveStatus.Stay || board.MoveStatus == MenuItemBase.emMoveStatus.In)
                board.SetMoveStatus(MenuItemBase.emMoveStatus.Out);
        }
    }
}
