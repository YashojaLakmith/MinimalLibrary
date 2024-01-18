namespace API
{
    /// <summary>
    /// Provides methods for extracting user information attached to the current HTTP context.
    /// </summary>
    public interface IContextUserExtractable
    {
        /// <summary>
        /// Gets the logged in user id for the current HTTP context.
        /// </summary>
        /// <returns>Id of the logged in user, or <c>null</c> if not found.</returns>
        string? GetCurrentUserId();
    }
}
