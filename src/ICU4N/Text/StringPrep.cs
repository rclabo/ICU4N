﻿using ICU4N.Globalization;
using ICU4N.Impl;
using ICU4N.Util;
using J2N.IO;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
#nullable enable

namespace ICU4N.Text
{
    /// <summary>
    /// Options for <see cref="StringPrep"/>.
    /// </summary>
    [Flags]
    public enum StringPrepOptions
    {
        /// <summary>
        /// Option to prohibit processing of unassigned code points in the input.
        /// </summary>
        /// <see cref="StringPrep.Prepare(string, StringPrepOptions)"/>
        /// <see cref="StringPrep.Prepare(ReadOnlySpan{char}, StringPrepOptions)"/>
        /// <see cref="StringPrep.TryPrepare(ReadOnlySpan{char}, Span{char}, out int, StringPrepOptions, out StringPrepErrorType)"/>
        /// <stable>ICU 2.8</stable>
        Default = 0x0000,

        /// <summary>
        /// Option to allow processing of unassigned code points in the input.
        /// </summary>
        /// <see cref="StringPrep.Prepare(string, StringPrepOptions)"/>
        /// <see cref="StringPrep.Prepare(ReadOnlySpan{char}, StringPrepOptions)"/>
        /// <see cref="StringPrep.TryPrepare(ReadOnlySpan{char}, Span{char}, out int, StringPrepOptions, out StringPrepErrorType)"/>
        /// <stable>ICU 2.8</stable>
        AllowUnassigned = 0x0001,
    }

    /// <summary>
    /// Profiles for <see cref="StringPrep.GetInstance(StringPrepProfile)"/>.
    /// </summary>
    public enum StringPrepProfile
    {
        /// <summary>
        /// Profile type: RFC3491 Nameprep
        /// </summary>
        /// <seealso cref="StringPrep.GetInstance(StringPrepProfile)"/>
        /// <stable>ICU 4.2</stable>
        Rfc3491NamePrep = 0,

        /// <summary>
        /// Profile type: RFC3530 nfs4_cs_prep
        /// </summary>
        /// <seealso cref="StringPrep.GetInstance(StringPrepProfile)"/>
        /// <stable>ICU 4.2</stable>
        Rfc3530Nfs4CsPrep = 1,

        /// <summary>
        /// Profile type: RFC3530 nfs4_cs_prep with case insensitive option
        /// </summary>
        /// <seealso cref="StringPrep.GetInstance(StringPrepProfile)"/>
        /// <stable>ICU 4.2</stable>
        Rfc3530Nfs4CsPrepCaseInsensitive = 2,

        /// <summary>
        /// Profile type: RFC3530 nfs4_cis_prep
        /// </summary>
        /// <seealso cref="StringPrep.GetInstance(StringPrepProfile)"/>
        /// <stable>ICU 4.2</stable>
        Rfc3530Nfs4CisPrep = 3,

        /// <summary>
        /// Profile type: RFC3530 nfs4_mixed_prep for prefix
        /// </summary>
        /// <seealso cref="StringPrep.GetInstance(StringPrepProfile)"/>
        /// <stable>ICU 4.2</stable>
        Rfc3530Nfs4MixedPrepPrefix = 4,

        /// <summary>
        /// Profile type: RFC3530 nfs4_mixed_prep for suffix
        /// </summary>
        /// <seealso cref="StringPrep.GetInstance(StringPrepProfile)"/>
        /// <stable>ICU 4.2</stable>
        Rfc3530Nfs4MixedPrepSuffix = 5,

        /// <summary>
        /// Profile type: RFC3722 iSCSI
        /// </summary>
        /// <seealso cref="StringPrep.GetInstance(StringPrepProfile)"/>
        /// <stable>ICU 4.2</stable>
        Rfc3722iSCSI = 6,

        /// <summary>
        /// Profile type: RFC3920 XMPP Nodeprep
        /// </summary>
        /// <seealso cref="StringPrep.GetInstance(StringPrepProfile)"/>
        /// <stable>ICU 4.2</stable>
        Rfc3920NodePrep = 7,

        /// <summary>
        /// Profile type: RFC3920 XMPP Resourceprep
        /// </summary>
        /// <seealso cref="StringPrep.GetInstance(StringPrepProfile)"/>
        /// <stable>ICU 4.2</stable>
        Rfc3920ResourcePrep = 8,

