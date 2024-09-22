﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using ICU4N.Impl;
using J2N.Text;
using System.Globalization;
using System;
using System.Text;
#nullable enable

namespace ICU4N.Text
{
    public sealed partial class LowerCaseMap : CaseMap
    {

        // ICU4N specific overload for convenience
        /// <summary>
        /// Lowercases a string.
        /// Casing is locale-dependent and context-sensitive.
        /// The result may be longer or shorter than the original.
        /// </summary>
        /// <param name="src">The original string.</param>
        /// <returns>The result string.</returns>
        /// <seealso cref="UChar.ToLower(CultureInfo, string)"/>
        /// <draft>ICU 60.1</draft>
        /// <provisional>This API might change or be removed in a future release.</provisional>
        public string Apply(ReadOnlySpan<char> src)
        {
            return CaseMapImpl.ToLower(GetCaseLocale(null), internalOptions, src);
        }

        /// <summary>
        /// Lowercases a string.
        /// Casing is locale-dependent and context-sensitive.
        /// The result may be longer or shorter than the original.
        /// </summary>
        /// <param name="locale">The locale ID. Can be null for <see cref="CultureInfo.CurrentCulture"/>.</param>
        /// <param name="src">The original string.</param>
        /// <returns>The result string.</returns>
        /// <seealso cref="UChar.ToLower(CultureInfo, string)"/>
        /// <draft>ICU 60</draft>
        /// <provisional>This API might change or be removed in a future release.</provisional>
        public string Apply(CultureInfo? locale, ReadOnlySpan<char> src)
        {
            return CaseMapImpl.ToLower(GetCaseLocale(locale), internalOptions, src);
        }
    
        // ICU4N specific overload for convenience
        /// <summary>
        /// Lowercases a string and optionally records edits (see <see cref="OmitUnchangedText"/>).
        /// Casing is locale-dependent and context-sensitive.
        /// The result may be longer or shorter than the original.
        /// </summary>
        /// <param name="src">The original string.</param>
        /// <param name="dest">A buffer for the result string. Must not be null.</param>
        /// <param name="edits">Records edits for index mapping, working with styled text,
        /// and getting only changes (if any).
        /// This function calls <see cref="Edits.Reset()"/> first. <paramref name="edits"/> can be null.
        /// </param>
        /// <returns><paramref name="dest"/> with the result string (or only changes) appended.</returns>
        /// <seealso cref="UChar.ToLower(CultureInfo, string)"/>
        /// <draft>ICU 60.1</draft>
        /// <provisional>This API might change or be removed in a future release.</provisional>
        public StringBuilder Apply(
            ReadOnlySpan<char> src, StringBuilder dest, Edits? edits)
        {
            return CaseMapImpl.ToLower(GetCaseLocale(null), internalOptions, src, dest, edits);
        }
        
        // ICU4N specific overload for convenience
        /// <summary>
        /// Lowercases a string and optionally records edits (see <see cref="OmitUnchangedText"/>).
        /// Casing is locale-dependent and context-sensitive.
        /// The result may be longer or shorter than the original.
        /// </summary>
        /// <param name="src">The original string.</param>
        /// <param name="dest">A buffer for the result string. Must not be null.</param>
        /// <param name="edits">Records edits for index mapping, working with styled text,
        /// and getting only changes (if any).
        /// This function calls <see cref="Edits.Reset()"/> first. <paramref name="edits"/> can be null.
        /// </param>
        /// <returns><paramref name="dest"/> with the result string (or only changes) appended.</returns>
        /// <seealso cref="UChar.ToLower(CultureInfo, string)"/>
        /// <draft>ICU 60.1</draft>
        /// <provisional>This API might change or be removed in a future release.</provisional>
        public T Apply<T>(
            ReadOnlySpan<char> src, T dest, Edits? edits) where T : IAppendable
        {
            return CaseMapImpl.ToLower(GetCaseLocale(null), internalOptions, src, dest, edits);
        }
        
