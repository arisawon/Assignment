using Ganss.Xss;

namespace Assignment.Helper
{
    /// <summary>
    /// This helper class for applying the HTML Sanitizer
    /// we can also customise the code as per our requirement.
    /// </summary>
    public class InputSanitizationService
    {
        private readonly HtmlSanitizer _sanitizer;

        /// <summary>
        /// Constructor
        /// </summary>
        public InputSanitizationService()
        {
            _sanitizer = new HtmlSanitizer();

            // Optional: Configure allowed tags and attributes
            _sanitizer.AllowedTags.Add("b");
            _sanitizer.AllowedTags.Add("i");
            _sanitizer.AllowedAttributes.Add("class");
        }


        /// <summary>
        /// For applying the sanitization
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Sanitize(string input)
        {
            return _sanitizer.Sanitize(input);
        }
    }
}
