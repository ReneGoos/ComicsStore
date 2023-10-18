namespace ComicsLibrary.Core;


public class ItemChangedEventArgs
{
    public ItemChangedEventArgs(string? itemName, int? id, ActionType actionType)
    {
        ItemName = itemName;
        Id = id;
        ActionType = actionType;
    }

    /// <summary>
    /// Indicates the name of the property that changed.
    /// </summary>
    public virtual string? ItemName { get; }
    public int? Id { get; }
    public ActionType ActionType { get; }
}