        /// <summary>
        /// Lowercases a string and optionally records edits (see <see cref="OmitUnchangedText"/>).
        /// Casing is locale-dependent and context-sensitive.
        /// The result may be longer or shorter than the original.
        /// </summary>
        /// <param name="locale">The locale ID. Can be null for <see cref="CultureInfo.CurrentCulture"/>.</param>
        /// <param name="src">The original string.</param>
        /// <param name="dest">A buffer for the result string. Must not be null.</param>
        /// <param name="edits">Records edits for index mapping, working with styled text,
        /// and getting only changes (if any).
        /// This function calls <see cref="Edits.Reset()"/> first. <paramref name="edits"/> can be null.
        /// </param>
        /// <returns><paramref name="dest"/> with the result string (or only changes) appended.</returns>
        /// <seealso cref="UChar.ToLower(CultureInfo, string)"/>
        /// <draft>ICU 60</draft>
        /// <provisional>This API might change or be removed in a future release.</provisional>
        public StringBuilder Apply(
            CultureInfo? locale, ReadOnlySpan<char> src, StringBuilder dest, Edits? edits)
        {
            return CaseMapImpl.ToLower(GetCaseLocale(locale), internalOptions, src, dest, edits);
        }
        
        /// <summary>
        /// Lowercases a string and optionally records edits (see <see cref="OmitUnchangedText"/>).
        /// Casing is locale-dependent and context-sensitive.
        /// The result may be longer or shorter than the original.
        /// </summary>
        /// <param name="locale">The locale ID. Can be null for <see cref="CultureInfo.CurrentCulture"/>.</param>
        /// <param name="src">The original string.</param>
        /// <param name="dest">A buffer for the result string. Must not be null.</param>
        /// <param name="edits">Records edits for index mapping, working with styled text,
        /// and getting only changes (if any).
        /// This function calls <see cref="Edits.Reset()"/> first. <paramref name="edits"/> can be null.
        /// </param>
        /// <returns><paramref name="dest"/> with the result string (or only changes) appended.</returns>
        /// <seealso cref="UChar.ToLower(CultureInfo, string)"/>
        /// <draft>ICU 60</draft>
        /// <provisional>This API might change or be removed in a future release.</provisional>
        public T Apply<T>(
            CultureInfo? locale, ReadOnlySpan<char> src, T dest, Edits? edits) where T : IAppendable
        {
            return CaseMapImpl.ToLower(GetCaseLocale(locale), internalOptions, src, dest, edits);
        }
        }

    public sealed partial class UpperCaseMap : CaseMap
    {

        // ICU4N specific overload for convenience
        /// <summary>
        /// Uppercases a string.
        /// Casing is locale-dependent and context-sensitive.
        /// The result may be longer or shorter than the original.
        /// </summary>
        /// <param name="src">The original string.</param>
        /// <returns>The result string.</returns>
        /// <seealso cref="UChar.ToUpper(CultureInfo, string)"/>
        /// <draft>ICU 60.1</draft>
        /// <provisional>This API might change or be removed in a future release.</provisional>
        public string Apply(ReadOnlySpan<char> src)
        {
            return CaseMapImpl.ToUpper(GetCaseLocale(null), internalOptions, src);
        }

        /// <summary>
        /// Uppercases a string.
        /// Casing is locale-dependent and context-sensitive.
        /// The result may be longer or shorter than the original.
        /// </summary>
        /// <param name="locale">The locale ID. Can be null for <see cref="CultureInfo.CurrentCulture"/>.</param>
        /// <param name="src">The original string.</param>
        /// <returns>The result string.</returns>
        /// <seealso cref="UChar.ToUpper(CultureInfo, string)"/>
        /// <draft>ICU 60</draft>
        /// <provisional>This API might change or be removed in a future release.</provisional>
        public string Apply(CultureInfo? locale, ReadOnlySpan<char> src)
        {
            return CaseMapImpl.ToUpper(GetCaseLocale(locale), internalOptions, src);
        }
    
