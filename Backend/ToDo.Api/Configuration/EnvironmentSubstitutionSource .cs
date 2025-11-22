namespace ToDo.Api.Configuration
{
    public class EnvironmentSubstitutionSource : IConfigurationSource
    {
        private readonly IConfigurationSource _innerSource;

        public EnvironmentSubstitutionSource(IConfigurationSource innerSource)
        {
            _innerSource = innerSource;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            var innerProvider = _innerSource.Build(builder);
            return new EnvironmentVariableSubstitutionConfigurationProvider(innerProvider);
        }
    }
}
