﻿using ICU4N.Support.Text;
using ICU4N.Text;
using ICU4N.Util;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using OpenType = ICU4N.Impl.ICUResourceBundle.OpenType; // ICU4N TODO: API - de-nest ?

namespace ICU4N.Impl
{
    /// <author>tomzhang</author>
    public class SimpleFilteredSentenceBreakIterator : BreakIterator
    {
        private BreakIterator @delegate;
        private UCharacterIterator text; // TODO(Tom): suffice to move into the local scope in next() ?
        private CharsTrie backwardsTrie; // i.e. ".srM" for Mrs.
        private CharsTrie forwardsPartialTrie; // Has ".a" for "a.M."

        /// <param name="adoptBreakIterator">Break iterator to adopt.</param>
        /// <param name="forwardsPartialTrie">Forward &amp; partial char trie to adopt.</param>
        /// <param name="backwardsTrie">Backward trie to adopt.</param>
        public SimpleFilteredSentenceBreakIterator(BreakIterator adoptBreakIterator, CharsTrie forwardsPartialTrie,
                CharsTrie backwardsTrie)
        {
            this.@delegate = adoptBreakIterator;
            this.forwardsPartialTrie = forwardsPartialTrie;
            this.backwardsTrie = backwardsTrie;
        }

        /// <summary>
        /// Reset the filter from the delegate.
        /// </summary>
        private void ResetState()
        {
            text = UCharacterIterator.GetInstance((CharacterIterator)@delegate.GetText().Clone());
        }

        /// <summary>
        /// Is there an exception at this point?
        /// </summary>
        /// <param name="n">The location of the possible break.</param>
        /// <returns></returns>
        private bool BreakExceptionAt(int n)
        {
            // Note: the C++ version of this function is SimpleFilteredSentenceBreakIterator::breakExceptionAt()

            int bestPosn = -1;
            int bestValue = -1;

            // loops while 'n' points to an exception
            text.Index = n;
            backwardsTrie.Reset();
            int uch;



            // Assume a space is following the '.' (so we handle the case: "Mr. /Brown")
            if ((uch = text.PreviousCodePoint()) == ' ')
            { // TODO: skip a class of chars here??
              // TODO only do this the 1st time?
            }
            else
            {
                uch = text.NextCodePoint();
            }

            Result r = Result.INTERMEDIATE_VALUE;

            while ((uch = text.PreviousCodePoint()) != UCharacterIterator.DONE && // more to consume backwards and..
                    ((r = backwardsTrie.NextForCodePoint(uch)).HasNext()))
            {// more in the trie
                if (r.HasValue())
                { // remember the best match so far
                    bestPosn = text.Index;
                    bestValue = backwardsTrie.GetValue();
                }
            }

            if (r.Matches())
            { // exact match?
                bestValue = backwardsTrie.GetValue();
                bestPosn = text.Index;
            }

            if (bestPosn >= 0)
            {
                if (bestValue == Builder.MATCH)
                { // exact match!
                    return true; // Exception here.
                }
                else if (bestValue == Builder.PARTIAL && forwardsPartialTrie != null)
                {
                    // make sure there's a forward trie
                    // We matched the "Ph." in "Ph.D." - now we need to run everything through the forwards trie
                    // to see if it matches something going forward.
                    forwardsPartialTrie.Reset();

                    Result rfwd = Result.INTERMEDIATE_VALUE;
                    text.Index = bestPosn; // hope that's close ..
                    while ((uch = text.NextCodePoint()) != BreakIterator.DONE
                            && ((rfwd = forwardsPartialTrie.NextForCodePoint(uch)).HasNext()))
                    {
                    }
                    if (rfwd.Matches())
                    {
                        // Exception here
                        return true;
                    } // else fall through
                } // else fall through
            } // else fall through
            return false; // No exception here.
        }

        /// <summary>
        /// Given that the delegate has already given its "initial" answer,
        /// find the NEXT actual (non-suppressed) break.
        /// </summary>
        /// <param name="n">Initial position from delegate.</param>
        /// <returns>New break position or <see cref="BreakIterator.DONE"/>.</returns>
        private int InternalNext(int n)
        {
            if (n == BreakIterator.DONE || // at end or
                    backwardsTrie == null)
            { // .. no backwards table loaded == no exceptions
                return n;
            }
            ResetState();

            int textLen = text.Length;

            while (n != BreakIterator.DONE && n != textLen)
            {
                // outer loop runs once per underlying break (from fDelegate).
                // loops while 'n' points to an exception.

                if (BreakExceptionAt(n))
                {
                    // n points to a break exception
                    n = @delegate.Next();
                }
                else
                {
                    // no exception at this spot
                    return n;
                }
            }
            return n; //hit underlying DONE or break at end of text
        }

