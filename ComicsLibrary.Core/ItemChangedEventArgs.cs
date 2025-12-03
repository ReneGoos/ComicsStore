namespace ComicsLibrary.Core;

public class ItemChangedEventArgs(string? itemName, int? id, ActionType actionType)
{

    /// <summary>
    /// Indicates the name of the property that changed.
    /// </summary>
    public virtual string? ItemName { get; } = itemName;
    public int? Id { get; } = id;
    public ActionType ActionType { get; } = actionType;
}