        // ICU4N specific overload for convenience
        /// <summary>
        /// Uppercases a string and optionally records edits (see <see cref="OmitUnchangedText"/>).
        /// Casing is locale-dependent and context-sensitive.
        /// The result may be longer or shorter than the original.
        /// </summary>
        /// <param name="src">The original string.</param>
        /// <param name="dest">A buffer for the result string. Must not be null.</param>
        /// <param name="edits">Records edits for index mapping, working with styled text,
        /// and getting only changes (if any).
        /// This function calls <see cref="Edits.Reset()"/> first. <paramref name="dest"/> can be null.
        /// </param>
        /// <returns><paramref name="dest"/> with the result string (or only changes) appended.</returns>
        /// <seealso cref="UChar.ToUpper(CultureInfo, string)"/>
        /// <draft>ICU 60.1</draft>
        /// <provisional>This API might change or be removed in a future release.</provisional>
        public StringBuilder Apply(
            ReadOnlySpan<char> src, StringBuilder dest, Edits? edits)
        {
            return CaseMapImpl.ToUpper(GetCaseLocale(null), internalOptions, src, dest, edits);
        }
        
        // ICU4N specific overload for convenience
        /// <summary>
        /// Uppercases a string and optionally records edits (see <see cref="OmitUnchangedText"/>).
        /// Casing is locale-dependent and context-sensitive.
        /// The result may be longer or shorter than the original.
        /// </summary>
        /// <param name="src">The original string.</param>
        /// <param name="dest">A buffer for the result string. Must not be null.</param>
        /// <param name="edits">Records edits for index mapping, working with styled text,
        /// and getting only changes (if any).
        /// This function calls <see cref="Edits.Reset()"/> first. <paramref name="dest"/> can be null.
        /// </param>
        /// <returns><paramref name="dest"/> with the result string (or only changes) appended.</returns>
        /// <seealso cref="UChar.ToUpper(CultureInfo, string)"/>
        /// <draft>ICU 60.1</draft>
        /// <provisional>This API might change or be removed in a future release.</provisional>
        public T Apply<T>(
            ReadOnlySpan<char> src, T dest, Edits? edits) where T : IAppendable
        {
            return CaseMapImpl.ToUpper(GetCaseLocale(null), internalOptions, src, dest, edits);
        }
        
        /// <summary>
        /// Uppercases a string and optionally records edits (see <see cref="OmitUnchangedText"/>).
        /// Casing is locale-dependent and context-sensitive.
        /// The result may be longer or shorter than the original.
        /// </summary>
        /// <param name="locale">The locale ID. Can be null for <see cref="CultureInfo.CurrentCulture"/>.</param>
        /// <param name="src">The original string.</param>
        /// <param name="dest">A buffer for the result string. Must not be null.</param>
        /// <param name="edits">Records edits for index mapping, working with styled text,
        /// and getting only changes (if any).
        /// This function calls <see cref="Edits.Reset()"/> first. <paramref name="dest"/> can be null.
        /// </param>
        /// <returns><paramref name="dest"/> with the result string (or only changes) appended.</returns>
        /// <seealso cref="UChar.ToUpper(CultureInfo, string)"/>
        /// <draft>ICU 60</draft>
        /// <provisional>This API might change or be removed in a future release.</provisional>
        public StringBuilder Apply(
            CultureInfo? locale, ReadOnlySpan<char> src, StringBuilder dest, Edits? edits)
        {
            return CaseMapImpl.ToUpper(GetCaseLocale(locale), internalOptions, src, dest, edits);
        }
        
        /// <summary>
        /// Uppercases a string and optionally records edits (see <see cref="OmitUnchangedText"/>).
        /// Casing is locale-dependent and context-sensitive.
        /// The result may be longer or shorter than the original.
        /// </summary>
        /// <param name="locale">The locale ID. Can be null for <see cref="CultureInfo.CurrentCulture"/>.</param>
        /// <param name="src">The original string.</param>
        /// <param name="dest">A buffer for the result string. Must not be null.</param>
        /// <param name="edits">Records edits for index mapping, working with styled text,
        /// and getting only changes (if any).
        /// This function calls <see cref="Edits.Reset()"/> first. <paramref name="dest"/> can be null.
        /// </param>
        /// <returns><paramref name="dest"/> with the result string (or only changes) appended.</returns>
        /// <seealso cref="UChar.ToUpper(CultureInfo, string)"/>
        /// <draft>ICU 60</draft>
        /// <provisional>This API might change or be removed in a future release.</provisional>
        public T Apply<T>(
            CultureInfo? locale, ReadOnlySpan<char> src, T dest, Edits? edits) where T : IAppendable
        {
            return CaseMapImpl.ToUpper(GetCaseLocale(locale), internalOptions, src, dest, edits);
        }
        }