        /// <summary>
        /// Given that the delegate has already given its "initial" answer,
        /// find the PREV actual (non-suppressed) break.
        /// </summary>
        /// <param name="n">Initial position from delegate.</param>
        /// <returns>New break position or <see cref="BreakIterator.DONE"/>.</returns>
        private int InternalPrev(int n)
        {
            if (n == 0 || n == BreakIterator.DONE || // at end or
                    backwardsTrie == null)
            { // .. no backwards table loaded == no exceptions
                return n;
            }
            ResetState();

            while (n != BreakIterator.DONE && n != 0)
            {
                // outer loop runs once per underlying break (from fDelegate).
                // loops while 'n' points to an exception.

                if (BreakExceptionAt(n))
                {
                    // n points to a break exception
                    n = @delegate.Previous();
                }
                else
                {
                    // no exception at this spot
                    return n;
                }
            }
            return n; //hit underlying DONE or break at end of text
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (this == obj)
                return true;
            if (GetType() != obj.GetType())
                return false;
            SimpleFilteredSentenceBreakIterator other = (SimpleFilteredSentenceBreakIterator)obj;
            return @delegate.Equals(other.@delegate) && text.Equals(other.text) && backwardsTrie.Equals(other.backwardsTrie)
                    && forwardsPartialTrie.Equals(other.forwardsPartialTrie);
        }

        public override int GetHashCode()
        {
            return (forwardsPartialTrie.GetHashCode() * 39) + (backwardsTrie.GetHashCode() * 11) + @delegate.GetHashCode();
        }

        public override object Clone()
        {
            SimpleFilteredSentenceBreakIterator other = (SimpleFilteredSentenceBreakIterator)base.Clone();
            return other;
        }


        public override int First()
        {
            // Don't suppress a break opportunity at the beginning of text.
            return @delegate.First();
        }

        public override int Preceding(int offset)
        {
            return InternalPrev(@delegate.Preceding(offset));
        }

        public override int Previous()
        {
            return InternalPrev(@delegate.Previous());
        }

        public override int Current
        {
            get { return @delegate.Current; }
        }

        public override bool IsBoundary(int offset)
        {
            if (!@delegate.IsBoundary(offset))
            {
                return false; // No underlying break to suppress?
            }

            // delegate thinks there's a break…
            if (backwardsTrie == null)
            {
                return true; // no data
            }

            ResetState();
            return !BreakExceptionAt(offset); // if there's an exception: no break.
        }

        public override int Next()
        {
            return InternalNext(@delegate.Next());
        }

        public override int Next(int n)
        {
            return InternalNext(@delegate.Next(n));
        }

        public override int Following(int offset)
        {
            return InternalNext(@delegate.Following(offset));
        }

        public override int Last()
        {
            // Don't suppress a break opportunity at the end of text.
            return @delegate.Last();
        }

        public override CharacterIterator GetText()
        {
            return @delegate.GetText();
        }

        public override void SetText(CharacterIterator newText)
        {
            @delegate.SetText(newText);
        }

        public class Builder : FilteredBreakIteratorBuilder // ICU4N TODO: API - de-nest ?
        {
            /// <summary>
            /// Filter set to store all exceptions.
            /// </summary>
            private HashSet<ICharSequence> filterSet = new HashSet<ICharSequence>();

            internal static readonly int PARTIAL = (1 << 0); // < partial - need to run through forward trie
            internal static readonly int MATCH = (1 << 1); // < exact match - skip this one.
            internal static readonly int SuppressInReverse = (1 << 0);
            internal static readonly int AddToForward = (1 << 1);

            public Builder(CultureInfo loc)
                : this(ULocale.ForLocale(loc))
            {
            }

            /// <summary>
            /// Create <see cref="SimpleFilteredSentenceBreakIterator.Builder"/> using given locale.
            /// </summary>
            /// <param name="loc">The locale to get filtered iterators.</param>
            public Builder(ULocale loc)
#pragma warning disable 612, 618
            : base()
#pragma warning restore 612, 618
            {
                ICUResourceBundle rb = ICUResourceBundle.GetBundleInstance(
                        ICUData.ICU_BRKITR_BASE_NAME, loc, OpenType.LOCALE_ROOT);

                ICUResourceBundle breaks = rb.FindWithFallback("exceptions/SentenceBreak");

                if (breaks != null)
                {
                    for (int index = 0, size = breaks.Length; index < size; ++index)
                    {
                        ICUResourceBundle b = (ICUResourceBundle)breaks.Get(index);
                        string br = b.GetString();
                        filterSet.Add(br.ToCharSequence());
                    }
                }
            }

