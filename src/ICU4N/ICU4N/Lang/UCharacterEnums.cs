﻿using System;
using System.Globalization;

namespace ICU4N.Lang
{
    /// <summary>
    /// Extension methods for <see cref="UnicodeCategory"/>.
    /// </summary>
    public static class UnicodeCategoryConvert
    {
        /// <summary>
        /// Converts a <see cref="UnicodeCategory"/> to the integer value that is used by
        /// ICU4N, which differs from the numeric value of the <see cref="UnicodeCategory"/> enum
        /// that is defined in System.Globalization.
        /// </summary>
        /// <param name="unicodeCategory">A <see cref="UnicodeCategory"/> value.</param>
        /// <returns>The integer associated with the <see cref="UnicodeCategory"/> for comparisions in ICU4N.</returns>
        public static int ToIcuValue(this UnicodeCategory unicodeCategory)
        {
            switch (unicodeCategory)
            {
                case UnicodeCategory.OtherNotAssigned: return 0; // UNASSIGNED / GENERAL_OTHER_TYPES
                case UnicodeCategory.UppercaseLetter: return 1;
                case UnicodeCategory.LowercaseLetter: return 2;
                case UnicodeCategory.TitlecaseLetter: return 3;
                case UnicodeCategory.ModifierLetter: return 4;
                case UnicodeCategory.OtherLetter: return 5;
                case UnicodeCategory.NonSpacingMark: return 6;
                case UnicodeCategory.EnclosingMark: return 7;
                case UnicodeCategory.SpacingCombiningMark: return 8;
                case UnicodeCategory.DecimalDigitNumber: return 9;
                case UnicodeCategory.LetterNumber: return 10;
                case UnicodeCategory.OtherNumber: return 11;
                case UnicodeCategory.SpaceSeparator: return 12;
                case UnicodeCategory.LineSeparator: return 13;
                case UnicodeCategory.ParagraphSeparator: return 14;
                case UnicodeCategory.Control: return 15;
                case UnicodeCategory.Format: return 16;
                case UnicodeCategory.PrivateUse: return 17;
                case UnicodeCategory.Surrogate: return 18;
                case UnicodeCategory.DashPunctuation: return 19;
                case UnicodeCategory.OpenPunctuation: return 20;
                case UnicodeCategory.ClosePunctuation: return 21;
                case UnicodeCategory.ConnectorPunctuation: return 22;
                case UnicodeCategory.OtherPunctuation: return 23;
                case UnicodeCategory.MathSymbol: return 24;
                case UnicodeCategory.CurrencySymbol: return 25;
                case UnicodeCategory.ModifierSymbol: return 26;
                case UnicodeCategory.OtherSymbol: return 27;
                case UnicodeCategory.InitialQuotePunctuation: return 28;
                case UnicodeCategory.FinalQuotePunctuation: return 29;
                default: return 0; // UNASSIGNED / GENERAL_OTHER_TYPES
            }
        }

