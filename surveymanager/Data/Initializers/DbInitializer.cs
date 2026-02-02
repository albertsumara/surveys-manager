namespace Projekt.Data.Initializers;

public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        await RoleInitializer.InitializeAsync(services);
        await UserInitializer.InitializeAsync(services);
        await SurveyInitializer.InitializeAsync(services);
        var random = new Random();
        await SurveyResultsInitializer.InitializeAsync(services, random);
    }




}
