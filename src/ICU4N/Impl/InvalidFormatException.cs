﻿using System;

namespace ICU4N.Impl
{
#if FEATURE_SERIALIZABLE_EXCEPTIONS
    [Serializable]
#endif
    public class InvalidFormatException : Exception // ICU4N TODO: API Subclass FormatException?
    {
        //// Generated by serialver from JDK 1.4.1_01
        //internal static readonly long serialVersionUID = 8883328905089345791L;

        public InvalidFormatException() { }

        public InvalidFormatException(Exception innerException)
            : base(string.Empty, innerException)
        {
        }

        public InvalidFormatException(string message)
            : base(message)
        {
        }

#if FEATURE_SERIALIZABLE_EXCEPTIONS
        /// <summary>
        /// Initializes a new instance of this class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
        protected InvalidFormatException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}