        /// <summary>
        /// Converts an integer value used by ICU4N to the corresponding <see cref="UnicodeCategory"/> value.
        /// </summary>
        /// <param name="value">An integer representing an ICU4J UCategory constant.</param>
        /// <returns>The <see cref="UnicodeCategory"/> representing the passed in <paramref name="value"/>.</returns>
        public static UnicodeCategory FromIcuValue(int value)
        {
            switch (value)
            {
                case 0: return UnicodeCategory.OtherNotAssigned; // UNASSIGNED / GENERAL_OTHER_TYPES
                case 1: return UnicodeCategory.UppercaseLetter;
                case 2: return UnicodeCategory.LowercaseLetter;
                case 3: return UnicodeCategory.TitlecaseLetter;
                case 4: return UnicodeCategory.ModifierLetter;
                case 5: return UnicodeCategory.OtherLetter;
                case 6: return UnicodeCategory.NonSpacingMark;
                case 7: return UnicodeCategory.EnclosingMark;
                case 8: return UnicodeCategory.SpacingCombiningMark;
                case 9: return UnicodeCategory.DecimalDigitNumber;
                case 10: return UnicodeCategory.LetterNumber;
                case 11: return UnicodeCategory.OtherNumber;
                case 12: return UnicodeCategory.SpaceSeparator;
                case 13: return UnicodeCategory.LineSeparator;
                case 14: return UnicodeCategory.ParagraphSeparator;
                case 15: return UnicodeCategory.Control;
                case 16: return UnicodeCategory.Format;
                case 17: return UnicodeCategory.PrivateUse;
                case 18: return UnicodeCategory.Surrogate;
                case 19: return UnicodeCategory.DashPunctuation;
                case 20: return UnicodeCategory.OpenPunctuation;
                case 21: return UnicodeCategory.ClosePunctuation;
                case 22: return UnicodeCategory.ConnectorPunctuation;
                case 23: return UnicodeCategory.OtherPunctuation;
                case 24: return UnicodeCategory.MathSymbol;
                case 25: return UnicodeCategory.CurrencySymbol;
                case 26: return UnicodeCategory.ModifierSymbol;
                case 27: return UnicodeCategory.OtherSymbol;
                case 28: return UnicodeCategory.InitialQuotePunctuation;
                case 29: return UnicodeCategory.FinalQuotePunctuation;
                default: return UnicodeCategory.OtherNotAssigned; // UNASSIGNED / GENERAL_OTHER_TYPES
            }
        }
    }


    //public enum ECharacterCategory : byte
    //{
    //    /**
    //     * Unassigned character type
    //     * @stable ICU 2.1
    //     */
    //    Unassigned = 0,

    //    /**
    //     * Character type Cn
    //     * Not Assigned (no characters in [UnicodeData.txt] have this property)
    //     * @stable ICU 2.6
    //     */
    //    GeneralOtherTypes = 0,

    //    /**
    //     * Character type Lu
    //     * @stable ICU 2.1
    //     */
    //    UppercaseLetter = 1,

    //    /**
    //     * Character type Ll
    //     * @stable ICU 2.1
    //     */
    //    LowercaseLetter = 2,

    //    /**
    //     * Character type Lt
    //     * @stable ICU 2.1
    //     */

    //    TitleCaseLetter = 3,

    //    /**
    //     * Character type Lm
    //     * @stable ICU 2.1
    //     */
    //    ModifierLetter = 4,

    //    /**
    //     * Character type Lo
    //     * @stable ICU 2.1
    //     */
    //    OtherLetter = 5,

    //    /**
    //     * Character type Mn
    //     * @stable ICU 2.1
    //     */
    //    NonSpacingMark = 6,

    //    /**
    //     * Character type Me
    //     * @stable ICU 2.1
    //     */
    //    EnclosingMark = 7,

    //    /**
    //     * Character type Mc
    //     * @stable ICU 2.1
    //     */
    //    CombiningSpacingMark = 8,

    //    /**
    //     * Character type Nd
    //     * @stable ICU 2.1
    //     */
    //    DecimalDigitNumber = 9,

    //    /**
    //     * Character type Nl
    //     * @stable ICU 2.1
    //     */
    //    LetterNumber = 10,

    //    /**
    //     * Character type No
    //     * @stable ICU 2.1
    //     */
    //    OtherNumber = 11,

    //    /**
    //     * Character type Zs
    //     * @stable ICU 2.1
    //     */
    //    SpaceSeparator = 12,

    //    /**
    //     * Character type Zl
    //     * @stable ICU 2.1
    //     */
    //    LineSeparator = 13,

    //    /**
    //     * Character type Zp
    //     * @stable ICU 2.1
    //     */
    //    ParagraphSeparator = 14,

    //    /**
    //     * Character type Cc
    //     * @stable ICU 2.1
    //     */
    //    Control = 15,

    //    /**
    //     * Character type Cf
    //     * @stable ICU 2.1
    //     */
    //    Format = 16,

    //    /**
    //     * Character type Co
    //     * @stable ICU 2.1
    //     */
    //    PrivateUse = 17,

