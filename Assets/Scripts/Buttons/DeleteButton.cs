using UnityEngine;

public class DeleteButton : ItemButton
{
    protected override void AddEvent()
    {
        button.onClick.AddListener(() => cell.DeleteItem());
    }
}