        /// <summary>
        /// Profile type: RFC4011 Policy MIB Stringprep
        /// </summary>
        /// <seealso cref="StringPrep.GetInstance(StringPrepProfile)"/>
        /// <stable>ICU 4.2</stable>
        Rfc4011MIB = 9,

        /// <summary>
        /// Profile type: RFC4013 SASLprep
        /// </summary>
        /// <seealso cref="StringPrep.GetInstance(StringPrepProfile)"/>
        /// <stable>ICU 4.2</stable>
        Rfc4013SaslPrep = 10,

        /// <summary>
        /// Profile type: RFC4505 trace
        /// </summary>
        /// <seealso cref="StringPrep.GetInstance(StringPrepProfile)"/>
        /// <stable>ICU 4.2</stable>
        Rfc4505Trace = 11,

        /// <summary>
        /// Profile type: RFC4518 LDAP
        /// </summary>
        /// <seealso cref="StringPrep.GetInstance(StringPrepProfile)"/>
        /// <stable>ICU 4.2</stable>
        Rfc4518Ldap = 12,

        /// <summary>
        /// Profile type: RFC4518 LDAP for case ignore, numeric and stored prefix
        /// matching rules
        /// </summary>
        /// <seealso cref="StringPrep.GetInstance(StringPrepProfile)"/>
        /// <stable>ICU 4.2</stable>
        Rfc4518LdapCaseInsensitive = 13,
    }

    /// <summary>
    /// <see cref="StringPrep"/> API implements the StingPrep framework as described by 
    /// <a href="http://www.ietf.org/rfc/rfc3454.txt">RFC 3454</a>.
    /// </summary>
    /// <remarks>
    /// <see cref="StringPrep"/> prepares Unicode strings for use in network protocols.
    /// Profiles of <see cref="StringPrep"/> are set of rules and data according to which the
    /// Unicode Strings are prepared. Each profiles contains tables which describe
    /// how a code point should be treated. The tables are broadly classied into
    /// <list type="table">
    ///     <item><term>Unassigned Table</term><description>
    ///         Contains code points that are unassigned in the
    ///         Unicode Version supported by <see cref="StringPrep"/>. Currently
    ///         RFC 3454 supports Unicode 3.2.
    ///     </description></item>
    ///     <item><term>Prohibited Table</term><description>
    ///         Contains code points that are prohibted from
    ///         the output of the <see cref="StringPrep"/> processing function.
    ///     </description></item>
    ///     <item><term>Mapping Table</term><description>
    ///         Contains code ponts that are deleted from the output or case mapped.
    ///     </description></item>
    /// </list>
    /// <para/>
    /// The procedure for preparing Unicode strings:
    /// <list type="table">
    ///     <item>Map<term></term><description>
    ///         For each character in the input, check if it has a mapping
    ///         and, if so, replace it with its mapping.
    ///     </description></item>
    ///     <item>Normalize<term></term><description>
    ///         Possibly normalize the result of step 1 using Unicode
    ///         normalization.
    ///     </description></item>
    ///     <item>Prohibit<term></term><description>
    ///         Check for any characters that are not allowed in the
    ///         output.  If any are found, return an error.
    ///     </description></item>
    ///     <item>Check bidi<term></term><description>
    ///         Possibly check for right-to-left characters, and if
    ///         any are found, make sure that the whole string satisfies the
    ///         requirements for bidirectional strings.  If the string does not
    ///         satisfy the requirements for bidirectional strings, return an
    ///         error.
    ///     </description></item>
    /// </list>
    /// </remarks>
    /// <author>Ram Viswanadha</author>
    /// <stable>ICU 2.8</stable>
    public sealed class StringPrep
    {
        private const int CharStackBufferSize = 32;

        // ICU4N specific - options moved to StringPrepOptions [Flags] enum.
        // Profile constants moved to StringPrepProfile enum.

        // Last available profile
        private const StringPrepProfile MAX_PROFILE = StringPrepProfile.Rfc4518LdapCaseInsensitive;