    //    /**
    //     * Character type Cs
    //     * @stable ICU 2.1
    //     */
    //    Surrogate = 18,

    //    /**
    //     * Character type Pd
    //     * @stable ICU 2.1
    //     */
    //    DashPunctuation = 19,

    //    /**
    //     * Character type Ps
    //     * @stable ICU 2.1
    //     */
    //    StartPunctuation = 20,

    //    /**
    //     * Character type Pe
    //     * @stable ICU 2.1
    //     */
    //    EndPunctuation = 21,

    //    /**
    //     * Character type Pc
    //     * @stable ICU 2.1
    //     */
    //    ConnectorPunctuation = 22,

    //    /**
    //     * Character type Po
    //     * @stable ICU 2.1
    //     */
    //    OtherPunctuation = 23,

    //    /**
    //     * Character type Sm
    //     * @stable ICU 2.1
    //     */
    //    MathSymbol = 24,

    //    /**
    //     * Character type Sc
    //     * @stable ICU 2.1
    //     */
    //    CurrencySymbol = 25,

    //    /**
    //     * Character type Sk
    //     * @stable ICU 2.1
    //     */
    //    ModifierSymbol = 26,

    //    /**
    //     * Character type So
    //     * @stable ICU 2.1
    //     */
    //    OtherSymbol = 27,

    //    /**
    //     * Character type Pi
    //     * @see #INITIAL_QUOTE_PUNCTUATION
    //     * @stable ICU 2.1
    //     */
    //    InitialPunctuation = 28,

    //    /**
    //     * Character type Pi
    //     * This name is compatible with java.lang.Character's name for this type.
    //     * @see #INITIAL_PUNCTUATION
    //     * @stable ICU 2.8
    //     */
    //    InitialQuotePunctuation = 28,

    //    /**
    //     * Character type Pf
    //     * @see #FINAL_QUOTE_PUNCTUATION
    //     * @stable ICU 2.1
    //     */
    //    FinalPunctuation = 29,

    //    /**
    //     * Character type Pf
    //     * This name is compatible with java.lang.Character's name for this type.
    //     * @see #FINAL_PUNCTUATION
    //     * @stable ICU 2.8
    //     */
    //    FinalQuotePunctuation = 29,

    //    /**
    //     * One more than the highest normal ECharacterCategory value.
    //     * This numeric value is stable (will not change), see
    //     * http://www.unicode.org/policies/stability_policy.html#Property_Value
    //     * @stable ICU 2.1
    //     */
    //    CharCategoryCount = 30
    //}

    public enum UnicodeDirection
    {
        /**
 * Directional type L
 * @stable ICU 2.1
 */
        LeftToRight = 0,

        /**
         * Equivalent to {@link
         * java.lang.Character#DIRECTIONALITY_LEFT_TO_RIGHT}.
         * Synonym of {@link #LEFT_TO_RIGHT}.
         * @stable ICU 3.0
         */
        DirectionalityLeftToRight = LeftToRight,

        /**
         * Directional type R
         * @stable ICU 2.1
         */
        RightToLeft = 1,

        /**
         * Equivalent to {@link
         * java.lang.Character#DIRECTIONALITY_RIGHT_TO_LEFT}.
         * Synonym of {@link #RIGHT_TO_LEFT}.
         * @stable ICU 3.0
         */
        DirectionalityRightToLeft = RightToLeft,

        /**
         * Directional type EN
         * @stable ICU 2.1
         */
        EuropeanNumber = 2,

        /**
         * Equivalent to {@link
         * java.lang.Character#DIRECTIONALITY_EUROPEAN_NUMBER}.
         * Synonym of {@link #EUROPEAN_NUMBER}.
         * @stable ICU 3.0
         */
        DirectionalityEuropeanNumber = (byte)EuropeanNumber,

        /**
         * Directional type ES
         * @stable ICU 2.1
         */
        EuropeanNumberSeparator = 3,

