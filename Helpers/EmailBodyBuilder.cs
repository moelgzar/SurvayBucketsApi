namespace SurvayBucketsApi.Helpers;

public static class EmailBodyBuilder
{
    public static string GenerateEmailBody(string template, Dictionary<string, string> templeteModel)
    {
       
        var templetePath = $"{Directory.GetCurrentDirectory()}/Templetes/{template}.html";
        var stramReader = new StreamReader(templetePath);
        var emailBody = stramReader.ReadToEnd();
        foreach (var item in templeteModel)
            emailBody = emailBody.Replace(item.Key, item.Value);
        

        return emailBody;
    }
}
