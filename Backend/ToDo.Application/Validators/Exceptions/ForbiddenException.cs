namespace ToDo.Application.Validators.Exceptions
{
    public class ForbiddenException : Exception
    {
        public string EntityName { get; }
        public object Key { get; }

        public ForbiddenException(string entityName, object key)
            : base($"{entityName} {key} is forbidden.")
        {
            EntityName = entityName;
            Key = key;
        }
    }
}