        /**
         * Equivalent to {@link
         * java.lang.Character#DIRECTIONALITY_EUROPEAN_NUMBER_SEPARATOR}.
         * Synonym of {@link #EUROPEAN_NUMBER_SEPARATOR}.
         * @stable ICU 3.0
         */
        DirectionalityEuropeanNumberSeparator = (byte)EuropeanNumberSeparator,

        /**
         * Directional type ET
         * @stable ICU 2.1
         */
        EuropeanNumberTerminator = 4,

        /**
         * Equivalent to {@link
         * java.lang.Character#DIRECTIONALITY_EUROPEAN_NUMBER_TERMINATOR}.
         * Synonym of {@link #EUROPEAN_NUMBER_TERMINATOR}.
         * @stable ICU 3.0
         */
        DirectionalityEuropeanNumberTerminator = (byte)EuropeanNumberTerminator,

        /**
         * Directional type AN
         * @stable ICU 2.1
         */
        ArabicNumber = 5,

        /**
         * Equivalent to {@link
         * java.lang.Character#DIRECTIONALITY_ARABIC_NUMBER}.
         * Synonym of {@link #ARABIC_NUMBER}.
         * @stable ICU 3.0
         */
        DirectionalityArabicNumber = (byte)ArabicNumber,

        /**
         * Directional type CS
         * @stable ICU 2.1
         */
        CommonNumberSeparator = 6,

        /**
         * Equivalent to {@link
         * java.lang.Character#DIRECTIONALITY_COMMON_NUMBER_SEPARATOR}.
         * Synonym of {@link #COMMON_NUMBER_SEPARATOR}.
         * @stable ICU 3.0
         */
        DirectionalityCommonNumberSeparator = (byte)CommonNumberSeparator,

        /**
         * Directional type B
         * @stable ICU 2.1
         */
        BlockSeparator = 7,

        /**
         * Equivalent to {@link
         * java.lang.Character#DIRECTIONALITY_PARAGRAPH_SEPARATOR}.
         * Synonym of {@link #BLOCK_SEPARATOR}.
         * @stable ICU 3.0
         */
        DirectionalityParagraphSeparator = (byte)BlockSeparator,

        /**
         * Directional type S
         * @stable ICU 2.1
         */
        SegmentSeparator = 8,

        /**
         * Equivalent to {@link
         * java.lang.Character#DIRECTIONALITY_SEGMENT_SEPARATOR}.
         * Synonym of {@link #SEGMENT_SEPARATOR}.
         * @stable ICU 3.0
         */
        DirectionalitySegmentSeparator = (byte)SegmentSeparator,

        /**
         * Directional type WS
         * @stable ICU 2.1
         */
        WhiteSpaceNeutral = 9,

        /**
         * Equivalent to {@link java.lang.Character#DIRECTIONALITY_WHITESPACE}.
         * Synonym of {@link #WHITE_SPACE_NEUTRAL}.
         * @stable ICU 3.0
         */
        DirectionalityWhiteSpace = (byte)WhiteSpaceNeutral,

        /**
         * Directional type ON
         * @stable ICU 2.1
         */
        OtherNeutral = 10,

        /**
         * Equivalent to {@link java.lang.Character#DIRECTIONALITY_OTHER_NEUTRALS}.
         * Synonym of {@link #OTHER_NEUTRAL}.
         * @stable ICU 3.0
         */
        DirectionalityOtherNeutrals = (byte)OtherNeutral,

        /**
         * Directional type LRE
         * @stable ICU 2.1
         */
        LeftToRightEmbedding = 11,

        /**
         * Equivalent to {@link
         * java.lang.Character#DIRECTIONALITY_LEFT_TO_RIGHT_EMBEDDING}.
         * Synonym of {@link #LEFT_TO_RIGHT_EMBEDDING}.
         * @stable ICU 3.0
         */
        DirectionalityLeftToRightEmbedding = (byte)LeftToRightEmbedding,

        /**
         * Directional type LRO
         * @stable ICU 2.1
         */
        LeftToRightOverride = 12,

