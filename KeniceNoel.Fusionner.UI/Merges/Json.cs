using System.Text;

namespace KeniceNoel.Fusionner.UI.Merges
{
    public class Json
    {

        private static string RemoveBrackets(string content)
        {
            var openBracket = content.IndexOf("[");
            content = content.Substring(openBracket + 1, content.Length - openBracket - 1);

            var closeBracket = content.LastIndexOf("]");
            content = content.Substring(0, closeBracket);

            return content;
        }

        private string Merge(string[] jsonData)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("[");
            for (var i = 0; i < jsonData.Length; i++)
            {
                var json = jsonData[i];
                var cleared = RemoveBrackets(json);
                stringBuilder.AppendLine(cleared);
                if (i != jsonData.Length - 1)
                {
                    stringBuilder.Append(",");
                }
            }

            stringBuilder.AppendLine("]");
            return stringBuilder.ToString();
        }
    }
}