            /// <summary>
            /// Create <see cref="SimpleFilteredSentenceBreakIterator.Builder"/> with no exception.
            /// </summary>
            public Builder()
#pragma warning disable 612, 618
            : base()
#pragma warning restore 612, 618
            {
            }

            internal override bool SuppressBreakAfter(ICharSequence str) // ICU4N TODO: API should be public
            {
                return filterSet.Add(str);
            }

            internal override bool UnsuppressBreakAfter(ICharSequence str) // ICU4N TODO: API should be public
            {
                return filterSet.Remove(str);
            }

            public override BreakIterator WrapIteratorWithFilter(BreakIterator adoptBreakIterator)
            {
                if (filterSet.Count == 0)
                {
                    // Short circuit - nothing to except.
                    return adoptBreakIterator;
                }

                CharsTrieBuilder builder = new CharsTrieBuilder();
                CharsTrieBuilder builder2 = new CharsTrieBuilder();

                int revCount = 0;
                int fwdCount = 0;

                int subCount = filterSet.Count;
                ICharSequence[] ustrs = new ICharSequence[subCount];
                int[] partials = new int[subCount];

                CharsTrie backwardsTrie = null; // i.e. ".srM" for Mrs.
                CharsTrie forwardsPartialTrie = null; // Has ".a" for "a.M."

                int i = 0;
                foreach (ICharSequence s in filterSet)
                {
                    ustrs[i] = s; // copy by value?
                    partials[i] = 0; // default: no partial
                    i++;
                }

                for (i = 0; i < subCount; i++)
                {
                    string thisStr = ustrs[i].ToString(); // TODO: don't cast to String?
                    int nn = thisStr.IndexOf('.'); // TODO: non-'.' abbreviations
                    if (nn > -1 && (nn + 1) != thisStr.Length)
                    {
                        // is partial.
                        // is it unique?
                        int sameAs = -1;
                        for (int j = 0; j < subCount; j++)
                        {
                            if (j == i)
                                continue;
                            if (thisStr.RegionMatches(0, ustrs[j].ToString() /* TODO */, 0, nn + 1))
                            {
                                if (partials[j] == 0)
                                { // hasn't been processed yet
                                    partials[j] = SuppressInReverse | AddToForward;
                                }
                                else if ((partials[j] & SuppressInReverse) != 0)
                                {
                                    sameAs = j; // the other entry is already in the reverse table.
                                }
                            }
                        }

                        if ((sameAs == -1) && (partials[i] == 0))
                        {
                            StringBuilder prefix = new StringBuilder(thisStr.Substring(0, (nn + 1) - 0)); // ICU4N: Checked 2nd parameter
                                                                                                          // first one - add the prefix to the reverse table.
                            prefix.Reverse();
                            builder.Add(prefix, PARTIAL);
                            revCount++;
                            partials[i] = SuppressInReverse | AddToForward;
                        }
                    }
                }

                for (i = 0; i < subCount; i++)
                {
                    string thisStr = ustrs[i].ToString(); // TODO
                    if (partials[i] == 0)
                    {
                        StringBuilder reversed = new StringBuilder(thisStr).Reverse();
                        builder.Add(reversed, MATCH);
                        revCount++;
                    }
                    else
                    {
                        // an optimization would be to only add the portion after the '.'
                        // for example, for "Ph.D." we store ".hP" in the reverse table. We could just store "D." in the
                        // forward,
                        // instead of "Ph.D." since we already know the "Ph." part is a match.
                        // would need the trie to be able to hold 0-length strings, though.
                        builder2.Add(thisStr, MATCH); // forward
                        fwdCount++;
                    }
                }

                if (revCount > 0)
                {
                    backwardsTrie = builder.Build(StringTrieBuilder.Option.FAST);
                }

                if (fwdCount > 0)
                {
                    forwardsPartialTrie = builder2.Build(StringTrieBuilder.Option.FAST);
                }
                return new SimpleFilteredSentenceBreakIterator(adoptBreakIterator, forwardsPartialTrie, backwardsTrie);
            }
        }
    }
}