        // Profile names must be aligned to profile type definitions 
        private static readonly string[] PROFILE_NAMES = {
            "rfc3491",      /* RFC3491_NAMEPREP */
            "rfc3530cs",    /* RFC3530_NFS4_CS_PREP */
            "rfc3530csci",  /* RFC3530_NFS4_CS_PREP_CI */
            "rfc3491",      /* RFC3530_NSF4_CIS_PREP */
            "rfc3530mixp",  /* RFC3530_NSF4_MIXED_PREP_PREFIX */
            "rfc3491",      /* RFC3530_NSF4_MIXED_PREP_SUFFIX */
            "rfc3722",      /* RFC3722_ISCSI */
            "rfc3920node",  /* RFC3920_NODEPREP */
            "rfc3920res",   /* RFC3920_RESOURCEPREP */
            "rfc4011",      /* RFC4011_MIB */
            "rfc4013",      /* RFC4013_SASLPREP */
            "rfc4505",      /* RFC4505_TRACE */
            "rfc4518",      /* RFC4518_LDAP */
            "rfc4518ci",    /* RFC4518_LDAP_CI */
        };

#if FEATURE_TYPEDWEAKREFERENCE
        private static readonly WeakReference<StringPrep>[] CACHE = new WeakReference<StringPrep>[(int)MAX_PROFILE + 1];
#else
        private static readonly WeakReference[] CACHE = new WeakReference[(int)MAX_PROFILE + 1];
#endif

        // ICU4N: Ported from usprep.cpp
        private enum StringPrepType
        {
            Unassigned = 0x0000,
            Map = 0x0001,
            Prohibited = 0x0002,
            Delete = 0x0003,
            TypeLimit = 0x0004,
        }

        private const int NORMALIZATION_ON = 0x0001;
        private const int CHECK_BIDI_ON = 0x0002;

        private const int TYPE_THRESHOLD = 0xFFF0;
        private const int MAX_INDEX_VALUE = 0x3FBF;   /*16139*/
                                                      //private static final int MAX_INDEX_TOP_LENGTH = 0x0003;

        /* indexes[] value names */
        //  private const int INDEX_TRIE_SIZE                  =  0; /* number of bytes in normalization trie */
        private const int INDEX_MAPPING_DATA_SIZE = 1; /* The array that contains the mapping   */
        private const int NORM_CORRECTNS_LAST_UNI_VERSION = 2; /* The index of Unicode version of last entry in NormalizationCorrections.txt */
        private const int ONE_UCHAR_MAPPING_INDEX_START = 3; /* The starting index of 1 UChar mapping index in the mapping data array */
        private const int TWO_UCHARS_MAPPING_INDEX_START = 4; /* The starting index of 2 UChars mapping index in the mapping data array */
        private const int THREE_UCHARS_MAPPING_INDEX_START = 5;
        private const int FOUR_UCHARS_MAPPING_INDEX_START = 6;
        private const int OPTIONS = 7; /* Bit set of options to turn on in the profile */
        private const int INDEX_TOP = 16;                          /* changing this requires a new formatVersion */


        // CharTrie implmentation for reading the trie data
        private CharTrie sprepTrie;
        // Indexes read from the data file
        private int[] indexes;
        // mapping data read from the data file
        private char[] mappingData;
        // the version of Unicode supported by the data file
        private VersionInfo? sprepUniVer;
        // the Unicode version of last entry in the
        // NormalizationCorrections.txt file if normalization
        // is turned on 
        private VersionInfo normCorrVer;
        // Option to turn on Normalization
        private bool doNFKC;
        // Option to turn on checking for BiDi rules
        private bool checkBiDi;
        // bidi properties
        private UBiDiProps? bdp;

        private char GetCodePointValue(int ch)
        {
            return sprepTrie.GetCodePointValue(ch);
        }

        private static VersionInfo GetVersionInfo(int comp)
        {
            int micro = comp & 0xFF;
            int milli = (comp >> 8) & 0xFF;
            int minor = (comp >> 16) & 0xFF;
            int major = (comp >> 24) & 0xFF;
            return VersionInfo.GetInstance(major, minor, milli, micro);
        }

        private static VersionInfo? GetVersionInfo(byte[] version)
        {
            if (version.Length != 4)
            {
                return null;
            }
            return VersionInfo.GetInstance((int)version[0], (int)version[1], (int)version[2], (int)version[3]);
        }

