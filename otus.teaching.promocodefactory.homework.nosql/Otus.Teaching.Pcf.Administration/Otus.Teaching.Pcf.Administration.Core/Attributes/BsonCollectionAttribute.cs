﻿using System;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Otus.Teaching.Pcf.Administration.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class BsonCollectionAttribute : Attribute
    {
        public string CollectionName { get; }

        public BsonCollectionAttribute(string collectionName)
        {
            CollectionName = collectionName;
        }
    }
}