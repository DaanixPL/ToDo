namespace ToDo.Api.Configuration
{
    public class EnvironmentVariableSubstitutionConfigurationProvider : ConfigurationProvider
    {
        private readonly IConfigurationProvider _inner;

        public EnvironmentVariableSubstitutionConfigurationProvider(IConfigurationProvider inner)
        {
            _inner = inner;
        }
        public override void Load()
        {
            _inner.Load();

            foreach (var key in _inner.GetChildKeys(Enumerable.Empty<string>(), null))
            {
                if (_inner.TryGet(key, out var value) && value != null && value.StartsWith("${") && value.EndsWith("}"))
                {
                    var envVar = value.Substring(2, value.Length - 3);
                    var envValue = Environment.GetEnvironmentVariable(envVar);
                    if (envValue != null)
                    {
                        Set(key, envValue);
                    }
                }
                else
                {
                    Set(key, value);
                }
            }
        }
    }
}
