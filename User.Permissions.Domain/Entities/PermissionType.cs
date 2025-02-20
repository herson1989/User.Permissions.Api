namespace User.Permissions.Domain.Entities
{
    public class PermissionType
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public Permission Permission { get; set; }
    }
}
