﻿using System;
#nullable enable

namespace ICU4N.Globalization
{
    /// <summary>
    /// An interface to represent the owner of a <see cref="NumberFormatRuleSet"/>. These are used
    /// internally to construct the rules. Rules need to be instantiated first before properties are set
    /// since any rule may reference any other rule.
    /// </summary>
    internal interface INumberFormatRules
    {
        /// <summary>
        /// Gets a reference to the current default rule set. The default
        /// rule set is the last public rule set in the description, or the one
        /// most recently set.
        /// </summary>
        NumberFormatRuleSet DefaultRuleSet { get; }

        /// <summary>
        /// Returns the named rule set. Throws an <see cref="ArgumentException"/>
        /// if this formatter doesn't have a rule set with that name.
        /// </summary>
        /// <param name="name">The name of the desired rule set.</param>
        /// <returns>The rule set with that name.</returns>
        /// <exception cref="ArgumentException">No rule exists with the provided <paramref name="name"/>.</exception>
        NumberFormatRuleSet FindRuleSet(string name); // ICU4N TODO: API - Change this to GetRuleSet()/TryGetRuleSet()/ContainsRuleSet() to make this easier to work with.
    }
}
