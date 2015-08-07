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

    private void OutAllButtons()
    {
        foreach (var button in mButtonList)
        {
            if (button.MoveStatus == MenuItemBase.emMoveStatus.Stay || button.MoveStatus == MenuItemBase.emMoveStatus.In)
                button.SetMoveStatus(MenuItemBase.emMoveStatus.Out);
        }
    }
}
