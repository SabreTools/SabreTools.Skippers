using System.Xml.Serialization;

namespace SabreTools.Skippers
{
    /// <summary>
    /// Determines the header skip operation
    /// </summary>
    public enum HeaderSkipOperation
    {
        [XmlEnum("none")]
        None = 0,

        [XmlEnum("bitswap")]
        Bitswap,

        [XmlEnum("byteswap")]
        Byteswap,

        [XmlEnum("wordswap")]
        Wordswap,

        [XmlEnum("wordbyteswap")]
        WordByteswap,
    }

    /// <summary>
    /// Determines the operator to be used in a file test
    /// </summary>
    public enum HeaderSkipTestFileOperator
    {
        [XmlEnum("equal")]
        Equal = 0,

        [XmlEnum("less")]
        Less,

        [XmlEnum("greater")]
        Greater,
    }
}
