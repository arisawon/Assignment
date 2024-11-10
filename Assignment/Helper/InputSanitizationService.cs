

using Ganss.Xss;

namespace Assignment.Helper
{
    public class InputSanitizationService
    {
        private readonly HtmlSanitizer _sanitizer;

        public InputSanitizationService()
        {
            _sanitizer = new HtmlSanitizer();

            // Optional: Configure allowed tags and attributes
            _sanitizer.AllowedTags.Add("b");
            _sanitizer.AllowedTags.Add("i");
            _sanitizer.AllowedAttributes.Add("class");
        }

        public string Sanitize(string input)
        {
            return _sanitizer.Sanitize(input);
        }
    }
}