        /// <summary>
        /// Creates an <see cref="StringPrep"/> object after reading the input stream.
        /// The object does not hold a reference to the input steam, so the stream can be
        /// closed after the method returns.
        /// </summary>
        /// <param name="inputStream">The stream for reading the <see cref="StringPrep"/> profile binarySun.</param>
        /// <exception cref="IOException">An exception occurs when I/O of the inputstream is invalid.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="inputStream"/> is <c>null</c>.</exception>
        /// <stable>ICU 2.8</stable>
        public StringPrep(Stream inputStream)
            : this(ICUBinary.GetByteBufferFromStreamAndDisposeStream(inputStream))
        {
            // TODO: Add a public constructor that takes ByteBuffer directly.
        }

        private StringPrep(ByteBuffer bytes)
        {
            StringPrepDataReader reader = new StringPrepDataReader(bytes);

            // read the indexes
            indexes = reader.ReadIndexes(INDEX_TOP);

            sprepTrie = new CharTrie(bytes, null);

            //indexes[INDEX_MAPPING_DATA_SIZE] store the size of mappingData in bytes
            // load the rest of the data data and initialize the data members
            mappingData = reader.Read(indexes[INDEX_MAPPING_DATA_SIZE] / 2);

            // get the options
            doNFKC = ((indexes[OPTIONS] & NORMALIZATION_ON) > 0);
            checkBiDi = ((indexes[OPTIONS] & CHECK_BIDI_ON) > 0);
            sprepUniVer = GetVersionInfo(reader.GetUnicodeVersion());
            normCorrVer = GetVersionInfo(indexes[NORM_CORRECTNS_LAST_UNI_VERSION]);
            VersionInfo normUniVer = UChar.UnicodeVersion;
            if (normUniVer.CompareTo(sprepUniVer) < 0 && /* the Unicode version of SPREP file must be less than the Unicode Vesion of the normalization data */
               normUniVer.CompareTo(normCorrVer) < 0 && /* the Unicode version of the NormalizationCorrections.txt file should be less than the Unicode Vesion of the normalization data */
               ((indexes[OPTIONS] & NORMALIZATION_ON) > 0) /* normalization turned on*/
               )
            {
                throw new IOException("Normalization Correction version not supported");
            }

            if (checkBiDi)
            {
                bdp = UBiDiProps.Instance;
            }
        }

        /// <summary>
        /// Gets a <see cref="StringPrep"/> instance for the specified profile.
        /// </summary>
        /// <param name="profile">The profile passed to find the <see cref="StringPrep"/> instance.</param>
        /// <stable>ICU 4.2</stable>
        public static StringPrep GetInstance(StringPrepProfile profile)
        {
            if (profile < 0 || profile > MAX_PROFILE)
            {
                throw new ArgumentException("Bad profile type");
            }

            StringPrep? instance = null;

            // A StringPrep instance is immutable.  We use a single instance
            // per type and store it in the internal cache.
            lock (CACHE)
            {
#if FEATURE_TYPEDWEAKREFERENCE
                WeakReference<StringPrep> @ref = CACHE[(int)profile];
#else
                WeakReference @ref = CACHE[(int)profile];
#endif
                if (@ref != null)
                {
#if FEATURE_TYPEDWEAKREFERENCE
                    //instance = @ref.Get();
                    @ref.TryGetTarget(out instance);
#else
                    instance = (StringPrep) @ref.Target;
#endif
                }

                if (instance == null)
                {
                    ByteBuffer bytes = ICUBinary.GetRequiredData(PROFILE_NAMES[(int)profile] + ".spp");
                    if (bytes != null)
                    {
                        // ICU4N: Removed unnecessary try/catch
                        instance = new StringPrep(bytes);
                    }
                    if (instance != null)
                    {
#if FEATURE_TYPEDWEAKREFERENCE
                        CACHE[(int)profile] = new WeakReference<StringPrep>(instance);
#else
                        CACHE[(int)profile] = new WeakReference(instance);
#endif
                    }
                }
            }
            return instance!;
        }