        /**
         * Equivalent to {@link
         * java.lang.Character#DIRECTIONALITY_LEFT_TO_RIGHT_OVERRIDE}.
         * Synonym of {@link #LEFT_TO_RIGHT_OVERRIDE}.
         * @stable ICU 3.0
         */
        DirectionalityLeftToRightOverride = (byte)LeftToRightOverride,

        /**
         * Directional type AL
         * @stable ICU 2.1
         */
        RightToLeftArabic = 13,

        /**
         * Equivalent to {@link
         * java.lang.Character#DIRECTIONALITY_RIGHT_TO_LEFT_ARABIC}.
         * Synonym of {@link #RIGHT_TO_LEFT_ARABIC}.
         * @stable ICU 3.0
         */
        DirectionalityRightToLeftArabic = (byte)RightToLeftArabic,

        /**
         * Directional type RLE
         * @stable ICU 2.1
         */
        RightToLeftEmbedding = 14,

        /**
         * Equivalent to {@link
         * java.lang.Character#DIRECTIONALITY_RIGHT_TO_LEFT_EMBEDDING}.
         * Synonym of {@link #RIGHT_TO_LEFT_EMBEDDING}.
         * @stable ICU 3.0
         */
        DirectionalityRightToLeftEmbedding = (byte)RightToLeftEmbedding,

        /**
         * Directional type RLO
         * @stable ICU 2.1
         */
        RightToLeftOverride = 15,

        /**
         * Equivalent to {@link
         * java.lang.Character#DIRECTIONALITY_RIGHT_TO_LEFT_OVERRIDE}.
         * Synonym of {@link #RIGHT_TO_LEFT_OVERRIDE}.
         * @stable ICU 3.0
         */
        DirectionalityRightToLeftOverride = (byte)RightToLeftOverride,

        /**
         * Directional type PDF
         * @stable ICU 2.1
         */
        PopDirectionalFormat = 16,

        /**
         * Equivalent to {@link
         * java.lang.Character#DIRECTIONALITY_POP_DIRECTIONAL_FORMAT}.
         * Synonym of {@link #POP_DIRECTIONAL_FORMAT}.
         * @stable ICU 3.0
         */
        DirectionalityPopDirectionalFormat = (byte)PopDirectionalFormat,

        /**
         * Directional type NSM
         * @stable ICU 2.1
         */
        DirNonSpacingMark = 17,

        /**
         * Equivalent to {@link
         * java.lang.Character#DIRECTIONALITY_NONSPACING_MARK}.
         * Synonym of {@link #DIR_NON_SPACING_MARK}.
         * @stable ICU 3.0
         */
        DirectionalityNonSpacingMark = (byte)DirNonSpacingMark,

        /**
         * Directional type BN
         * @stable ICU 2.1
         */
        BoundaryNeutral = 18,

        /**
         * Equivalent to {@link
         * java.lang.Character#DIRECTIONALITY_BOUNDARY_NEUTRAL}.
         * Synonym of {@link #BOUNDARY_NEUTRAL}.
         * @stable ICU 3.0
         */
        DirectionalityBoundaryNeutral = (byte)BoundaryNeutral,

        /**
         * Directional type FSI
         * @stable ICU 52
         */
        FirstStrongIsolate = 19,

        /**
         * Directional type LRI
         * @stable ICU 52
         */
        LeftToRightIsolate = 20,

        /**
         * Directional type RLI
         * @stable ICU 52
         */
        RightToLeftIsolate = 21,

        /**
         * Directional type PDI
         * @stable ICU 52
         */
        PopDirectionalIsolate = 22,

        /**
         * One more than the highest normal ECharacterDirection value.
         * The highest value is available via UCharacter.getIntPropertyMaxValue(UProperty.BIDI_CLASS).
         *
         * @deprecated 
         */
        [Obsolete("ICU 58 The numeric value may change over time, see ICU ticket #12420.")]
        CharDirectionCount = 23,

        /**
         * Undefined bidirectional character type. Undefined <code>char</code>
         * values have undefined directionality in the Unicode specification.
         * @stable ICU 3.0
         */
        DirectionalityUndefined = -1,
    }
}
