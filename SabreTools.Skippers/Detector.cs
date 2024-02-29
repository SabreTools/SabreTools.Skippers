using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace SabreTools.Skippers
{
    [XmlRoot("detector")]
    public abstract class Detector
    {
        #region Fields

        /// <summary>
        /// Detector name
        /// </summary>
        [XmlElement("name")]
        public string? Name { get; protected set; }

        /// <summary>
        /// Author names
        /// </summary>
        [XmlElement("author")]
        public string? Author { get; protected set; }

        /// <summary>
        /// File version
        /// </summary>
        [XmlElement("version")]
        public string? Version { get; protected set; }

        /// <summary>
        /// Set of all rules in the skipper
        /// </summary>
        [XmlElement("rule")]
        public Rule[]? Rules { get; protected set; }

        /// <summary>
        /// Filename the skipper lives in
        /// </summary>
        [XmlIgnore]
        public string? SourceFile { get; protected set; }

        #endregion

        #region Matching

        /// <summary>
        /// Get the Rule associated with a given stream
        /// </summary>
        /// <param name="input">Stream to be checked</param>
        /// <param name="skipperName">Name of the skipper to be used, blank to find a matching skipper</param>
        /// <returns>The Rule that matched the stream, null otherwise</returns>
        public Rule? GetMatchingRule(Stream input, string skipperName)
        {
            // If we have no name supplied, try to blindly match
            if (string.IsNullOrEmpty(skipperName))
                return GetMatchingRule(input);

            // If the name matches the internal name of the skipper
            else if (string.Equals(skipperName, Name, StringComparison.OrdinalIgnoreCase))
                return GetMatchingRule(input);

            // If the name matches the source file name of the skipper
            else if (string.Equals(skipperName, SourceFile, StringComparison.OrdinalIgnoreCase))
                return GetMatchingRule(input);

            // Otherwise, nothing matches by default
            return null;
        }

        /// <summary>
        /// Get the matching Rule from all Rules, if possible
        /// </summary>
        /// <param name="input">Stream to be checked</param>
        /// <returns>The Rule that matched the stream, null otherwise</returns>
        private Rule? GetMatchingRule(Stream input)
        {
            // If we have no rules
            if (Rules == null)
                return null;

            // Loop through the rules until one is found that works
            foreach (Rule rule in Rules)
            {
                // Always reset the stream back to the original place
                input.Seek(0, SeekOrigin.Begin);

                // If all tests in the rule pass
                if (rule.PassesAllTests(input))
                    return rule;
            }

            // If nothing passed
            return null;
        }

        #endregion
    }
}