        // ICU4N: Ported from usprep.cpp
        private static StringPrepType GetValues(char trieWord, out int value, out bool isIndex)
        {
            StringPrepType type;
            if (trieWord == 0)
            {
                /* 
                 * Initial value stored in the mapping table 
                 * just return TYPE_LIMIT .. so that
                 * the source codepoint is copied to the destination
                 */
                type = StringPrepType.TypeLimit;
                isIndex = false;
                value = 0;
            }
            else if (trieWord >= TYPE_THRESHOLD)
            {
                type = (StringPrepType)(trieWord - TYPE_THRESHOLD);
                isIndex = false;
                value = 0;
            }
            else
            {
                /* get the type */
                type = StringPrepType.Map;
                /* ascertain if the value is index or delta */
                if ((trieWord & 0x02) > 0)
                {
                    isIndex = true;
                    value = trieWord >> 2; //mask off the lower 2 bits and shift

                }
                else
                {
                    isIndex = false;
                    value = (trieWord << 16) >> 16;
                    value = (value >> 2);

                }

                if ((trieWord >> 2) == MAX_INDEX_VALUE)
                {
                    type = StringPrepType.Delete;
                    isIndex = false;
                    value = 0;
                }
            }
            return type;
        }

        // ICU4N: Factored out UCharacterIterator, a port of usprep.cpp/usprep_map()
        private bool TryMap(ReadOnlySpan<char> src, ref ValueStringBuilder dest, StringPrepOptions options, out StringPrepErrorType errorType, [MaybeNullWhen(true)] out string rules, out int errorPosition)
        {
            char result = (char)0;
            bool allowUnassigned = ((options & StringPrepOptions.AllowUnassigned) > 0);
            int srcLength = src.Length;

            for (int srcIndex = 0; srcIndex < srcLength;)
            {
                UTF16.Next(src, ref srcIndex, out int ch);

                result = GetCodePointValue(ch);

                StringPrepType type = GetValues(result, out int value, out bool isIndex);

                // check if the source codepoint is unassigned
                if (type == StringPrepType.Unassigned && allowUnassigned == false)
                {
                    errorType = StringPrepErrorType.UnassignedError;
                    rules = src.ToString();
                    errorPosition = srcIndex;
                    return false;
                }
                else if ((type == StringPrepType.Map))
                {
                    int index, length;

                    if (isIndex)
                    {
                        index = value;
                        if (index >= indexes[ONE_UCHAR_MAPPING_INDEX_START] &&
                                 index < indexes[TWO_UCHARS_MAPPING_INDEX_START])
                        {
                            length = 1;
                        }
                        else if (index >= indexes[TWO_UCHARS_MAPPING_INDEX_START] &&
                                index < indexes[THREE_UCHARS_MAPPING_INDEX_START])
                        {
                            length = 2;
                        }
                        else if (index >= indexes[THREE_UCHARS_MAPPING_INDEX_START] &&
                                index < indexes[FOUR_UCHARS_MAPPING_INDEX_START])
                        {
                            length = 3;
                        }
                        else
                        {
                            length = mappingData[index++];
                        }
                        /* copy mapping to destination */
                        dest.Append(mappingData, index, length);
                        continue;

                    }
                    else
                    {
                        ch -= value;
                    }
                }
                else if (type == StringPrepType.Delete)
                {
                    // just consume the codepoint and contine
                    continue;
                }
                //copy the code point into destination
                dest.AppendCodePoint(ch);
            }

            errorType = (StringPrepErrorType)(-1);
            rules = default;
            errorPosition = -1;
            return true;//return dest;
        }

        private void Normalize(ReadOnlySpan<char> source, ref ValueStringBuilder destination)
        {
#pragma warning disable 612, 618
            Normalizer.Normalize(
                source,
                NormalizerMode.NFKC,
                NormalizerUnicodeVersion.Unicode3_2,
                ref destination);
#pragma warning restore 612, 618
        }

        /*
        boolean isLabelSeparator(int ch){
            int result = getCodePointValue(ch);
            if( (result & 0x07)  == LABEL_SEPARATOR){
                return true;
            }
            return false;
        }
        */
        /*
          1) Map -- For each character in the input, check if it has a mapping
             and, if so, replace it with its mapping.  

          2) Normalize -- Possibly normalize the result of step 1 using Unicode
             normalization. 

          3) Prohibit -- Check for any characters that are not allowed in the
             output.  If any are found, return an error.  

          4) Check bidi -- Possibly check for right-to-left characters, and if
             any are found, make sure that the whole string satisfies the
             requirements for bidirectional strings.  If the string does not
             satisfy the requirements for bidirectional strings, return an
             error.  
             [Unicode3.2] defines several bidirectional categories; each character
              has one bidirectional category assigned to it.  For the purposes of
              the requirements below, an "RandALCat character" is a character that
              has Unicode bidirectional categories "R" or "AL"; an "LCat character"
              is a character that has Unicode bidirectional category "L".  Note


              that there are many characters which fall in neither of the above
              definitions; Latin digits (<U+0030> through <U+0039>) are examples of
              this because they have bidirectional category "EN".

              In any profile that specifies bidirectional character handling, all
              three of the following requirements MUST be met:

              1) The characters in section 5.8 MUST be prohibited.

              2) If a string contains any RandALCat character, the string MUST NOT
                 contain any LCat character.

              3) If a string contains any RandALCat character, a RandALCat
                 character MUST be the first character of the string, and a
                 RandALCat character MUST be the last character of the string.
        */