    public sealed partial class FoldCaseMap : CaseMap
    {

        /// <summary>
        /// Case-folds a string.
        /// The result may be longer or shorter than the original.
        /// </summary>
        /// <remarks>
        /// Case-folding is locale-independent and not context-sensitive,
        /// but there is an option for whether to include or exclude mappings for dotted I
        /// and dotless i that are marked with 'T' in CaseFolding.txt.
        /// </remarks>
        /// <param name="src">The original string.</param>
        /// <returns>The result string.</returns>
        /// <seealso cref="UChar.FoldCase(string, Globalization.FoldCase)"/>
        /// <draft>ICU 60</draft>
        /// <provisional>This API might change or be removed in a future release.</provisional>
        public string Apply(ReadOnlySpan<char> src)
        {
            return CaseMapImpl.Fold(internalOptions, src);
        }
    
        /// <summary>
        /// Case-folds a string and optionally records edits (see <see cref="OmitUnchangedText"/>).
        /// The result may be longer or shorter than the original.
        /// </summary>
        /// <remarks>
        /// Case-folding is locale-independent and not context-sensitive,
        /// but there is an option for whether to include or exclude mappings for dotted I
        /// and dotless i that are marked with 'T' in CaseFolding.txt.
        /// </remarks>
        /// <param name="src">The original string.</param>
        /// <param name="dest">A buffer for the result string. Must not be null.</param>
        /// <param name="edits">
        /// Records edits for index mapping, working with styled text,
        /// and getting only changes (if any).
        /// This function calls <see cref="Edits.Reset()"/> first. <paramref name="edits"/> can be null.
        /// </param>
        /// <returns><paramref name="dest"/> with the result string (or only changes) appended.</returns>
        /// <seealso cref="UChar.FoldCase(string, Globalization.FoldCase)"/>
        /// <draft>ICU 59</draft>
        /// <provisional>This API might change or be removed in a future release.</provisional>
        public StringBuilder Apply(
            ReadOnlySpan<char> src, StringBuilder dest, Edits? edits)
        {
            return CaseMapImpl.Fold(internalOptions, src, dest, edits);
        }
        
        /// <summary>
        /// Case-folds a string and optionally records edits (see <see cref="OmitUnchangedText"/>).
        /// The result may be longer or shorter than the original.
        /// </summary>
        /// <remarks>
        /// Case-folding is locale-independent and not context-sensitive,
        /// but there is an option for whether to include or exclude mappings for dotted I
        /// and dotless i that are marked with 'T' in CaseFolding.txt.
        /// </remarks>
        /// <param name="src">The original string.</param>
        /// <param name="dest">A buffer for the result string. Must not be null.</param>
        /// <param name="edits">
        /// Records edits for index mapping, working with styled text,
        /// and getting only changes (if any).
        /// This function calls <see cref="Edits.Reset()"/> first. <paramref name="edits"/> can be null.
        /// </param>
        /// <returns><paramref name="dest"/> with the result string (or only changes) appended.</returns>
        /// <seealso cref="UChar.FoldCase(string, Globalization.FoldCase)"/>
        /// <draft>ICU 59</draft>
        /// <provisional>This API might change or be removed in a future release.</provisional>
        public T Apply<T>(
            ReadOnlySpan<char> src, T dest, Edits edits) where T : IAppendable
        {
            return CaseMapImpl.Fold(internalOptions, src, dest, edits);
        }
    
    }
}