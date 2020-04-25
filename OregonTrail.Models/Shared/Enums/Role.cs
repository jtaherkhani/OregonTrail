namespace OregonTrail.Models.Shared.Enums
{
    /// <summary>
    /// Defines the possible identity roles.
    /// </summary>
    /// <remarks>
    /// After adding new roles a new migration must be created to be reflected in the database.
    /// </remarks>
    public enum Role
    {
        Admin,
        GameMaster,
        Player
    }
}