        internal bool TryPrepare(ReadOnlySpan<char> source, ref ValueStringBuilder destination, StringPrepOptions options, out StringPrepErrorType errorType, [MaybeNullWhen(true)] out string rules, out int errorPosition)
        {
            ValueStringBuilder mapOut = new ValueStringBuilder(stackalloc char[CharStackBufferSize]);
            ValueStringBuilder normOut = new ValueStringBuilder(stackalloc char[CharStackBufferSize]);
            try
            {
                // map 
                if (!TryMap(source, ref mapOut, options, out errorType, out rules, out errorPosition))
                    return false;

                if (doNFKC)
                {
                    // normalize 
                    Normalize(mapOut.AsSpan(), ref normOut);
                }
                else
                {
                    destination.Append(mapOut.AsSpan());
                }

                int ch;
                char result;
                ReadOnlySpan<char> normOutSpan = normOut.AsSpan();
                int normOutLength = normOut.Length;
#pragma warning disable 612, 618
                UCharacterDirection direction = UCharacterDirectionExtensions.CharDirectionCount,
                    firstCharDir = UCharacterDirectionExtensions.CharDirectionCount;
#pragma warning restore 612, 618
                int rtlPos = -1, ltrPos = -1;
                bool rightToLeft = false, leftToRight = false;

                for (int normOutIndex = 0; normOutIndex < normOutLength;)
                {
                    UTF16.Next(normOutSpan, ref normOutIndex, out ch);

                    result = GetCodePointValue(ch);

                    StringPrepType type = GetValues(result, out int value, out _);

                    if (type == StringPrepType.Prohibited)
                    {
                        errorType = StringPrepErrorType.ProhibitedError;
                        rules = normOutSpan.ToString(); //iter.GetText();
                        errorPosition = value;
                        return false;
                    }

                    if (checkBiDi)
                    {
                        direction = bdp!.GetClass(ch);
#pragma warning disable 612, 618
                        if (firstCharDir == UCharacterDirectionExtensions.CharDirectionCount)
#pragma warning restore 612, 618
                        {
                            firstCharDir = direction;
                        }
                        if (direction == UCharacterDirection.LeftToRight)
                        {
                            leftToRight = true;
                            ltrPos = normOutIndex - 1; //ltrPos = iter.Index - 1;
                        }
                        if (direction == UCharacterDirection.RightToLeft || direction == UCharacterDirection.RightToLeftArabic)
                        {
                            rightToLeft = true;
                            rtlPos = normOutIndex - 1; //rtlPos = iter.Index - 1;
                        }
                    }
                }
                if (checkBiDi == true)
                {
                    // satisfy 2
                    if (leftToRight == true && rightToLeft == true)
                    {
                        errorType = StringPrepErrorType.CheckBiDiError;
                        rules = normOutSpan.ToString(); //iter.GetText();
                        errorPosition = (rtlPos > ltrPos) ? rtlPos : ltrPos;
                        return false;
                    }

                    //satisfy 3
                    if (rightToLeft == true &&
                        !((firstCharDir == UCharacterDirection.RightToLeft || firstCharDir == UCharacterDirection.RightToLeftArabic) &&
                        (direction == UCharacterDirection.RightToLeft || direction == UCharacterDirection.RightToLeftArabic))
                      )
                    {
                        errorType = StringPrepErrorType.CheckBiDiError;
                        rules = normOutSpan.ToString(); //iter.GetText();
                        errorPosition = (rtlPos > ltrPos) ? rtlPos : ltrPos;
                        return false;
                    }
                }

                destination.Append(normOutSpan);//return normOut;
                errorType = (StringPrepErrorType)(-1);
                rules = default;
                errorPosition = -1;
                return true;
            }
            finally
            {
                mapOut.Dispose();
                normOut.Dispose();
            }
        }

        /// <summary>
        /// Prepare the input String for use in applications with the given profile. This operation maps, normalizes(NFKC),
        /// checks for prohibited and BiDi characters in the order defined by RFC 3454
        /// depending on the options specified in the profile.
        /// </summary>
        /// <param name="source">A string.</param>
        /// <param name="options">A bit set of options:
        /// <list type="bullet">
        ///     <item><term><see cref="StringPrepOptions.Default"/></term><description>Prohibit processing of unassigned code points in the input.</description></item>
        ///     <item><term><see cref="StringPrepOptions.AllowUnassigned"/></term><description>Treat the unassigned code points are in the input as normal Unicode code points.</description></item>
        /// </list>
        /// </param>
        /// <returns>A string containing the output.</returns>
        /// <exception cref="StringPrepFormatException">An exception occurs when parsing a string is invalid.</exception>
        /// <stable>ICU 4.2</stable>
        public string Prepare(string source, StringPrepOptions options)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            return Prepare(source.AsSpan(), options);
        }

        /// <summary>
        /// Prepare the input String for use in applications with the given profile. This operation maps, normalizes(NFKC),
        /// checks for prohibited and BiDi characters in the order defined by RFC 3454
        /// depending on the options specified in the profile.
        /// </summary>
        /// <param name="source">A string.</param>
        /// <param name="options">A bit set of options:
        /// <list type="bullet">
        ///     <item><term><see cref="StringPrepOptions.Default"/></term><description>Prohibit processing of unassigned code points in the input.</description></item>
        ///     <item><term><see cref="StringPrepOptions.AllowUnassigned"/></term><description>Treat the unassigned code points are in the input as normal Unicode code points.</description></item>
        /// </list>
        /// </param>
        /// <returns>A string containing the output.</returns>
        /// <exception cref="StringPrepFormatException">An exception occurs when parsing a string is invalid.</exception>
        /// <stable>ICU 4.2</stable>
        public string Prepare(ReadOnlySpan<char> source, StringPrepOptions options)
        {
            ValueStringBuilder sb = new ValueStringBuilder(stackalloc char[CharStackBufferSize]);
            try
            {
                if (!TryPrepare(source, ref sb, options, out StringPrepErrorType errorType, out string? rules, out int errorPosition))
                    ThrowHelper.ThrowStringPrepFormatException(errorType, rules, errorPosition);
                return sb.ToString();
            }
            finally
            {
                sb.Dispose();
            }
        }

        /// <summary>
        /// Prepare the input String for use in applications with the given profile. This operation maps, normalizes(NFKC),
        /// checks for prohibited and BiDi characters in the order defined by RFC 3454
        /// depending on the options specified in the profile.
        /// </summary>
        /// <param name="source">A string.</param>
        /// <param name="destination">The span in which to write the converted value.</param>
        /// <param name="charsLength">Upon return, will contain the length of <paramref name="destination"/> after
        /// the operation. If the return value is <c>false</c>,
        /// this will contain the length of <paramref name="destination"/> that would need to be provided to make the
        /// operation succeed.</param>
        /// <param name="options">A bit set of options:
        /// <list type="bullet">
        ///     <item><term><see cref="StringPrepOptions.Default"/></term><description>Prohibit processing of unassigned code points in the input.</description></item>
        ///     <item><term><see cref="StringPrepOptions.AllowUnassigned"/></term><description>Treat the unassigned code points are in the input as normal Unicode code points.</description></item>
        /// </list>
        /// </param>
        /// <param name="errorType">Upon unsuccessful return (<c>false</c>), will contain the type of error that occurred.</param>
        /// <returns><c>true</c> if the operation was successful; otherwise, <c>false</c>.</returns>
        /// <draft>ICU 60.1</draft>
        public bool TryPrepare(ReadOnlySpan<char> source, Span<char> destination, out int charsLength, StringPrepOptions options, out StringPrepErrorType errorType)
        {
            ValueStringBuilder sb = new ValueStringBuilder(destination);
            try
            {
                bool success = TryPrepare(source, ref sb, options, out errorType, out _, out _);
                if (!sb.FitsInitialBuffer(out charsLength) && success)
                {
                    errorType = StringPrepErrorType.BufferOverflowError;
                    return false;
                }
                return success;
            }
            finally
            {
                sb.Dispose();
            }
        }
    }
}
